public class PayPalAdapter : IPaymentGateway
{
    private readonly PayPalAPI _payPalApi;

    public PayPalAdapter(PayPalAPI payPalApi)
    {
        _payPalApi = payPalApi;
    }

    public string ProcessPayment(PaymentDetailsDto details)
    {
        return _payPalApi.MakePayment(details.Amount.ToString(), details.From!, details.To!);
    }
}

public class CardAdapter : IPaymentGateway
{
    private readonly CardAPI _cardApi;

    public CardAdapter(CardAPI cardApi)
    {
        _cardApi = cardApi;
    }

    public string ProcessPayment(PaymentDetailsDto details)
    {
        return _cardApi.ProcessTransaction((double)details.Amount, details.CardNumber!, details.Cvv!, details.ExpiryDate!);
    }
}