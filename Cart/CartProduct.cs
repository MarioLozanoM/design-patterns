public class CartProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public CartProduct(int id, string name, decimal price, int quantity)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public decimal TotalPrice => Price * Quantity;
    
    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Price: {Price}, Quantity: {Quantity}";
    }
}