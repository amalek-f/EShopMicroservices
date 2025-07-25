﻿using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Features.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order)
    : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {      
        RuleFor(x => x.Order.CustomerId)
            .NotEmpty()
            .WithMessage("CustomerId is required.");
        RuleFor(x => x.Order.OrderName)
            .NotEmpty()
            .WithMessage("OrderName is required.");
        RuleForEach(x => x.Order.OrderItems)
            .NotEmpty()
            .WithMessage("OrderItems should not be empty.");
    }
}
