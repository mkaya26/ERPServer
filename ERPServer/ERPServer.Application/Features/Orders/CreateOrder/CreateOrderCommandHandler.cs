using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Orders.CreateOrder
{
    internal sealed class CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateOrderCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var lastOrder = await orderRepository.
                Where(f => f.OrderNumberYear == request.OrderDate.Year)
                .OrderByDescending(f => f.OrderNumber)
                .FirstOrDefaultAsync(cancellationToken);
            //
            long lastOrderNumber = 0;
            if (lastOrder is not null) lastOrderNumber = lastOrder.OrderNumber;
            //
            Order order = mapper.Map<Order>(request);
            order.OrderNumber = lastOrderNumber + 1;
            order.OrderNumberYear = (short)request.OrderDate.Year;
            //order.OrderNumberFull = "SP" + (short)request.OrderDate.Year + lastOrderNumber;
            //
            await orderRepository.AddAsync(order, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Sipariş başarıyla kaydedildi.";
        }
    }
}
