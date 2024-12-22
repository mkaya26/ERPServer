using Ardalis.SmartEnum;

namespace ERPServer.Domain.Enums
{
    public sealed class OrderStatusEnum : SmartEnum<OrderStatusEnum>
    {
        public static readonly OrderStatusEnum Pending = new OrderStatusEnum("Bekliyor", 1);
        public static readonly OrderStatusEnum RequirementsPlanWorked = new OrderStatusEnum("İhtiyaç Planı Çalışıldı", 2);
        public static readonly OrderStatusEnum BeingProduced = new OrderStatusEnum("Üretiliyor", 3);
        public static readonly OrderStatusEnum Complated = new OrderStatusEnum("Tamamlandı", 4);
        public OrderStatusEnum(string name, int value) : base(name, value)
        {
        }
    }
}
