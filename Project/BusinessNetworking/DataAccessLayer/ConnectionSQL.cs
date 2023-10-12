using BusinessLogicLayer.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ConnectionSQL
    {
        protected string connectionString;

        public ConnectionSQL()
        {
            connectionString = "Server=mssqlstud.fhict.local;" +
                                "Database=dbi457570;" +
                                "User Id=dbi457570;" +
                                "Password=TestPass132;";
        }
    }
}
