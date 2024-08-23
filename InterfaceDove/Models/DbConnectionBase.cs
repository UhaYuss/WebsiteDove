using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InterfaceDove.Models
{
    public abstract class DbConnectionBase
    {
        private string connectionString;

        public DbConnectionBase()
        {
            connectionString = "Data Source=DESKTOP-2G03BM8\\SQLEXPRESS;Initial Catalog=BanHangDove;Integrated Security=True";
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}