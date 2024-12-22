using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPServer.Infrastructure.Configurations
{
    internal sealed class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.Property(p => p.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.UnitType).HasConversion(type => type.Value, value => UnitTypeEnum.FromValue(value));
        }
    }
}
