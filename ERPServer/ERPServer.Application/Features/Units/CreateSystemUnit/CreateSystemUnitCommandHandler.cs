using Model = ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;
using Microsoft.EntityFrameworkCore;
using ERPServer.Domain.Enums;

namespace ERPServer.Application.Features.Units.CreateSystemUnit
{
    internal sealed class CreateSystemUnitCommandHandler(
        IUnitRepository unitRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateSystemUnitCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateSystemUnitCommand request, CancellationToken cancellationToken)
        {
            List<Model.Unit> units = await unitRepository.GetAll().ToListAsync(cancellationToken);
            //
            unitRepository.DeleteRange(units);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            List<Model.Unit> newUnits = new List<Model.Unit>();
            //
            Model.Unit metre = new Model.Unit
            {
                Code = "M",
                Name = "Metre",
                Multiplier = 1,
                UnitType = UnitTypeEnum.LengthUnit
            };
            //
            newUnits.Add(metre);
            //
            Model.Unit santimetre = new Model.Unit
            {
                Code = "CM",
                Name = "Santimetre",
                Multiplier = 100,
                UnitType = UnitTypeEnum.LengthUnit,
                TopUnitId = metre.Id
            };
            //
            newUnits.Add(santimetre);
            //
            Model.Unit milimetre = new Model.Unit
            {
                Code = "MM",
                Name = "Milimetre",
                Multiplier = 1000,
                UnitType = UnitTypeEnum.LengthUnit,
                TopUnitId = metre.Id
            };
            //
            newUnits.Add(milimetre);
            //
            Model.Unit kilogram = new Model.Unit
            {
                Code = "KG",
                Name = "Kilogram",
                Multiplier = 1,
                UnitType = UnitTypeEnum.WeightUnit
            };
            //
            newUnits.Add(kilogram);
            //
            Model.Unit gram = new Model.Unit
            {
                Code = "GR",
                Name = "Gram",
                Multiplier = 1000,
                UnitType = UnitTypeEnum.WeightUnit,
                TopUnitId = kilogram.Id
            };
            //
            newUnits.Add(gram);
            //
            Model.Unit metreKare = new Model.Unit
            {
                Code = "M2",
                Name = "Metre Kare",
                Multiplier = 1,
                UnitType = UnitTypeEnum.AreaUnit
            };
            //
            newUnits.Add(metreKare);
            //
            Model.Unit santimetreKare = new Model.Unit
            {
                Code = "CM2",
                Name = "Santimetre Kare",
                Multiplier = 10000,
                UnitType = UnitTypeEnum.AreaUnit,
                TopUnitId = metreKare.Id
            };
            //
            newUnits.Add(santimetreKare);
            //
            Model.Unit milimetreKare = new Model.Unit
            {
                Code = "MM2",
                Name = "Milimetre Kare",
                Multiplier = 1000000,
                UnitType = UnitTypeEnum.AreaUnit,
                TopUnitId = metreKare.Id
            };
            //
            newUnits.Add(milimetreKare);
            //
            Model.Unit adet = new Model.Unit
            {
                Code = "ADET",
                Name = "Adet",
                Multiplier = 1,
                UnitType = UnitTypeEnum.PieceUnit
            };
            //
            newUnits.Add(adet);
            //
            Model.Unit litre = new Model.Unit
            {
                Code = "LT",
                Name = "Litre",
                Multiplier = 1,
                UnitType = UnitTypeEnum.VolumeUnit
            };
            //
            newUnits.Add(litre);
            //
            Model.Unit santilitre = new Model.Unit
            {
                Code = "CL",
                Name = "Santilitre",
                Multiplier = 100,
                UnitType = UnitTypeEnum.VolumeUnit,
                TopUnitId = litre.Id
            };
            //
            newUnits.Add(santilitre);
            //
            Model.Unit mililitre = new Model.Unit
            {
                Code = "CC",
                Name = "Mililitre",
                Multiplier = 1000,
                UnitType = UnitTypeEnum.VolumeUnit,
                TopUnitId = litre.Id
            };
            //
            newUnits.Add(mililitre);
            //
            await unitRepository.AddRangeAsync(newUnits,cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return "Sistem Birimleri Oluşturuldu.";
        }
    }
}
