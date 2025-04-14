public static class CartEndpoints
{
    public static void MapCartEndpoints(this WebApplication app)
    {
        app.MapPost("/add-product-by-name", (string product, int quantity) =>
        {
            try
            {
                var cart = app.Services.GetRequiredService<IShoppingCart>();
                var commandInvoker = app.Services.GetRequiredService<ICommandInvoker>();
                var addProductCommand = new AddProductCommand(cart: cart, productName: product, quantity: quantity);
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
        .WithName("AddProductByName")
        .WithOpenApi()
        .WithTags("Cart");

        app.MapPost("/add-product-by-id", (int productId, int quantity) =>
        {
            try
            {
                var cart = app.Services.GetRequiredService<IShoppingCart>();
                var commandInvoker = app.Services.GetRequiredService<ICommandInvoker>();
                var addProductCommand = new AddProductCommand(cart: cart, productId: productId, quantity: quantity);
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

        app.MapDelete("/remove-product-by-name", (string product) =>
        {
            try
            {
                var cart = app.Services.GetRequiredService<IShoppingCart>();
                var commandInvoker = app.Services.GetRequiredService<ICommandInvoker>();
                var removeProductCommand = new RemoveProductCommand(cart: cart, productName: product);
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
        .WithName("RemoveProductByName")
        .WithOpenApi()
        .WithTags("Cart");

        app.MapDelete("/remove-product-by-id", (int productId) =>
        {
            try
            {
                var cart = app.Services.GetRequiredService<IShoppingCart>();
                var commandInvoker = app.Services.GetRequiredService<ICommandInvoker>();
                var removeProductCommand = new RemoveProductCommand(cart: cart, productId: productId);
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
        .WithName("RemoveProductById")
        .WithOpenApi()
        .WithTags("Cart");

        app.MapDelete("/decrease-product-by-name", (string product) =>
        {
            try
            {
                var cart = app.Services.GetRequiredService<IShoppingCart>();
                var commandInvoker = app.Services.GetRequiredService<ICommandInvoker>();
                var decreaseProductCommand = new DecreaseProductCommand(cart: cart, productName: product);
                commandInvoker.ExecuteCommand(decreaseProductCommand);
                return Results.Ok("Producto disminuido del carrito.");
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
        .WithName("DecreaseProductByName")
        .WithOpenApi()
        .WithTags("Cart");

        app.MapDelete("/decrease-product-by-id", (int productId) =>
        {
            try
            {
                var cart = app.Services.GetRequiredService<IShoppingCart>();
                var commandInvoker = app.Services.GetRequiredService<ICommandInvoker>();
                var decreaseProductCommand = new DecreaseProductCommand(cart: cart, productId: productId);
                commandInvoker.ExecuteCommand(decreaseProductCommand);
                return Results.Ok("Producto disminuido del carrito.");
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
        .WithName("DecreaseProductById")
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