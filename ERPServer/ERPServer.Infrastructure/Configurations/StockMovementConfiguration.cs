using ERPServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPServer.Infrastructure.Configurations
{
    internal sealed class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.Property(f => f.NumberOfEntries).HasColumnType("decimal(7,2)");
            builder.Property(f => f.NumberOfOutputs).HasColumnType("decimal(7,2)");
            builder.Property(f => f.Price).HasColumnType("money");
        }
    }
}
