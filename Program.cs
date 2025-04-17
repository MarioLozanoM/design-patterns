using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IQueryBuilder, QueryBuilder>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IShoppingCart, ShoppingCart>();
builder.Services.AddSingleton<ICommandInvoker, CommandInvoker>();
builder.Services.AddSingleton<PayPalAPI>();
builder.Services.AddSingleton<CardAPI>();
builder.Services.AddSingleton<PaymentGatewayFactory>();
builder.Services.AddSingleton<PayPalAdapter>();
builder.Services.AddSingleton<CardAdapter>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    Env.Load();
}

app.UseHttpsRedirection();

app.MapProductsEndpoints();
app.MapCartEndpoints();
app.MapPaymentsEndpoints();

app.Run();