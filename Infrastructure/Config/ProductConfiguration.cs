using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Configure the Entity properties or type for the Product
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Name).HasMaxLength(250);
        }
    }
}
