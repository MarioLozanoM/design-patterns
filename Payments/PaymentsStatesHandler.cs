public abstract class PaymentHandler
{
    protected PaymentHandler? NextHandler;

    public void SetNextHandler(PaymentHandler nextHandler)
    {
        NextHandler = nextHandler;
    }

    public abstract string Handle(decimal amount, string message = "");
}

public class DiscountHandler : PaymentHandler
{
    private readonly decimal _discountPercentage;

    public DiscountHandler(decimal discountPercentage = 0.1m)
    {
        _discountPercentage = discountPercentage;
    }

    public override string Handle(decimal amount, string message = "")
    {
        decimal discountedAmount = amount * (1-_discountPercentage);
        message += $"  ||  Descuento aplicado, nuevo monto: ${discountedAmount}";
        return NextHandler?.Handle(discountedAmount, message) ?? message;
    }
}

public class BalanceHandler : PaymentHandler
{
    private readonly CustomerAccount _customerAccount;
    public BalanceHandler()
    {
        _customerAccount = new CustomerAccount();
    }

    public override string Handle(decimal amount, string message = "")
    {
        message += $"  ||  Verificando saldo suficiente para pagar ${amount}...";

        var hasSufficientBalance = _customerAccount.HasSufficientBalance(amount);
        if (!hasSufficientBalance)
        {
            throw new Exception("Saldo insuficiente.");
        }
        
        return NextHandler?.Handle(amount, message) ?? message;
    }
}

public class PaymentProcessorHandler : PaymentHandler
{
    public override string Handle(decimal amount, string message = "")
    {
        message += $"  ||  Procesando el pago de ${amount}...";
        return NextHandler?.Handle(amount, message) ?? message;
    }
}