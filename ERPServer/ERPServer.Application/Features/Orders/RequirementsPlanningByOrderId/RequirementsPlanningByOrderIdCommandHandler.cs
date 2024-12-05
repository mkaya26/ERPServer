using ERPServer.Domain.Dtos;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Orders.RequirementsPlanningByOrderId
{
    internal sealed class RequirementsPlanningByOrderIdCommandHandler(
        IOrderRepository orderRepository,
        IStockMovementRepository movementRepository,
        IRecipeRepository recipeRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<RequirementsPlanningByOrderIdCommand, Result<RequirementsPlanningByOrderIdCommandResponse>>
    {
        public async Task<Result<RequirementsPlanningByOrderIdCommandResponse>> Handle(RequirementsPlanningByOrderIdCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.Where(
                f => f.Id == request.Id)
                .Include(i => i.OrderDetails!)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(cancellationToken);
            //
            if (order is null)
            {
                return Result<RequirementsPlanningByOrderIdCommandResponse>.Failure("Sipariş Bulunamadı...");
            }
            //
            List<ProductDto> uretilmesiGerekenUrunListesi = new();
            List<ProductDto> requirementsPlaningProducts = new();
            //
            if(order.OrderDetails is not null)
            {
                foreach(var item in order.OrderDetails)
                {
                    var product = item.Product;
                    List<StockMovement> movements = await movementRepository.Where(f => f.ProductId == product!.Id)
                        .ToListAsync(cancellationToken);
                    //
                    decimal stock = movements.Sum(p => p.NumberOfEntries) - movements.Sum(p => p.NumberOfOutputs);
                    //
                    if(stock < item.Quantity)
                    {
                        ProductDto uretilmesiGerekenUrun = new()
                        {
                            Id = item.ProductId,
                            Name = product!.Name,
                            Quantity = item.Quantity - stock
                        };
                        //
                        uretilmesiGerekenUrunListesi.Add(uretilmesiGerekenUrun);
                    }
                }
                foreach(var item in uretilmesiGerekenUrunListesi)
                {
                    Recipe? recipe = await recipeRepository
                        .Where(p => p.ProductId == item.Id)
                        .Include(i => i.RecipeDetails!)
                        .ThenInclude(t => t.Product)
                        .FirstOrDefaultAsync(cancellationToken);
                    //
                    if(recipe is not null)
                    {
                        if(recipe.RecipeDetails is not null)
                        {
                            foreach (var urun in recipe.RecipeDetails)
                            {
                                List<StockMovement> urunMovements = await movementRepository.Where(f => f.ProductId == urun!.ProductId)
        .ToListAsync(cancellationToken);
                                //
                                decimal stock = urunMovements.Sum(p => p.NumberOfEntries) - urunMovements.Sum(p => p.NumberOfOutputs);
                                //
                                if(stock < urun.Quantity)
                                {
                                    ProductDto ihtiyacOlanUrun = new()
                                    {
                                        Id = urun.ProductId,
                                        Name = urun.Product!.Name,
                                        Quantity = urun.Quantity - stock
                                    };
                                    //
                                    requirementsPlaningProducts.Add(ihtiyacOlanUrun);
                                }
                            }
                        }
                    }
                }
            }
            //
            requirementsPlaningProducts = requirementsPlaningProducts.GroupBy(g => g.Id)
                .Select(g => new ProductDto
                {
                    Id = g.Key,
                    Name = g.First().Name,
                    Quantity = g.Sum(f => f.Quantity)
                }).ToList();
            //
            order.Status = OrderStatusEnum.RequirementsPlanWorked;
            orderRepository.Update(order);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return new RequirementsPlanningByOrderIdCommandResponse(
                DateOnly.FromDateTime(DateTime.Now),
                order.OrderNumberFull + " Nolu Siparişin İhtiyaç Planlaması", requirementsPlaningProducts);
        }
    }
}
