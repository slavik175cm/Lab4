using System;
using System.Collections.Generic;
using System.Text;

namespace DataManager.Options
{
    public class DataBaseOptions
    {
        public string DataSource;
        public string InitialCatalog;
        public bool IntegratedSecurity;
        public string User;
        public string Password;

        public string BuildConnectionString()
        {
            string connectionString;
            if (IntegratedSecurity) 
                connectionString = $"Data Source={DataSource}; Initial Catalog={InitialCatalog}; Integrated Security=True;";
            else connectionString = $"Data Source={DataSource}; Initial Catalog={InitialCatalog}; Integrated Security=False; User={User}; Password={Password}";
            return connectionString;
        }
    }
}
