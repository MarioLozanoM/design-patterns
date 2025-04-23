public interface IProductRepository
{
    List<Product> GetProducts(GetProductDto getProductDto);
    Product GetById(int id);
    Product GetByName(string name);
}

public record GetProductDto(string? Name, string? Category, decimal MinPrice, decimal MaxPrice, double MinRating, string OrderByField, bool Ascending);

public class ProductRepository : IProductRepository
{
    private readonly SqlConnection _connection;
    private IQueryBuilder _queryBuilder = new QueryBuilder();
    private readonly Dictionary<int, ProductFlyweight> _cache = new();

    public ProductRepository()
    {
        DatabaseConnection dbConnection = DatabaseConnection.Instance;
        _connection = dbConnection.GetConnection();
    }

    public Product GetById(int id)
    {
        if (_cache.ContainsKey(id)){
            return _cache[id].GetProduct();
        }

        _queryBuilder = new QueryBuilder();
        string query = _queryBuilder.SetId(id).BuildQuery();
        Product product = new();

        try
        {
            _connection.Open();
            SqlCommand command = new SqlCommand(query, _connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                product = BuildProduct(reader);
            }
            else
            {
                throw new NotFoundException("Product not found.");
            }
            _cache[id] = new ProductFlyweight(product);
        }
        finally
        {
            _connection.Close();
        }

        return product;
    }

    public Product GetByName(string name)
    {
        _queryBuilder = new QueryBuilder();
        string query = _queryBuilder.SetName(name).BuildQuery();
        Product product = new();

        try
        {
            _connection.Open();
            SqlCommand command = new SqlCommand(query, _connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                product = BuildProduct(reader);
            }
            else
            {
                throw new NotFoundException("Product not found.");
            }
        }
        finally
        {
            _connection.Close();
        }

        return product;
    }

    public List<Product> GetProducts(GetProductDto getProductDto)
    {
        _queryBuilder = new QueryBuilder();
        var director = new QueryDirector(_queryBuilder);
        string query;
        if (string.IsNullOrEmpty(getProductDto.Name) &&
            string.IsNullOrEmpty(getProductDto.Category) &&
            getProductDto.MinPrice <= 0 && 
            getProductDto.MaxPrice <= 0 &&
            getProductDto.MinRating <= 0)
        {
            query = director.BuildGetDefaultProductQuery();
        }
        else
        {
            query = _queryBuilder.SetName(getProductDto.Name ?? string.Empty)
                .SetCategory(getProductDto.Category ?? string.Empty)
                .SetPriceRange(getProductDto.MinPrice, getProductDto.MaxPrice)
                .SetMinRating(getProductDto.MinRating)
                .SetOrderBy(getProductDto.OrderByField, getProductDto.Ascending)
                .BuildQuery();
        }

        List<Product> products = new List<Product>();

        try
        {
            _connection.Open();
            SqlCommand command = new SqlCommand(query, _connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Product product = BuildProduct(reader);
                products.Add(product);
            }
        }
        finally
        {
            _connection.Close();
        }

        return products;
    }

    private Product BuildProduct(SqlDataReader reader)
    {
        return new Product
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            Category = reader.GetString(reader.GetOrdinal("Category")),
            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
            Rating = reader.GetDouble(reader.GetOrdinal("Rating"))
        };
    }
}