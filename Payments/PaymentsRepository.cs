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
    private readonly IPaymentStrategy _paymentStrategy;
    private readonly DiscountHandler _discountHandler;
    private readonly BalanceHandler _balanceHandler;
    private readonly PaymentProcessorHandler _paymentProcessorHandler;

    public PaymentRepository(IPaymentStrategy paymentStrategy, PaymentType paymentType)
    {
        _paymentStrategy = paymentStrategy;
        _discountHandler = new DiscountHandler();
        _balanceHandler = new BalanceHandler();
        _paymentProcessorHandler = new PaymentProcessorHandler();
        _discountHandler.SetNextHandler(paymentType == PaymentType.Cash ? _paymentProcessorHandler : _balanceHandler);
        _balanceHandler.SetNextHandler(_paymentProcessorHandler);
    }

    public string ProcessPayment(decimal amount)
    {
        var processMessage = _paymentStrategy.ProcessPayment(amount);
        var message = _discountHandler.Handle(amount, processMessage);
        message += $"  ||  Pago procesado con {_paymentStrategy.GetType().Name}.";
        return message;
    }
}