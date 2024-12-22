using ERPServer.Domain.Abstractions;
using ERPServer.Domain.Enums;

namespace ERPServer.Domain.Entities
{
    public class Unit : Entity
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Multiplier { get; set; } = 1;
        public Guid? TopUnitId { get; set; }
        public UnitTypeEnum UnitType { get; set; } = UnitTypeEnum.PieceUnit;
    }
}
