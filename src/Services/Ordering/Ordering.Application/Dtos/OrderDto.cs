﻿namespace Ordering.Application.Dtos;

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto PaymentMethod,
    OrderStatus Status,
    List<OrderItemDto> OrderItems);
