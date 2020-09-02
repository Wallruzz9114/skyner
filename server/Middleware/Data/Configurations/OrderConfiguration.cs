using System;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Middleware.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(order => order.ShippingAddress, onb => onb.WithOwner());
            builder.Property(order => order.OrderStatus)
                .HasConversion(
                    orderStatus => orderStatus.ToString(),
                    orderStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), orderStatus)
                );
            builder.HasMany(order => order.OrderItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}