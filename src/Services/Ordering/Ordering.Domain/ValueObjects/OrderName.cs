namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    private const int DefaultLength = 5; 
    public string Value { get; init; } = default!;
    private OrderName(string value) => Value = value;
    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
        if (string.IsNullOrWhiteSpace(value) || value.Length < DefaultLength)
        {
            throw new DomainException($"Order name must be at least {DefaultLength} characters long.");
        }
        return new OrderName(value);
    }
}
