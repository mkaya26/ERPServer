using ERPServer.Domain.Abstractions;
using ERPServer.Domain.Enums;

namespace ERPServer.Domain.Entities
{
    public sealed class Product : Entity
    {
        public string Name { get; set; } = string.Empty;
        public ProductTypeEnum Type { get; set; } = ProductTypeEnum.Product;
        public Guid UnitId { get; set; }
        public Unit? Unit { get; set; }
    }
}
