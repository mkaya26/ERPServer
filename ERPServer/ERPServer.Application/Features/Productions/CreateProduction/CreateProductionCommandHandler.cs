using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Productions.CreateProduction
{
    internal sealed class CreateProductionCommandHandler(
        IProductionRepository productionRepository,
        IRecipeRepository recipeRepository,
        IStockMovementRepository stockMovementRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateProductionCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateProductionCommand request, CancellationToken cancellationToken)
        {
            Production production = mapper.Map<Production>(request);
            //
            production.ProductionDate = DateTime.Now;
            //
            List<StockMovement> newStockMovements = new();
            //
            Recipe? recipe = await recipeRepository.Where(p => p.ProductId == request.ProductId)
                .Include(i => i.RecipeDetails!)
                .ThenInclude(t => t.Product)
                .FirstOrDefaultAsync(cancellationToken);
            //
            if (recipe is not null && recipe.RecipeDetails is not null)
            {
                var details = recipe.RecipeDetails;
                //
                foreach (var item in details)
                {
                    List<StockMovement> movements = await stockMovementRepository.Where(f => f.ProductId == item.ProductId).ToListAsync(cancellationToken);
                    //
                    List<Guid> depotIds = movements.GroupBy(g => g.DepotId)
                        .Select(s => s.Key)
                        .ToList();
                    //
                    decimal stock = movements.Sum(p => p.NumberOfEntries) - movements.Sum(p => p.NumberOfOutputs);
                    //
                    if(item.Quantity > stock)
                    {
                        return Result<string>.Failure(item.Product!.Name + " üründen üretim için yeterli miktar yok. Eksik miktar: " + (item.Quantity - stock));
                    }
                    //
                    foreach(var depotId in depotIds)
                    {
                        if (item.Quantity <= 0) break;
                        //
                        decimal quantity = movements.Where(f => f.DepotId == depotId)
                            .Sum(s => s.NumberOfEntries - s.NumberOfOutputs);
                        //
                        decimal totalAmount = movements
                            .Where(f => f.DepotId == depotId && f.NumberOfEntries > 0)
                            .Sum(s => s.Price * s.NumberOfEntries);
                        //
                        decimal totalEntiresQuantity = movements
                            .Where(f => f.DepotId == depotId && f.NumberOfEntries > 0)
                            .Sum(s => s.NumberOfEntries);
                        //
                        decimal price = totalAmount / totalEntiresQuantity;
                        //
                        StockMovement stockMovement = new()
                        {
                            ProductionId = production.Id,
                            ProductId = item.ProductId,
                            DepotId = depotId,
                            Price = price
                        };
                        //
                        if (item.Quantity <= quantity)
                        {
                            stockMovement.NumberOfOutputs = item.Quantity;
                        }
                        else
                        {
                            stockMovement.NumberOfOutputs = quantity;
                        }
                        item.Quantity -= quantity;
                        //
                        newStockMovements.Add(stockMovement);
                    }
                }
            }
            //
            await stockMovementRepository.AddRangeAsync(newStockMovements, cancellationToken);
            await productionRepository.AddAsync(production, cancellationToken);
            //
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Ürün başarıyla üretildi.";
        }
    }
}
