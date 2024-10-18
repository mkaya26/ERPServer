using ERPServer.Domain.Dtos;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.UpdateOrder
{
    public sealed record UpdateOrderCommand(
        Guid Id,
        Guid CustomerId,
        DateOnly OrderDate,
        DateOnly DeliveryDate,
        List<OrderDetailDto> OrderDetails) : IRequest<Result<string>>;
}
