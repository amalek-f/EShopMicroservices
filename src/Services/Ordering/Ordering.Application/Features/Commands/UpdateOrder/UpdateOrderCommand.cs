﻿namespace Ordering.Application.Features.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto Order)
    : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order.Id)
            .NotEmpty()
            .WithMessage("Order Id is required.");
        RuleFor(x => x.Order.CustomerId)
            .NotEmpty()
            .WithMessage("CustomerId is required.");
        RuleFor(x => x.Order.OrderName)
            .NotEmpty()
            .WithMessage("OrderName is required.");

    }
}