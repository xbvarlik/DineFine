namespace DineFine.Accessor.DataAccessors.Mssql;

public abstract class DataConstants
{
    public const string AzureConnectionString =
        "Server=tcp:dinefineserver.database.windows.net,1433;Initial Catalog=DineFineDb;Persist Security Info=False;User ID=XDADMIN;Password=Password123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=90;";

    public const string ConnectionString = @"Data Source=LAPTOP-GACC8D0D\\\\MSSQLSERVER,1433;Initial Catalog=DineFineDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
}