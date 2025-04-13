public static class CartEndpoints
{
    public static void MapCartEndpoints(this WebApplication app)
    {
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
    }
}