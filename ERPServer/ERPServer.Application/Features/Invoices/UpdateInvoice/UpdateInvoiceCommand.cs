using ERPServer.Domain.Dtos;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.UpdateInvoice
{
    public sealed record UpdateInvoiceCommand(
        Guid Id,
        Guid CustomerId,
        DateOnly InvoiceDate,
        string InvoiceNumberFull,
        List<InvoiceDetailDto> InvoiceDetails,
        Guid? OrderId) : IRequest<Result<string>>;
}
