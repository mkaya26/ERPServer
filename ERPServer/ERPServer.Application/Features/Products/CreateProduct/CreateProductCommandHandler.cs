using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace ERPServer.Application.Features.Products.CreateProduct
{
    internal sealed class CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateProductCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext.User?.Claims.Select(f => f.Value).FirstOrDefault();
            //
            bool isNameExists = await productRepository.AnyAsync(f => f.Name == request.Name, cancellationToken);
            if (isNameExists) 
            {
                return Result<string>.Failure("Ürün adı daha önce kullanılmış.");
            }
            //
            Product product = mapper.Map<Product>(request);
            //
            product.CreateBy = userId?.ToUpper() ?? "";
            product.CreateDate = DateTime.Now;
            //
            await productRepository.AddAsync(product,cancellationToken); ;
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Ürün başarıyla kaydedildi.";
        }
    }
}
