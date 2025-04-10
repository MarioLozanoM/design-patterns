public class AddProductCommand : ICommand
{
    private readonly IShoppingCart _cart;
    private readonly string _product;

    public AddProductCommand(IShoppingCart cart, string product)
    {
        _cart = cart;
        _product = product;
    }

    public void Execute()
    {
        _cart.AddProduct(_product);
    }

    public void Undo()
    {
        _cart.RemoveProduct(_product);
    }
}

public class RemoveProductCommand : ICommand
{
    private readonly IShoppingCart _cart;
    private readonly string _product;

    public RemoveProductCommand(IShoppingCart cart, string product)
    {
        _cart = cart;
        _product = product;
    }

    public void Execute()
    {
        _cart.RemoveProduct(_product);
    }

    public void Undo()
    {
        _cart.AddProduct(_product);
    }
}
