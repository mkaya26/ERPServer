using ERPServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPServer.Infrastructure.Configurations
{
    internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(p => p.Name).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.TaxDepartment).HasColumnType("varchar(150)");
            builder.Property(p => p.TaxNumber).HasColumnType("varchar(11)");
            builder.Property(p => p.City).HasColumnType("varchar(100)");
            builder.Property(p => p.Town).HasColumnType("varchar(100)");
            builder.Property(p => p.FullAddress).HasColumnType("varchar(250)");
        }
    }
}
