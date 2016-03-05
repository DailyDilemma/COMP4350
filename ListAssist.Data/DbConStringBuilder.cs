using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListAssist.Data
{
    public class DbConStringBuilder
    {
        private string _databaseName = "";

        public DbConStringBuilder(string databaseName)
        {
            this._databaseName = databaseName;
        }

        public string getConnectionString()
        {            
            // Initialize the connection string builder for the
            // underlying provider.
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

            // Set the properties for the data source.
            sqlBuilder.DataSource = @"(localdb)\MSSQLLocalDB";
            sqlBuilder.InitialCatalog = this._databaseName;
            sqlBuilder.IntegratedSecurity = true;                        

            // Build and return the string
            return sqlBuilder.ToString(); 
        }
    }
}
