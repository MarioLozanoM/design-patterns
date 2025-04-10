public interface IFilterPrototype<T> where T : class
{
    T Clone();
}

public class ProductFilter : IFilterPrototype<ProductFilter>
{
    const decimal DEFAULT_MIN_PRICE = 10;
    const decimal DEFAULT_MAX_PRICE = 1000;
    const double DEFAULT_MIN_RATING = 4.5;
    const string DEFAULT_ORDER_BY = "Name";
    const bool DEFAULT_ASCENDING = true;

    public string Name { get; set; }
    public string Category { get; set; }
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public double MinRating { get; set; }
    public string OrderBy { get; set; }
    public bool Ascending { get; set; }

    public ProductFilter()
    {
        Name = string.Empty;
        Category = string.Empty;
        MinPrice = DEFAULT_MIN_PRICE;
        MaxPrice = DEFAULT_MAX_PRICE;
        MinRating = DEFAULT_MIN_RATING;
        OrderBy = DEFAULT_ORDER_BY;
        Ascending = DEFAULT_ASCENDING;
    }

    public ProductFilter(string name, string category, decimal minPrice, decimal maxPrice, double minRating, string sortBy, bool ascending)
    {
        Name = name;
        Category = category;
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        MinRating = minRating;
        OrderBy = sortBy;
        Ascending = ascending;
    }

    public ProductFilter Clone()
    {
        return new ProductFilter()
        {
            Name = Name,
            Category = Category,
            MinPrice = MinPrice,
            MaxPrice = MaxPrice,
            MinRating = MinRating,
            OrderBy = OrderBy,
            Ascending = Ascending
        };
    }
}