﻿namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string? CardName { get; } = default!;
    public string? CardNumber { get; } = default!;
    public string? Expiration { get; } = default!;
    public string? CVV { get; } = default!;  
    public int PaymentMethod { get; } = default!; // 0: CreditCard, 1: PayPal, etc.

    protected Payment()
    {
    }

    private Payment(string? cardName, string? cardNumber, string? expiration, string? cvv, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string? cardName, string? cardNumber, string? expiration, string? cvv, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrEmpty(cardName, nameof(cardName));
        ArgumentException.ThrowIfNullOrEmpty(cardNumber, nameof(cardNumber));
        ArgumentException.ThrowIfNullOrEmpty(cvv, nameof(cvv));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length,3);
      
        return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
    }
}
