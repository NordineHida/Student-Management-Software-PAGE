using Oracle.ManagedDataAccess.Client;
using System;

public class DataSourceProvider
{
    private static OracleConnection oneDataSource;

    private DataSourceProvider()
    {
    }

    public static OracleConnection GetOneDataSourceInstance()
    {
        if (oneDataSource == null)
        {
            oneDataSource = new OracleConnection();
            oneDataSource.ConnectionString = "User Id=IQ_BD_HIDA;Password=HIDA0000;Data Source=srv-iq-ora:1521/orclpdb.iut21.u-bourgogne.fr;";
            oneDataSource.Open();
        }
        return oneDataSource;
    }
}
