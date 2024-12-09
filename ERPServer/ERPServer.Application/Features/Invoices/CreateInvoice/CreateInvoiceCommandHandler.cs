using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.CreateInvoice
{
    internal sealed class CreateInvoiceCommandHandler(
        IInvoiceRepository invoiceRepository,
        IStockMovementRepository stockMovementRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateInvoiceCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            Invoice invoice = mapper.Map<Invoice>(request);
            //
            if(invoice.InvoiceDetails is not null)
            {
                List<StockMovement> movements = new();
                foreach(var item in invoice.InvoiceDetails)
                {
                    StockMovement movement = new()
                    {
                        InvoiceId = invoice.Id,
                        NumberOfEntries = request.InvoiceTypeValue == 1 ? item.Quantity : 0,
                        NumberOfOutputs = request.InvoiceTypeValue == 2 ? item.Quantity : 0,
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
            await invoiceRepository.AddAsync(invoice, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Fatura başarıyla oluşturuldu.";
        }
    }
}
