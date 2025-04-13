public interface IDatabaseConnection
{
    SqlConnection GetConnection();
}

public class DatabaseConnection : IDatabaseConnection
{
    private static DatabaseConnection? _instance;

    private static readonly object _lock = new object();

    private SqlConnection _connection;

    private DatabaseConnection()
    {
        var db_server = Environment.GetEnvironmentVariable("db_server");
        var db_name = Environment.GetEnvironmentVariable("db_name");
        var db_user = Environment.GetEnvironmentVariable("db_user");
        var db_password = Environment.GetEnvironmentVariable("db_password");
        if (string.IsNullOrEmpty(db_server) || string.IsNullOrEmpty(db_name) || string.IsNullOrEmpty(db_user) || string.IsNullOrEmpty(db_password))
        {
            throw new InvalidOperationException("Database connection parameters are not set in environment variables.");
        }
        string connectionString = $"Data Source={db_server};Initial Catalog={db_name};User ID={db_user};Password={db_password};TrustServerCertificate=True;";
        _connection = new SqlConnection(connectionString);
    }

    public static DatabaseConnection Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new DatabaseConnection();
                }
            }
            return _instance;
        }
    }

    public SqlConnection GetConnection()
    {
        return _connection;
    }
}