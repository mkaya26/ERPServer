using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.UpdateInvoice
{
    internal sealed class UpdateInvoiceCommandHandler(
        IInvoiceRepository invoiceRepository,
        IStockMovementRepository stockMovementRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateInvoiceCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            Invoice invoice = await invoiceRepository.GetByExpressionWithTrackingAsync(f => f.Id == request.Id, cancellationToken);
            //
            if(invoice is null)
            {
                return Result<string>.Failure("Fatura bulunamadı.");
            }
            //
            await stockMovementRepository.DeleteByExpressionAsync(f => f.InvoiceId == request.Id, cancellationToken);
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
                        NumberOfEntries = request.InvoiceType == 1 ? item.Quantity : 0,
                        NumberOfOutputs = request.InvoiceType == 2 ? item.Quantity : 0,
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
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Fatura başarıyla güncelleştirildi.";
        }
    }
}
