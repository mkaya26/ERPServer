using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Depots.CreateDepot
{
    internal sealed class CreateDepotCommandHandler(
    IDepotRepository DepotRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateDepotCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateDepotCommand request, CancellationToken cancellationToken)
        {
            Depot Depot = mapper.Map<Depot>(request);
            //
            await DepotRepository.AddAsync(Depot, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Depo kaydı başarıyla tamamlandı.";
        }
    }
}
