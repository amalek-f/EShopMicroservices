﻿namespace Ordering.Application.Features.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId)
    : ICommand<DeleteOrderResult>;

public record DeleteOrderResult(bool IsSuccess);

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("Order Id is required.");
    }
}
