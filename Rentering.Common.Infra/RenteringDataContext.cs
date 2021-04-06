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

        public SqlConnection Connection { get; set; }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
    }
}
