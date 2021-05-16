using System;
using System.Data;
using System.Data.SqlClient;

namespace Rentering.Common.Infra
{
    public class RenteringDataContext : IDisposable
    {
        public RenteringDataContext()
        {
            Connection = new SqlConnection(DatabaseSettings.connectionString);
            Connection.Open();
        }

        public IDbConnection Connection { get; set; }
        public IDbTransaction Transaction { get; set; }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
