public record PaymentDetailsDto
{
    public decimal Amount { get; init; }
    public string? From { get; init; }
    public string? To { get; init; }
    public string? CardNumber { get; init; }
    public string? Cvv { get; init; }
    public string? ExpiryDate { get; init; }

    public PaymentDetailsDto(decimal amount, string? from, string? to)
    {
        Amount = amount;
        From = from;
        To = to;
    }

    public PaymentDetailsDto(decimal amount, string? cardNumber, string? cvv, string? expiryDate)
    {
        Amount = amount;
        CardNumber = cardNumber;
        Cvv = cvv;
        ExpiryDate = expiryDate;
    }
}

public interface IPaymentGateway
{
    string ProcessPayment(PaymentDetailsDto details);
}

public class PaymentGatewayFactory
{
    private readonly IServiceProvider _services;

    public PaymentGatewayFactory(IServiceProvider services)
    {
        _services = services;
    }

    public IPaymentGateway GetPaymentGateway(PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Card => _services.GetRequiredService<CardAdapter>(),
            PaymentType.PayPal => _services.GetRequiredService<PayPalAdapter>(),
            _ => throw new ArgumentException("Invalid payment type")
        };
    }
}