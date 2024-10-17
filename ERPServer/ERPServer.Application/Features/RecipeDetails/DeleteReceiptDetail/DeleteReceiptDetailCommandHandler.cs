using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.RecipeDetails.DeleteReceiptDetail
{
    internal sealed class DeleteReceiptDetailCommandHandler(
        IRecipeDetailRepository recipeDetailRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteReceiptDetailCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteReceiptDetailCommand request, CancellationToken cancellationToken)
        {
            var recipeDetail = await recipeDetailRepository.GetByExpressionAsync(
                f => f.Id == request.Id, cancellationToken);
            if(recipeDetail is null)
            {
                return Result<string>.Failure("Reçete detayı bulunamadı.");
            }
            //
            recipeDetailRepository.Delete(recipeDetail);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Reçete detayı başarıyla silindi.";
        }
    }
}
