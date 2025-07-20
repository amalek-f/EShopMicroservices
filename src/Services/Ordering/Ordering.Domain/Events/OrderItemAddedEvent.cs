namespace Ordering.Domain.Events;

public record OrderItemAddedEvent(OrderItem OrderItem): IDomainEvent; 