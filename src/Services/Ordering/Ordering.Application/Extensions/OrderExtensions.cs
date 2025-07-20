namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(order => new OrderDto(
            order.Id.Value,
            order.CustomerId.Value,
            order.OrderName.Value,
            new AddressDto(order.ShippingAdress.FirstName, order.ShippingAdress.LastName, order.ShippingAdress.EmailAddress!, order.ShippingAdress.AddressLine, order.ShippingAdress.Country, order.ShippingAdress.State, order.ShippingAdress.ZipCode),
            new AddressDto(order.BillingAdress.FirstName, order.BillingAdress.LastName, order.BillingAdress.EmailAddress!, order.BillingAdress.AddressLine, order.BillingAdress.Country, order.BillingAdress.State, order.BillingAdress.ZipCode),
            new PaymentDto(order.Payment.CardNumber!, order.Payment.CardName!, order.Payment.Expiration!, order.Payment.CVV!, order.Payment.PaymentMethod),
            order.Status,
            [.. order.OrderItems.Select(item => new OrderItemDto(item.OrderId.Value, item.ProductId.Value, item.Quantity, item.Price))]
        ));       
    }

    public static OrderDto ToOrderDto(this Order order)
    {
        return DtoFromOrder(order);
    }

    private static OrderDto DtoFromOrder(Order order)
    {
        return new OrderDto(
                    order.Id.Value,
                    order.CustomerId.Value,
                    order.OrderName.Value,
                    new AddressDto(order.ShippingAdress.FirstName, order.ShippingAdress.LastName, order.ShippingAdress.EmailAddress!, order.ShippingAdress.AddressLine, order.ShippingAdress.Country, order.ShippingAdress.State, order.ShippingAdress.ZipCode),
                    new AddressDto(order.BillingAdress.FirstName, order.BillingAdress.LastName, order.BillingAdress.EmailAddress!, order.BillingAdress.AddressLine, order.BillingAdress.Country, order.BillingAdress.State, order.BillingAdress.ZipCode),
                    new PaymentDto(order.Payment.CardNumber!, order.Payment.CardName!, order.Payment.Expiration!, order.Payment.CVV!, order.Payment.PaymentMethod),
                    order.Status,
                    [.. order.OrderItems.Select(item => new OrderItemDto(item.OrderId.Value, item.ProductId.Value, item.Quantity, item.Price))]
                );
    }
}
