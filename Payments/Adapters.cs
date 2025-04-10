public class PayPalAdapter : IPaymentGateway
{
    private readonly PayPalAPI _payPalApi;

    public PayPalAdapter(PayPalAPI payPalApi)
    {
        _payPalApi = payPalApi;
    }

    public string ProcessPayment(PaymentDetailsDto details)
    {
        _payPalApi.MakePayment(details.Amount.ToString(), details.From!, details.To!);
        return "Pago realizado con PayPal a través del adaptador.";
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
        _cardApi.ProcessTransaction((double)details.Amount, details.CardNumber!, details.Cvv!, details.ExpiryDate!);
        return "Pago realizado con Tarjeta de Crédito a través del adaptador.";
    }
}