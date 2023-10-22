namespace DineFine.Accessor.DataAccessors.Mssql;

public abstract class DataConstants
{
    public const string ConnectionString =
        "Server=tcp:dinefineserver.database.windows.net,1433;Initial Catalog=DineFineDb;Persist Security Info=False;User ID=XDADMIN;Password=Password123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=90;";
}