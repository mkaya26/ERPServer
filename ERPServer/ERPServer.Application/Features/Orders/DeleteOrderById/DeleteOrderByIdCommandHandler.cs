﻿using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.DeleteOrderById
{
    internal sealed class DeleteOrderByIdCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteOrderByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteOrderByIdCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetByExpressionAsync(f => f.Id == request.Id, cancellationToken);
            if (order is null)
            {
                return Result<string>.Failure("Sipariş bulunamadı.");
            }
            //
            orderRepository.Delete(order);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Sipariş başarıyla silindi.";
        }
    }
}
