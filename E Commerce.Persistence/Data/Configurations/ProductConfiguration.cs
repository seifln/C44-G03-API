using E_Commerce.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(X => X.Name)
                .HasMaxLength(maxLength: 100);

            builder.Property(X => X.Description)
                .HasMaxLength(maxLength: 500);

            builder.Property(X => X.PictureUrl)
                .HasMaxLength(maxLength: 200);

            builder.Property(X => X.Price)
                .HasPrecision(precision: 18, scale: 2);

            builder.HasOne(X => X.ProductBrand)
                .WithMany()
                .HasForeignKey(X => X.BrandId);

            builder.HasOne( X => X.ProductType)
                .WithMany()
                .HasForeignKey( X => X.TypeId);
        }

    }
}
