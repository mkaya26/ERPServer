using ERPServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;
using Unit = ERPServer.Domain.Entities.Unit;

namespace ERPServer.Application.Features.Units.GetAllTopUnit
{
    internal sealed class GetAllTopUnitQueryHandler(
        IUnitRepository unitRepository) : IRequestHandler<GetAllTopUnitQuery, Result<List<Unit>>>
    {
        public async Task<Result<List<Unit>>> Handle(GetAllTopUnitQuery request, CancellationToken cancellationToken)
        {
            var units = await unitRepository.Where(f => f.TopUnitId == null).ToListAsync(cancellationToken);
            //
            return units;
        }
    }
}
