using AutoMapper;
using ERPServer.Domain.Dtos;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.UpdateOrder
{
    public sealed record UpdateOrderCommand(
        Guid Id,
        Guid CustomerId,
        DateTime OrderDate,
        DateTime DeliveryDate,
        List<OrderDetailDto> OrderDetails) : IRequest<Result<string>>;

    internal sealed class UpdateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateOrderCommand, Result<string>>
    {
        public Task<Result<string>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
