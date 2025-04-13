public static class PaymentsEndpoints
{
    public static void MapPaymentsEndpoints(this WebApplication app)
    {
        app.MapPost("/pay-with-cash", (decimal amount) =>
        {
            try
            {
                IPaymentStrategy strategy = new CashStrategy();
                IPaymentRepository repository = new PaymentRepository(strategy, PaymentType.Cash);
                var message = repository.ProcessPayment(amount);
                return Results.Ok(message);
            }
            catch (NotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        })
        .WithName("PayWithCash")
        .WithOpenApi()
        .WithTags("Payments");

        app.MapPost("/pay-with-card", (decimal amount, string cardNumber, string cvv, string expiryDate, PaymentGatewayFactory factory) =>
        {
            try
            {
                var cardApi = new CardAPI();
                var paymentGateway = factory.GetPaymentGateway(PaymentType.Card);
                IPaymentStrategy strategy = new CardStrategy(paymentGateway, cardNumber, cvv, expiryDate);
                IPaymentRepository repository = new PaymentRepository(strategy, PaymentType.Card);
                var message = repository.ProcessPayment(amount);
                return Results.Ok(message);
            }
            catch (NotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        })
        .WithName("PayWithCard")
        .WithOpenApi()
        .WithTags("Payments");

        app.MapPost("/pay-with-paypal", (decimal amount, string emailFrom, string emailTo, PaymentGatewayFactory factory) =>
        {
            try
            {
                var payPalApi = new PayPalAPI();
                var paymentGateway = factory.GetPaymentGateway(PaymentType.PayPal);
                IPaymentStrategy strategy = new PayPalStrategy(paymentGateway, emailFrom, emailTo);
                IPaymentRepository repository = new PaymentRepository(strategy, PaymentType.PayPal);
                var message = repository.ProcessPayment(amount);
                return Results.Ok(message);
            }
            catch (NotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        })
        .WithName("PayWithPayPal")
        .WithOpenApi()
        .WithTags("Payments");
    }
}