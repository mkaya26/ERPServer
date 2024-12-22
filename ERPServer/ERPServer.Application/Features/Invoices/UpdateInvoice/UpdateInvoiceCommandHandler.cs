using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.UpdateInvoice
{
    internal sealed class UpdateInvoiceCommandHandler(
        IInvoiceRepository invoiceRepository,
        IInvoiceDetailRepository invoiceDetailRepository,
        IStockMovementRepository stockMovementRepository,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateInvoiceCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            Invoice? invoice = await invoiceRepository.WhereWithTracking(f => f.Id == request.Id)
                .Include(i => i.InvoiceDetails)
                .FirstOrDefaultAsync(cancellationToken);
            //
            if(invoice is null)
            {
                return Result<string>.Failure("Fatura bulunamadı.");
            }
            //
            await stockMovementRepository.DeleteByExpressionAsync(f => f.InvoiceId == request.Id, cancellationToken);
            //
            invoiceDetailRepository.DeleteRange(invoice.InvoiceDetails);
            //
            invoice.InvoiceDetails = request.InvoiceDetails.Select(s => new InvoiceDetail
            {
                InvoiceId = invoice.Id,
                DepotId = s.DepotId,
                ProductId = s.ProductId,
                Price = s.Price,
                Quantity = s.Quantity,
                CreateDate = DateTime.Now
            }).ToList();
            //
            await invoiceDetailRepository.AddRangeAsync(invoice.InvoiceDetails, cancellationToken);
            //
            mapper.Map(request, invoice);
            //
            if (request.InvoiceDetails is not null)
            {
                List<StockMovement> movements = new();
                foreach (var item in request.InvoiceDetails)
                {
                    StockMovement movement = new()
                    {
                        InvoiceId = request.Id,
                        NumberOfEntries = invoice.InvoiceType == 1 ? item.Quantity : 0,
                        NumberOfOutputs = invoice.InvoiceType == 2 ? item.Quantity : 0,
                        DepotId = item.DepotId,
                        ProductId = item.ProductId,
                        Price = item.Price,
                        CreateDate = DateTime.Now
                    };
                    //
                    movements.Add(movement);
                }
                await stockMovementRepository.AddRangeAsync(movements, cancellationToken);
            }
            //
            if (request.OrderId is not null)
            {
                Order? order = await orderRepository.GetByExpressionWithTrackingAsync(f => f.Id == request.OrderId, cancellationToken);
                //
                if (order is not null)
                {
                    order.Status = OrderStatusEnum.Complated;
                }
            }
            //
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Fatura başarıyla güncelleştirildi.";
        }
    }
}
