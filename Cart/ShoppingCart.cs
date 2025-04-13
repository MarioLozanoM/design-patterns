public interface IShoppingCart
{
    void AppProductById(int id);
    void AddProductByName(string productName);
    void RemoveProductById(int id);
    void RemoveProductByName(string productName);
    List<string> ShowCart();
    void ClearCart();
    int GetProductCount();
}

public class ShoppingCart : IShoppingCart
{
    private readonly List<Product> _products = [];
    private readonly IProductRepository _productRepository;

    public ShoppingCart()
    {
        _productRepository = new ProductRepository(new QueryBuilder());
    }

    public void AppProductById(int id)
    {
        ValidateProductId(id);
        var product = _productRepository.GetById(id);
        AddProduct(product);
    }

    public void AddProductByName(string productName)
    {
        ValidateProductName(productName);
        var product = _productRepository.GetByName(productName);
        AddProduct(product);
    }

    public void AddProduct(Product product)
    {
        //TODO: handle add quantity when repeated products are added
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product not found."); //TODO: create custom exceptions for different cases
        }
        _products.Add(product);
    }

    public void RemoveProductById(int id)
    {
        ValidateProductId(id);
        var product = _productRepository.GetById(id);
        RemoveProduct(product);
    }

    public void RemoveProductByName(string productName)
    {
        ValidateProductName(productName);
        var product = _productRepository.GetByName(productName);
        RemoveProduct(product);
    }

    public void RemoveProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product not found."); //TODO: create custom exceptions for different cases
        }
        if (!_products.Contains(product))
        {
            throw new Exception("Product not in cart"); //TODO: create custom exceptions for different cases
        }
        _products.Remove(product);
    }

    public List<string> ShowCart()
    {
        return _products.Select(p => p.ToString()).ToList();
    }

    public void ClearCart()
    {
        _products.Clear();
    }
    
    public int GetProductCount()
    {
        return _products.Count;
    }

    private void ValidateProductName(string productName)
    {
        if (string.IsNullOrEmpty(productName))
        {
            throw new ArgumentException("Product name cannot be null or empty.");
        }
    }

    private void ValidateProductId(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Product ID must be greater than zero.");
        }
    }
}