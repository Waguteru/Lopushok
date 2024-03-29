using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Lopushok
{
    public class DataBase
    {
        NpgsqlConnection npgsqlConnection = new NpgsqlConnection ("Server = localhost; port = 5432; DataBase = lopushok; User Id = postgres; Password = 123");

        public void OpenConnection()
        {
            if(npgsqlConnection.State == System.Data.ConnectionState.Closed)
            {
                npgsqlConnection.Open ();
            }
        }

        public void CloseConnection()
        {
            if(npgsqlConnection.State == System.Data.ConnectionState.Open)
            {
                npgsqlConnection.Close ();
            }
        }

        public NpgsqlConnection GetConnection()
        {
            return npgsqlConnection;
        }
    }
}
