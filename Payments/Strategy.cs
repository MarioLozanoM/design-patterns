public interface IPaymentStrategy
{
    string ProcessPayment(decimal amount);
}

public class PayPalStrategy : IPaymentStrategy
{
    private readonly IPaymentGateway _paymentGateway;
    private string _emailFrom;
    private string _emailTo;

    public PayPalStrategy(IPaymentGateway paymentGateway, string emailFrom, string emailTo)
    {
        _paymentGateway = paymentGateway;
        _emailFrom = emailFrom;
        _emailTo = emailTo;
    }

    public string ProcessPayment(decimal amount)
    {
        var details = new PaymentDetailsDto(amount, _emailFrom, _emailTo);
        return _paymentGateway.ProcessPayment(details);
    }
}

public class CardStrategy : IPaymentStrategy
{
    private readonly IPaymentGateway _paymentGateway;
    private string _cardNumber;
    private string _cvv;
    private string _expiryDate;

    public CardStrategy(IPaymentGateway paymentGateway, string cardNumber, string cvv, string expiryDate)
    {
        _paymentGateway = paymentGateway;
        _cardNumber = cardNumber;
        _cvv = cvv;
        _expiryDate = expiryDate;
    }

    public string ProcessPayment(decimal amount)
    {
        var details = new PaymentDetailsDto(amount, _cardNumber, _cvv, _expiryDate);
        return _paymentGateway.ProcessPayment(details);
    }
}

public class CashStrategy : IPaymentStrategy
{
    public string ProcessPayment(decimal amount)
    {
        return "Pago realizado en efectivo.";
    }
}