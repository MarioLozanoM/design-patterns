using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IQueryBuilder, QueryBuilder>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IShoppingCart, ShoppingCart>();
builder.Services.AddSingleton<ICommandInvoker, CommandInvoker>();
builder.Services.AddSingleton<PayPalAPI>();
builder.Services.AddSingleton<CardAPI>();
builder.Services.AddSingleton<PaymentGatewayFactory>();
builder.Services.AddSingleton<PayPalAdapter>();
builder.Services.AddSingleton<CardAdapter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    Env.Load();
}

app.UseHttpsRedirection();

//TODO: separate each section in a different file

app.MapGet("/products", (IProductRepository repository) =>
{
    var getProductDto = new GetProductDto(
        Name: string.Empty,
        Category: string.Empty,
        MinPrice: 0,
        MaxPrice: 0,
        MinRating: 0,
        OrderByField: "Name",
        Ascending: true
    );
    repository.GetProducts(getProductDto);
    return Results.Ok(repository.GetProducts(getProductDto));
})
.WithName("GetProduct")
.WithOpenApi()
.WithTags("Products");

app.MapGet("/product/{id}", (int id, IProductRepository repository) =>
{
    var product = repository.GetById(id);
    return product != null ? Results.Ok(product) : Results.NotFound("Producto no encontrado.");
})
.WithName("GetProductById")
.WithOpenApi()
.WithTags("Products");

app.MapGet("/product/name/{name}", (string name, IProductRepository repository) =>
{
    var product = repository.GetByName(name);
    return product != null ? Results.Ok(product) : Results.NotFound("Producto no encontrado.");
})
.WithName("GetProductByName")
.WithOpenApi()
.WithTags("Products");

app.MapPost("/add-product-by-name", (string product) =>
{
    //TODO: fix what happens when the product is not found
    var cart = app.Services.GetRequiredService<IShoppingCart>();
    var commandInvoker = app.Services.GetRequiredService<ICommandInvoker>();
    var addProductCommand = new AddProductCommand(cart: cart, productName: product);
    commandInvoker.ExecuteCommand(addProductCommand);
    return Results.Ok("Producto agregado al carrito.");
})
.WithName("AddProduct")
.WithOpenApi()
.WithTags("Cart");

app.MapPost("/add-product-by-id", (int productId) =>
{
    var cart = app.Services.GetRequiredService<IShoppingCart>();
    var commandInvoker = app.Services.GetRequiredService<ICommandInvoker>();
    var addProductCommand = new AddProductCommand(cart: cart, productId: productId);
    commandInvoker.ExecuteCommand(addProductCommand);
    return Results.Ok("Producto agregado al carrito.");
})
.WithName("AddProductById")
.WithOpenApi()
.WithTags("Cart");

app.MapDelete("/remove-product", (string product) =>
{
    var cart = app.Services.GetRequiredService<IShoppingCart>();
    var commandInvoker = app.Services.GetRequiredService<ICommandInvoker>();
    var removeProductCommand = new RemoveProductCommand(cart, product);
    commandInvoker.ExecuteCommand(removeProductCommand);
    return Results.Ok("Producto eliminado del carrito.");
})
.WithName("RemoveProduct")
.WithOpenApi()
.WithTags("Cart");

app.MapGet("/cart", () =>
{
    var cart = app.Services.GetRequiredService<IShoppingCart>();
    var productList = cart.ShowCart();
    return Results.Ok(productList);
})
.WithName("ShowCart")
.WithOpenApi()
.WithTags("Cart");

app.MapDelete("/clear-cart", () =>
{
    var cart = app.Services.GetRequiredService<IShoppingCart>();
    cart.ClearCart();
    return Results.Ok("Carrito limpiado.");
})
.WithName("ClearCart")
.WithOpenApi()
.WithTags("Cart");

app.MapPost("/undo", () =>
{
    var commandInvoker = app.Services.GetRequiredService<ICommandInvoker>();
    var undid = commandInvoker.UndoLastCommand();
    return Results.Ok(undid ? "Último comando deshecho." : "No hay comandos para deshacer.");
})
.WithName("UndoLastCommand")
.WithOpenApi()
.WithTags("Cart");

app.MapPost("/pay-with-cash", (decimal amount) =>
{
    IPaymentStrategy strategy = new CashStrategy();
    IPaymentRepository repository = new PaymentRepository(strategy, PaymentType.Cash);
    var message = repository.ProcessPayment(amount);
    return Results.Ok(message);
})
.WithName("PayWithCash")
.WithOpenApi()
.WithTags("Payments");

app.MapPost("/pay-with-card", (decimal amount, string cardNumber, string cvv, string expiryDate, PaymentGatewayFactory factory) =>
{
    var cardApi = new CardAPI();
    var paymentGateway = factory.GetPaymentGateway(PaymentType.Card);
    IPaymentStrategy strategy = new CardStrategy(paymentGateway, cardNumber, cvv, expiryDate);
    IPaymentRepository repository = new PaymentRepository(strategy, PaymentType.Card);
    var message = repository.ProcessPayment(amount);
    return Results.Ok(message);
})
.WithName("PayWithCard")
.WithOpenApi()
.WithTags("Payments");

app.MapPost("/pay-with-paypal", (decimal amount, string emailFrom, string emailTo, PaymentGatewayFactory factory) =>
{
    var payPalApi = new PayPalAPI();
    var paymentGateway = factory.GetPaymentGateway(PaymentType.PayPal);
    IPaymentStrategy strategy = new PayPalStrategy(paymentGateway, emailFrom, emailTo);
    IPaymentRepository repository = new PaymentRepository(strategy, PaymentType.PayPal);
    var message = repository.ProcessPayment(amount);
    return Results.Ok(message);
})
.WithName("PayWithPayPal")
.WithOpenApi()
.WithTags("Payments");

app.Run();