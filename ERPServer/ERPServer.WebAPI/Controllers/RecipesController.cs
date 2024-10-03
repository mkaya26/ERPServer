﻿using ERPServer.Application.Features.Recipies.CreateRecipe;
using ERPServer.Application.Features.Recipies.DeleteRecipeById;
using ERPServer.Application.Features.Recipies.GetAllRecipe;
using ERPServer.Application.Features.Recipies.GetByIdRecipeWithDetails;
using ERPServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERPServer.WebAPI.Controllers
{
    public class RecipesController : ApiController
    {
        public RecipesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> GetAll(GetAllRecipeQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteById(DeleteRecipeByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> GetByIdRecipeWithDetails(GetByIdRecipeWithDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
