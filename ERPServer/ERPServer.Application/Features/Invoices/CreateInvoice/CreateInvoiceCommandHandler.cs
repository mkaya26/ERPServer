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
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateInvoiceCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            Invoice invoice = mapper.Map<Invoice>(request);
            //
            await invoiceRepository.AddAsync(invoice, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Fatura başarıyla oluşturuldu.";
        }
    }
}
