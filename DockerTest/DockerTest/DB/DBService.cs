using System.Data.SqlClient;

namespace DockerTest.DB;
public class DBService : IDisposable
{
    public SqlConnection Connection { get; set; }
    public DBService()
    {
        var csBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "mssql-db",
            InitialCatalog = "DockerTestData",
            UserID = "sa",
            Password = "admin123#1",
            TrustServerCertificate = true
        };
        Connection = new SqlConnection(csBuilder.ConnectionString);
        Connection.Open();
    }

    public void Dispose()
    {
        if(Connection != null && Connection.State != System.Data.ConnectionState.Closed)
        {
            Connection.Close();
        }
    }
}