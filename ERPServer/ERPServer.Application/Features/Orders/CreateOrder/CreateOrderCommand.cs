using ERPServer.Domain.Dtos;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.CreateOrder
{
    public sealed record CreateOrderCommand(
        Guid CustomerId,
        DateOnly OrderDate,
        DateOnly DeliveryDate,
        List<OrderDetailDto> OrderDetails) : IRequest<Result<string>>;
}
