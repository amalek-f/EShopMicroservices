namespace Ordering.Application.Features.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
  
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        // update order entity from command object
        // save to database, etc.
        // return the created order result

        var orderId = OrderId.Of(command.Order.Id);
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken) ?? throw new OrderNotFoundException(command.Order.Id);
        UpdateOrderWithNewValues(order,command.Order);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateOrderResult(true);
    }

    private static void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        var shippingAddress = Address.Of(
            orderDto.ShippingAddress.FirstName,
            orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.EmailAddress,
            orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.Country,
            orderDto.ShippingAddress.State,
            orderDto.ShippingAddress.ZipCode);

        var billingAddress = Address.Of(
            orderDto.BillingAddress.FirstName,
            orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAddress,
            orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.Country,
            orderDto.BillingAddress.State,
            orderDto.BillingAddress.ZipCode);

        var updatedPayment = Payment.Of(
            orderDto.PaymentMethod.CardName,
            orderDto.PaymentMethod.CardNumber,
            orderDto.PaymentMethod.Expiration,
            orderDto.PaymentMethod.Cvv,
            orderDto.PaymentMethod.PaymentMethod);

        // ✅ Protect against missing or invalid status
        var status = Enum.IsDefined(orderDto.Status)
            ? orderDto.Status
            : order.Status;

        // ✅ Apply order-level updates
        order.Update(
            OrderName.Of(orderDto.OrderName),
            shippingAddress,
            billingAddress,
            updatedPayment,
            status);

        // ✅ Convert DTO items to domain values and update the list
        var itemTuples = orderDto.OrderItems.Select(x =>
            (ProductId.Of(x.ProductId), x.Quantity, x.Price));
        order.SetOrderItems(itemTuples);
    }

}

