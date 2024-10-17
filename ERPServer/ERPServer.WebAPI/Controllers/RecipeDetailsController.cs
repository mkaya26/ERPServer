using ERPServer.Application.Features.RecipeDetails.CreateRecipeDetail;
using ERPServer.Application.Features.RecipeDetails.DeleteReceiptDetail;
using ERPServer.Application.Features.RecipeDetails.UpdateRecipeDetail;
using ERPServer.Application.Features.Recipies.CreateRecipe;
using ERPServer.Application.Features.Recipies.DeleteRecipeById;
using ERPServer.Application.Features.Recipies.GetAllRecipe;
using ERPServer.Application.Features.Recipies.GetByIdRecipeWithDetails;
using ERPServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERPServer.WebAPI.Controllers
{
    public class RecipeDetailsController : ApiController
    {
        public RecipeDetailsController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeDetailCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteById(DeleteReceiptDetailCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateRecipeDetailCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
