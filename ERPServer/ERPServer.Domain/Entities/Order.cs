using ERPServer.Domain.Abstractions;
using ERPServer.Domain.Enums;

namespace ERPServer.Domain.Entities
{
    public sealed class Order : Entity
    {
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public long OrderNumber { get; set; }
        public short OrderNumberYear { get; set; }
        public string OrderNumberFull { get; set; } = string.Empty;
        public DateOnly OrderDate { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public OrderStatusEnum Status { get; set; } = OrderStatusEnum.Pending;
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
