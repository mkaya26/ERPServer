using ERPServer.Domain.Dtos;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.CreateInvoice
{
    public sealed record CreateInvoiceCommand(
        Guid CustomerId,
        DateOnly InvoiceDate,
        string InvoiceNumber,
        int InvoiceType,
        List<InvoiceDetailDto> InvoiceDetails) : IRequest<Result<string>>;
}
