﻿using Ordering.Application.Features.Commands.CreateOrder;

namespace Ordering.Application.Features.EventHandlers.Integration;

public class BasketCheckoutEventHandler
    (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        // ToDo: create new order and start order fullfillment process.
        logger.LogInformation("Integration Event Handled: {IntegrationEvent}", context.Message.GetType().Name);

        var command = MapToCreateOrderCommand(context.Message);

        await sender.Send(command);
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        // create full order with incoming event data.
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress,
             message.AddressLine, message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration,
            message.CVV, message.PaymentMethod);

        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            orderId,
            message.CustomerId,
            message.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            PaymentMethod: paymentDto,
            Status: OrderStatus.Pending,
            OrderItems:
            [
                new OrderItemDto(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),2,500),
                new OrderItemDto(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),1,400)
            ]);

        return new CreateOrderCommand(orderDto);

    }
}
