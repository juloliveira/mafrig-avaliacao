using System;
using System.Data;
using System.Data.SqlClient;

namespace Mafrig.Avaliacao.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        public IDbConnection GetConnection()
        {
            return new SqlConnection("Server=LOCALHOST\\SQLEXPRESS;Database=Mafrig;Trusted_Connection=True;");
        }
    }
}
