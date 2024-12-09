using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.GetAllInvoice
{
    internal sealed class GetAllInvoiceQueryHandler(
        IInvoiceRepository invoiceRepository) : IRequestHandler<GetAllInvoiceQuery, Result<List<Invoice>>>
    {
        public async Task<Result<List<Invoice>>> Handle(GetAllInvoiceQuery request, CancellationToken cancellationToken)
        {
            List<Invoice> invoices = await invoiceRepository.Where(f => f.InvoiceType == InvoiceTypeEnum.FromValue(request.Type))
                .Include(i => i.Customer)
                .Include(i => i.InvoiceDetails!)
                .ThenInclude(i => i.Product)
                .Include(i => i.InvoiceDetails!)
                .ThenInclude(i => i.Depot)
                .OrderBy(f => f.InvoiceDate)
                .ToListAsync(cancellationToken);
            return invoices;
        }
    }
}
