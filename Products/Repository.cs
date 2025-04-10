public interface IProductRepository
{
    List<Product> GetProducts(GetProductDto getProductDto);
}

public record GetProductDto(string? Name, string? Category, decimal MinPrice, decimal MaxPrice, double MinRating, string OrderByField, bool Ascending);

public class ProductRepository : IProductRepository
{
    private readonly SqlConnection _connection;
    private IQueryBuilder _queryBuilder;

    public ProductRepository(IQueryBuilder queryBuilder)
    {
        DatabaseConnection dbConnection = DatabaseConnection.Instance;
        _connection = dbConnection.GetConnection();
        _queryBuilder = queryBuilder;
    }

    public List<Product> GetProducts(GetProductDto getProductDto)
    {
        var director = new QueryDirector(_queryBuilder);
        string query;
        if (string.IsNullOrEmpty(getProductDto.Name) &&
            string.IsNullOrEmpty(getProductDto.Category) &&
            getProductDto.MinPrice <= 0 && getProductDto.MaxPrice <= 0 &&
            getProductDto.MinRating <= 0)
        {
            query = director.BuildFeaturedProductsQuery();
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
                Product product = new Product
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Category = reader.GetString(reader.GetOrdinal("Category")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                    Rating = reader.GetDouble(reader.GetOrdinal("Rating"))
                };
                products.Add(product);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            _connection.Close();
        }

        return products;
    }
}