public interface IDatabaseConnection
{
    SqlConnection GetConnection();
}

public class DatabaseConnection : IDatabaseConnection
{
    private static DatabaseConnection? _instance;

    private static readonly object _lock = new object();

    private SqlConnection _connection;

    // Constructor privado.
    private DatabaseConnection()
    {
        string connectionString = "Data Source=MARIO-LG;Initial Catalog=project;User ID=sa;Password=$$abc123;TrustServerCertificate=True;"; // TODO: move to secrets
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