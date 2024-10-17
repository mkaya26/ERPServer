using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Orders.GetAllOrder
{
    internal sealed class GetAllOrderQueryHandler(
        IOrderRepository orderRepository) : IRequestHandler<GetAllOrderQuery, Result<List<Order>>>
    {
        public async Task<Result<List<Order>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            List<Order> result = await orderRepository
                .GetAll()
                .Include(p => p.Customer)
                .Include(p => p.OrderDetails!)
                .ThenInclude(i => i.Product)
                .OrderByDescending(p => p.OrderDate)
                .ToListAsync(cancellationToken);
            return result;
        }
    }
}
