using AutoMapper;
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
            mapper.Map(request, order);
            //
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Sipariş başarıyla güncellendi.";
        }
    }
}
