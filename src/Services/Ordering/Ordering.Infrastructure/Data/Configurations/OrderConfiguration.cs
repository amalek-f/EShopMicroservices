﻿using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .HasConversion(
                orderId => orderId.Value,
                dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(o => o.OrderName, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.ShippingAdress, adressBuilder =>
        {
            adressBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            adressBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();
            adressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50)
                .IsRequired();
            adressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180);
            adressBuilder.Property(a => a.Country)
                .HasMaxLength(50);
            adressBuilder.Property(a => a.State)
                .HasMaxLength(50);
            adressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.BillingAdress, adressBuilder =>
        {
            adressBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();
            adressBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();
            adressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50)
                .IsRequired();
            adressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(180);
            adressBuilder.Property(a => a.Country)
                .HasMaxLength(50);
            adressBuilder.Property(a => a.State)
                .HasMaxLength(50);
            adressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.Payment, paymentBuilder =>
        {
            paymentBuilder.Property(p => p.CardName)
                .HasMaxLength(50);

            paymentBuilder.Property(p => p.CardNumber)
               .HasMaxLength(24)
               .IsRequired();

            paymentBuilder.Property(p => p.Expiration)
                .HasMaxLength(10);

            paymentBuilder.Property(p => p.CVV)
               .HasMaxLength(3);

            paymentBuilder.Property(p => p.PaymentMethod);
        });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                s => s.ToString(),
                dbStatus => Enum.Parse<OrderStatus>(dbStatus));

        builder.Property(o => o.TotalPrice);
            //.HasColumnType("decimal(18,2)")
            //.HasComputedColumnSql("SUM(OrderItems.Price * OrderItems.Quantity)")
            //.ValueGeneratedOnAddOrUpdate()
            //.IsRequired();

    }
}
