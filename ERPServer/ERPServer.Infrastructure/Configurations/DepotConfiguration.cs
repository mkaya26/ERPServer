using ERPServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPServer.Infrastructure.Configurations
{
    internal sealed class DepotConfiguration : IEntityTypeConfiguration<Depot>
    {
        public void Configure(EntityTypeBuilder<Depot> builder)
        {
            builder.Property(p => p.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.City).HasColumnType("varchar(100)");
            builder.Property(p => p.Town).HasColumnType("varchar(100)");
            builder.Property(p => p.FullAddress).HasColumnType("varchar(250)");
        }
    }
}
