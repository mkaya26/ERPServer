using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Orders.UpdateOrder
{
    internal sealed class UpdateOrderCommandHandler(
        IOrderRepository orderRepository,
        IOrderDetailRepository orderDetailRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateOrderCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.Where(f => f.Id == request.Id)
                .Include(i => i.OrderDetails)
                .FirstOrDefaultAsync(cancellationToken);
            if(order is null)
            {
                return Result<string>.Failure("Sipariş Bulunamadı.");
            }
            //
            orderDetailRepository.DeleteRange(order.OrderDetails);
            //
            List<OrderDetail> newDetails = request.OrderDetails.Select(
                s => new OrderDetail
                {
                    OrderId = order.Id,
                    Price = s.Price,
                    ProductId = s.ProductId,
                    Quantity = s.Quantity
                }).ToList();
            //
            await orderDetailRepository.AddRangeAsync(newDetails);
            //
            mapper.Map(request, order);
            //
            orderRepository.Update(order);
            //
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Sipariş başarıyla güncellendi.";
        }
    }
}
