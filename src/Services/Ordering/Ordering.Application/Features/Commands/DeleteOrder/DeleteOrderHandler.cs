namespace Ordering.Application.Features.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>

{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        // Logic to delete the order from the database
        // If the order is not found, throw OrderNotFoundException
        // If successful, return DeleteOrderResult with IsSuccess = true

        var orderId = OrderId.Of(command.OrderId);
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken) ?? throw new OrderNotFoundException(command.OrderId);

        // Remove the order from the database context
        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        // Assuming the deletion was successful, return a successful result
        return new DeleteOrderResult(true);

    }
}
