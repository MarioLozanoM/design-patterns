public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
    public double Rating { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Category: {Category}, Price: {Price}, Rating: {Rating}";
    }
}