using ERPServer.Domain.Dtos;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.CreateInvoice
{
    public sealed record CreateInvoiceCommand(
        Guid CustomerId,
        DateOnly InvoiceDate,
        string InvoiceNumberFull,
        int InvoiceTypeValue,
        List<InvoiceDetailDto> InvoiceDetails,
        Guid? OrderId) : IRequest<Result<string>>;
}
