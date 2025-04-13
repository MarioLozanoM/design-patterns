public static class ProductsEndpoints
{
    public static void MapProductsEndpoints(this WebApplication app)
    {
        app.MapGet("/products", (IProductRepository repository) =>
        {
            try
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
        .WithName("GetProduct")
        .WithOpenApi()
        .WithTags("Products");

        app.MapGet("/product/{id}", (int id, IProductRepository repository) =>
        {
            try
            {
                var product = repository.GetById(id);
                return product != null ? Results.Ok(product) : Results.NotFound("Producto no encontrado.");
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
        .WithName("GetProductById")
        .WithOpenApi()
        .WithTags("Products");

        app.MapGet("/product/name/{name}", (string name, IProductRepository repository) =>
        {
            try
            {
                var product = repository.GetByName(name);
                return product != null ? Results.Ok(product) : Results.NotFound("Producto no encontrado.");
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
        .WithName("GetProductByName")
        .WithOpenApi()
        .WithTags("Products");
    }
}