﻿namespace Basket.API.Features.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);  

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        var isDeleted = await repository.DeleteBasket(command.UserName, cancellationToken);
        return new DeleteBasketResult(isDeleted);
    }
}

