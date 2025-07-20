namespace Ordering.Domain.Events;

public record OrderItemRemovedEvent(OrderItem OrderItem) : IDomainEvent;
