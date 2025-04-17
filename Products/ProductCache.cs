public interface IProductoFlyweight
{
    string GetDetails();
    Product GetProduct();
}

public class ProductFlyweight : IProductoFlyweight
{
    public Product _product { get; }

    public ProductFlyweight(Product product)
    {
        _product = product;
    }

    public string GetDetails()
    {
        return _product.ToString();
    }

    public Product GetProduct()
    {
        return _product;
    }
}
