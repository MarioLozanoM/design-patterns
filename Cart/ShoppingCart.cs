public interface IShoppingCart
{
    void AddProduct(string product);
    void RemoveProduct(string product);
    List<string> ShowCart();
    void ClearCart();
    int GetProductCount();
}

public class ShoppingCart : IShoppingCart
{
    private readonly List<string> _products = new List<string>();

    public void AddProduct(string product)
    {
        _products.Add(product);
    }

    public void RemoveProduct(string product)
    {
        _products.Remove(product);
    }

    public List<string> ShowCart()
    {
        return _products;
    }

    public void ClearCart()
    {
        _products.Clear();
    }
    public int GetProductCount()
    {
        return _products.Count;
    }
}