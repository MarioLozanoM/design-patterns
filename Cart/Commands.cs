public class BaseProductCommand
{
    public void AddProduct(IShoppingCart cart, string? productName, int? productId)
    {
        if (!string.IsNullOrEmpty(productName))
        {
            cart.AddProductByName(productName);
        }
        else if (productId.HasValue)
        {
            cart.AppProductById(productId.Value);
        }
    }

    public void RemoveProduct(IShoppingCart cart, string? productName, int? productId)
    {
        if (!string.IsNullOrEmpty(productName))
        {
            cart.RemoveProductByName(productName);
        }
        else if (productId.HasValue)
        {
            cart.RemoveProductById(productId.Value);
        }
    }
}

public class AddProductCommand : BaseProductCommand, ICommand
{
    private readonly IShoppingCart _cart;
    private readonly string? _productName;
    private readonly int? _productId;

    public AddProductCommand(IShoppingCart cart, string? productName = null, int? productId = null)
    {
        _cart = cart;
        _productName = productName;
        _productId = productId;
    }

    public void Execute()
    {
        AddProduct(_cart, _productName, _productId);
    }

    public void Undo()
    {
        RemoveProduct(_cart, _productName, _productId);
    }
}

public class RemoveProductCommand : BaseProductCommand, ICommand
{
    private readonly IShoppingCart _cart;
    private readonly string? _productName;
    private readonly int? _productId;

    public RemoveProductCommand(IShoppingCart cart, string? productName = null, int? productId = null)
    {
        _cart = cart;
        _productName = productName;
        _productId = productId;
    }

    public void Execute()
    {
        RemoveProduct(_cart, _productName, _productId);
    }

    public void Undo()
    {
        AddProduct(_cart, _productName, _productId);
    }
}
