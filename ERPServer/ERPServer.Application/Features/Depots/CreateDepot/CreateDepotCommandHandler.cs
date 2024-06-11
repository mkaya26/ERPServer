using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace ERPServer.Application.Features.Depots.CreateDepot
{
    internal sealed class CreateDepotCommandHandler(
    IDepotRepository DepotRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateDepotCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateDepotCommand request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext.User?.Claims.Select(f => f.Value).FirstOrDefault();
            //
            Depot Depot = mapper.Map<Depot>(request);
            //
            Depot.CreateBy = userId?.ToUpper() ?? "";
            Depot.CreateDate = DateTime.Now;
            //
            await DepotRepository.AddAsync(Depot, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Depo kaydı başarıyla tamamlandı.";
        }
    }
}
