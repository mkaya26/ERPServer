using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace ERPServer.Application.Features.Products.UpdateProduct
{
    internal sealed class UpdateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : IRequestHandler<UpdateProductCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext.User?.Claims.Select(f => f.Value).FirstOrDefault();
            //
            Product product = await productRepository.GetByExpressionWithTrackingAsync(f => f.Id == request.Id);
            if (product is null)
            {
                return Result<string>.Failure("Ürün bulunamadı.");
            }
            //
            if (product.Name != request.Name)
            {
                bool isNameExists = await productRepository.AnyAsync(f => f.Name == request.Name, cancellationToken);
                if (isNameExists)
                {
                    return Result<string>.Failure("Ürün adı daha önce kullanılmış.");
                }
            }
            //
            mapper.Map(request, product);
            //
            product.UpdateBy = userId?.ToUpper() ?? "";
            product.UpdateDate = DateTime.Now;
            //
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Ürün bilgileri güncellendi.";
        }
    }
}
