using ERPServer.Domain.Dtos;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Products.GetAllProduct
{
    internal sealed class GetAllProductQueryHandler(
        IProductRepository productRepository,
        IStockMovementRepository movementRepository) : IRequestHandler<GetAllProductQuery, Result<List<ProductDto>>>
    {
        public async Task<Result<List<ProductDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await productRepository.GetAll().OrderBy(p => p.Name).ToListAsync(cancellationToken);
            //
            List<ProductDto> result = products.Select(s => new ProductDto
            {
                Id = s.Id,
                Name = s.Name,
                Type = s.Type,
                Quantity = 0
            }).ToList();
            //
            foreach (var product in result)
            {
                decimal stock = await movementRepository.Where(f => f.ProductId == product.Id).SumAsync(s => s.NumberOfEntries - s.NumberOfOutputs);
                //
                product.Quantity = stock;
            }
            //
            return result;
        }
    }
}
