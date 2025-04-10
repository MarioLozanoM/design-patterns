public class PayPalAPI
{
    public void MakePayment(string amount, string from, string to)
    {
        Console.WriteLine($"PayPal: Procesando pago de ${amount} desde {from} a {to}...");
    }
}

public class CardAPI
{
    public void ProcessTransaction(double amount, string number, string cvv, string expiryDate)
    {
        Console.WriteLine($"Visa: Procesando transacción de ${amount} con tarjeta nro. {number}, de cvv {cvv} y fecha de expiración {expiryDate}...");
    }
}

public class CustomerAccount
{
    public bool hasSufficientBalance(decimal amount)
    {
        // Simulación de verificación de saldo.
        return true;
    }
}