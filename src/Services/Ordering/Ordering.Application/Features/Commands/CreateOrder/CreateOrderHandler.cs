namespace Ordering.Application.Features.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        // create order entity from command object
        // save to database, etc.
        // return the created order result

        var order = CreateNewOrder(command.Order);

        dbContext.Orders.Add(order);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);

    }

    private static Order CreateNewOrder(OrderDto order)
    {
        var shippingAddress = Address.Of(
            order.ShippingAddress.FirstName,
            order.ShippingAddress.LastName,
            order.ShippingAddress.EmailAddress,
            order.ShippingAddress.AddressLine,
            order.ShippingAddress.Country,
            order.ShippingAddress.State,
            order.ShippingAddress.ZipCode);

        var billingAddress = Address.Of(
            order.BillingAddress.FirstName,
            order.BillingAddress.LastName,
            order.BillingAddress.EmailAddress,
            order.BillingAddress.AddressLine,
            order.BillingAddress.Country,
            order.BillingAddress.State,
            order.BillingAddress.ZipCode);

        var newOrder = Order.Create(
            OrderId.Of(Guid.NewGuid()),
            CustomerId.Of(order.CustomerId),
            OrderName.Of(order.OrderName),
            shippingAddress,
            billingAddress,
            Payment.Of(
                order.PaymentMethod.CardName, 
                order.PaymentMethod.CardNumber, 
                order.PaymentMethod.Expiration, 
                order.PaymentMethod.Cvv,
                order.PaymentMethod.PaymentMethod));

        foreach (var item in order.OrderItems)
        {
            newOrder.AddItem(
                ProductId.Of(item.ProductId),
                item.Quantity,
                item.Price
                );
        }

        return newOrder;
    }
}
