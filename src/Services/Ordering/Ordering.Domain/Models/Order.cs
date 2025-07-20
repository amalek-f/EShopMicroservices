namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = [];
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address  ShippingAdress { get; private set; } = default!;
    public Address BillingAdress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice
    {
        get => OrderItems.Sum(item => item.Price * item.Quantity);
        private set { }
    }

    public static Order Create(
        OrderId orderId,
        CustomerId customerId,
        OrderName orderName,
        Address shippingAddress,
        Address billingAddress,
        Payment payment)
    {
        var order = new Order
        {
            Id = orderId,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAdress = shippingAddress,
            BillingAdress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending
        };
        order.AddDomainEvent(new OrderCreatedEvent(order));
        return order;
    }

    public void Update(
        OrderName orderName,
        Address shippingAddress,
        Address billingAddress,
        Payment payment,        
        OrderStatus status)
    {
        OrderName = orderName;
        ShippingAdress = shippingAddress;
        BillingAdress = billingAddress;
        Payment = payment;
        Status = status;
        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void AddItem(
        ProductId productId,
        int quantity,
        decimal price)
    {
        ArgumentNullException.ThrowIfNull(productId, nameof(productId));
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero.");
        var orderItem = new OrderItem(Id, productId, quantity, price);
        _orderItems.Add(orderItem);
        AddDomainEvent(new OrderItemAddedEvent(orderItem));
    }

    public void RemoveItem(OrderItemId orderItemId)
    {
        ArgumentNullException.ThrowIfNull(orderItemId, nameof(orderItemId));
        var orderItem = _orderItems.FirstOrDefault(item => item.Id == orderItemId) ?? throw new InvalidOperationException($"Order item with ID {orderItemId} not found.");
        _orderItems.Remove(orderItem);
        AddDomainEvent(new OrderItemRemovedEvent(orderItem));
    }

    public void SetOrderItems(IEnumerable<(ProductId productId, int quantity, decimal price)> items)
    {
        _orderItems.Clear();

        foreach (var (productId, quantity, price) in items)
        {
            //if (quantity <= 0)
            //    throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
            //if (price <= 0)
            //    throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero.");

            var item = new OrderItem(Id, productId, quantity, price);
            _orderItems.Add(item);
            AddDomainEvent(new OrderItemAddedEvent(item));
        }
    }

}

