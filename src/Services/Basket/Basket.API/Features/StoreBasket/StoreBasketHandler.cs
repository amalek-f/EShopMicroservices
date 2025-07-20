using Discount.Grpc.Protos;

namespace Basket.API.Features.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class StoreBasketCommandHandler
    (IBasketRepository repository,
    DiscountProtoService.DiscountProtoServiceClient discountProto)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        // Todo: comminicate with Discount.grpc to get the discounts for the items in the cart
        await DeductDiscount(command.Cart);

        // Store the basket in the database(use Marten upsert - if exist update, if not exist insert)
        var basket = await repository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(basket.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });
            item.Price = coupon.Amount > 0 ? item.Price - coupon.Amount : item.Price;
        }
    }
}
