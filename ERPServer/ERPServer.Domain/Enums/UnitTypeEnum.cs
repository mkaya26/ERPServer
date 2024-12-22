using Ardalis.SmartEnum;

namespace ERPServer.Domain.Enums
{
    public sealed class UnitTypeEnum : SmartEnum<UnitTypeEnum>
    {
        public static readonly UnitTypeEnum LengthUnit = new("Uzunluk Birim Seti", 1);
        public static readonly UnitTypeEnum AreaUnit = new("Alan Birim Seti", 2);
        public static readonly UnitTypeEnum VolumeUnit = new("Hacim Birim Seti", 3);
        public static readonly UnitTypeEnum WeightUnit = new("Ağırlık Birim Seti", 4);
        public static readonly UnitTypeEnum PieceUnit = new("Adet Birim Seti", 5);
        public UnitTypeEnum(string name, int value) : base(name, value)
        {
        }
    }
}
