using ERPServer.Domain.Entities;

namespace ERPServer.Domain.Dtos
{
    public sealed record OrderDetailDto(
        Guid ProductId,
        decimal Quantity,
        decimal Price);
}
