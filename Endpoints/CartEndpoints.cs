public static class CartEndpoints
{
    public static void MapCartEndpoints(this WebApplication app)
    {
        app.MapPost("/add-product-by-name", (string product) =>
        {
            try
            {
                var cart = app.Services.GetRequiredService<IShoppingCart>();
                var commandInvoker = app.Services.GetRequiredService<ICommandInvoker>();
                var addProductCommand = new AddProductCommand(cart: cart, productName: product);
                commandInvoker.ExecuteCommand(addProductCommand);
                return Results.Ok("Producto agregado al carrito.");
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
        .WithName("AddProduct")
        .WithOpenApi()
        .WithTags("Cart");

        app.MapPost("/add-product-by-id", (int productId) =>
        {
            try
            {
                var cart = app.Services.GetRequiredService<IShoppingCart>();
                var commandInvoker = app.Services.GetRequiredService<ICommandInvoker>();
                var addProductCommand = new AddProductCommand(cart: cart, productId: productId);
                commandInvoker.ExecuteCommand(addProductCommand);
                return Results.Ok("Producto agregado al carrito.");
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
        .WithName("AddProductById")
        .WithOpenApi()
        .WithTags("Cart");

        app.MapDelete("/remove-product", (string product) =>
        {
            try
            {
                var cart = app.Services.GetRequiredService<IShoppingCart>();
                var commandInvoker = app.Services.GetRequiredService<ICommandInvoker>();
                var removeProductCommand = new RemoveProductCommand(cart, product);
                commandInvoker.ExecuteCommand(removeProductCommand);
                return Results.Ok("Producto eliminado del carrito.");
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
        .WithName("RemoveProduct")
        .WithOpenApi()
        .WithTags("Cart");

        app.MapGet("/cart", () =>
        {
            try
            {
                var cart = app.Services.GetRequiredService<IShoppingCart>();
                var productList = cart.ShowCart();
                return Results.Ok(productList);
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
        .WithName("ShowCart")
        .WithOpenApi()
        .WithTags("Cart");

        app.MapDelete("/clear-cart", () =>
        {
            try
            {
                var cart = app.Services.GetRequiredService<IShoppingCart>();
                cart.ClearCart();
                return Results.Ok("Carrito limpiado.");
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
        .WithName("ClearCart")
        .WithOpenApi()
        .WithTags("Cart");

        app.MapPost("/undo", () =>
        {
            try
            {
                var commandInvoker = app.Services.GetRequiredService<ICommandInvoker>();
                var undid = commandInvoker.UndoLastCommand();
                return Results.Ok(undid ? "Último comando deshecho." : "No hay comandos para deshacer.");
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
        .WithName("UndoLastCommand")
        .WithOpenApi()
        .WithTags("Cart");
    }
}