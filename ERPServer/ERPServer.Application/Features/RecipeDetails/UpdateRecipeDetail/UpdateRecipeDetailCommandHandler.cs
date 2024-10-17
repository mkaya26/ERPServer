using AutoMapper;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.RecipeDetails.UpdateRecipeDetail
{
    internal sealed class UpdateRecipeDetailCommandHandler(
        IRecipeDetailRepository recipeDetailRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateRecipeDetailCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateRecipeDetailCommand request, CancellationToken cancellationToken)
        {
            var recipeDetail = await recipeDetailRepository.GetByExpressionWithTrackingAsync(
    f => f.Id == request.Id, cancellationToken);
            if (recipeDetail is null)
            {
                return Result<string>.Failure("Reçete detayı bulunamadı.");
            }
            //
            mapper.Map(request, recipeDetail);
            //
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "REçetedeki ürün başarıyla güncellendi.";
        }
    }
}
