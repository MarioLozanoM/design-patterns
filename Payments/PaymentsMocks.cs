public class PayPalAPI
{
    public string MakePayment(string amount, string from, string to)
    {
        return $"PayPal: Procesando pago de ${amount} desde {from} a {to}...";
    }
}

public class CardAPI
{
    public string ProcessTransaction(double amount, string number, string cvv, string expiryDate)
    {
        return $"Visa: Procesando transacción de ${amount} con tarjeta nro. {number}, de cvv {cvv} y fecha de expiración {expiryDate}...";
    }
}

public class CustomerAccount
{
    public bool HasSufficientBalance(decimal amount)
    {
        // Simulación de verificación de saldo.
        return true;
    }
}