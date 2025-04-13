public interface IQueryBuilder
{
    IQueryBuilder SetId(int id);
    IQueryBuilder SetName(string name);
    IQueryBuilder SetCategory(string category);
    IQueryBuilder SetPriceRange(decimal minPrice, decimal maxPrice);
    IQueryBuilder SetMinRating(double rating);
    IQueryBuilder SetOrderBy(string field, bool ascending);
    string BuildQuery();
}

public class QueryBuilder : IQueryBuilder
{
    private int? _id;
    private string? _name;
    private string? _category;
    private decimal _minPrice;
    private decimal _maxPrice;
    private double _minRating;
    private string? _orderByField;
    private bool _ascending;

    public IQueryBuilder SetId(int id)
    {
        _id = id;
        return this;
    }

    public IQueryBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public IQueryBuilder SetCategory(string category)
    {
        _category = category;
        return this;
    }

    public IQueryBuilder SetPriceRange(decimal minPrice, decimal maxPrice)
    {
        _minPrice = minPrice;
        _maxPrice = maxPrice;
        return this;
    }

    public IQueryBuilder SetMinRating(double rating)
    {
        _minRating = rating;
        return this;
    }

    public IQueryBuilder SetOrderBy(string field, bool ascending)
    {
        _orderByField = field;
        _ascending = ascending;
        return this;
    }

    public string BuildQuery()
    {
        string query = "SELECT * FROM Products WHERE 1=1";

        if (_id != null && _id > 0)
            query += $" AND Id = {_id}";

        if (!string.IsNullOrEmpty(_name))
            query += $" AND Name LIKE '%{_name}%'";

        if (!string.IsNullOrEmpty(_category))
            query += $" AND Category = '{_category}'";

        if (_minPrice > 0 && _maxPrice > 0)
            query += $" AND Price BETWEEN {_minPrice} AND {_maxPrice}";

        if (_minRating > 0)
            query += $" AND Rating >= {_minRating}";

        if (!string.IsNullOrEmpty(_orderByField))
            query += $" ORDER BY {_orderByField} " + (_ascending ? "ASC" : "DESC");

        return query;
    }
}

public class QueryDirector
{
    private readonly IQueryBuilder _builder;
    private readonly ProductFilter _defaultFilter;

    public QueryDirector(IQueryBuilder builder)
    {
        _builder = builder;
        _defaultFilter = new ProductFilter();
    }

    public string BuildGetProductsQuery()
    {
        var currentFilter = _defaultFilter.Clone();
        return _builder
            .SetCategory(currentFilter.Category)
            .SetPriceRange(currentFilter.MinPrice, currentFilter.MaxPrice)
            .SetMinRating(currentFilter.MinRating)
            .SetOrderBy(currentFilter.OrderBy, currentFilter.Ascending)
            .BuildQuery();
    }
}