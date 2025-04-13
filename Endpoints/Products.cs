public static class ProductsEndpoints
{
    public static void MapProductsEndpoints(this WebApplication app)
    {
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
    }
}