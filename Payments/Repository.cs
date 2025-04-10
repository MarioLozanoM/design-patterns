public interface IPaymentRepository
{
    string ProcessPayment(decimal amount);
}

public enum PaymentType
{
    Card,
    Cash,
    PayPal
}

public class PaymentRepository : IPaymentRepository
{
    private readonly IPaymentStrategy _paymentProcessor;
    private readonly DiscountHandler _discountHandler;
    private readonly BalanceHandler _balanceHandler;
    private readonly PaymentProcessorHandler _paymentProcessorHandler;

    public PaymentRepository(IPaymentStrategy paymentProcessor, PaymentType paymentType)
    {
        _paymentProcessor = paymentProcessor;
        _discountHandler = new DiscountHandler();
        _balanceHandler = new BalanceHandler();
        _paymentProcessorHandler = new PaymentProcessorHandler();
        _discountHandler.SetNextHandler(paymentType == PaymentType.Cash ? _paymentProcessorHandler : _balanceHandler);
        _balanceHandler.SetNextHandler(_paymentProcessorHandler);
    }

    public string ProcessPayment(decimal amount)
    {
        var processMessage = _paymentProcessor.ProcessPayment(amount);
        var message = _discountHandler.Handle(amount, processMessage);
        message += $"  ||  Pago procesado con {_paymentProcessor.GetType().Name}.";
        return message;
    }
}