using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace ERPServer.Application.Features.Depots.UpdateDepot
{
    internal sealed class UpdateDepotCommandHandler(
      IDepotRepository depotRepository,
      IUnitOfWork unitOfWork,
      IMapper mapper,
       IHttpContextAccessor httpContextAccessor) : IRequestHandler<UpdateDepotCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateDepotCommand request, CancellationToken cancellationToken)
        {
            var userId = httpContextAccessor.HttpContext.User?.Claims.Select(f => f.Value).FirstOrDefault();
            //
            Depot depot = await depotRepository.GetByExpressionWithTrackingAsync(f => f.Id == request.Id);
            //
            if (depot is null)
            {
                return Result<string>.Failure("Depo bulunamadı.");
            }
            //
            mapper.Map(request, depot);
            //
            depot.UpdateBy = userId?.ToUpper() ?? "";
            depot.UpdateDate = DateTime.Now;
            //
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Depo bilgileri başarıyla güncellendi.";
        }
    }
}
