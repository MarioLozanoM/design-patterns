public interface IShoppingCart
{
    void AppProductById(int id, int quantity = 1);
    void AddProductByName(string productName, int quantity = 1);
    void RemoveProductById(int id);
    void RemoveProductByName(string productName);
    void DecreaseProductById(int id);
    void DecreaseProductByName(string productName);
    List<string> ShowCart();
    void ClearCart();
    int GetProductCount();
}

public class ShoppingCart : IShoppingCart
{
    private readonly List<CartProduct> _products = [];
    private readonly IProductRepository _productRepository;

    public ShoppingCart(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void AppProductById(int id, int quantity = 1)
    {
        ValidateProductId(id);
        var product = _productRepository.GetById(id);
        AddProduct(new CartProduct(product.Id!, product.Name!, product.Price, quantity));
    }

    public void AddProductByName(string productName, int quantity = 1)
    {
        ValidateProductName(productName);
        var product = _productRepository.GetByName(productName);
        AddProduct(new CartProduct(product.Id!, product.Name!, product.Price, quantity));
    }

    public void AddProduct(CartProduct product)
    {
        if (product == null)
        {
            throw new NotFoundException($"Product not found.");
        }
        if (!ProductExists(product))
        {
            _products.Add(product);
        }
        else
        {
            IncreaseProductQuantity(product);
        }
    }

    private bool ProductExists(CartProduct product)
    {
        return _products.Any(p => p.Id == product.Id);
    }

    public void RemoveProductById(int id)
    {
        ValidateProductId(id);
        var product = _productRepository.GetById(id);
        RemoveProduct(new CartProduct(product.Id!, product.Name!, product.Price, 1));
    }

    public void RemoveProductByName(string productName)
    {
        ValidateProductName(productName);
        var product = _productRepository.GetByName(productName);
        RemoveProduct(new CartProduct(product.Id!, product.Name!, product.Price, 1));
    }

    public void RemoveProduct(CartProduct product)
    {
        if (product == null)
        {
            throw new NotFoundException($"Product not found.");
        }
        if (!ProductExists(product))
        {
            throw new NotFoundException("Product not in cart");
        }
        _products.Remove(_products.First(p => p.Id == product.Id));
    }

    public void DecreaseProductById(int id)
    {
        ValidateProductId(id);
        var product = _productRepository.GetById(id);
        DecreaseProduct(new CartProduct(product.Id!, product.Name!, product.Price, 1));
    }

    public void DecreaseProductByName(string productName)
    {
        ValidateProductName(productName);
        var product = _productRepository.GetByName(productName);
        DecreaseProduct(new CartProduct(product.Id!, product.Name!, product.Price, 1));
    }

    public void DecreaseProduct(CartProduct product)
    {
        if (product == null)
        {
            throw new NotFoundException($"Product not found.");
        }
        if (!ProductExists(product))
        {
            throw new NotFoundException("Product not in cart");
        }
        var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct != null && existingProduct.Quantity > 1)
        {
            existingProduct.Quantity--;
        }
        else if (existingProduct != null)
        {
            _products.Remove(existingProduct);
        }
    }

    public List<string> ShowCart()
    {
        return [.. _products.Select(p => p.ToString())];
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
            throw new BadRequestException("Product name cannot be null or empty.");
        }
    }

    private void ValidateProductId(int id)
    {
        if (id <= 0)
        {
            throw new BadRequestException("Product ID must be greater than zero.");
        }
    }

    private void IncreaseProductQuantity(CartProduct product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            existingProduct.Quantity++;
        }
    }
}