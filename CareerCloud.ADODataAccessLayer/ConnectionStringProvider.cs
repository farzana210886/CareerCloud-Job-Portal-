using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public static class ConnectionStringProvider
    {
        private static readonly IConfiguration _configuration;
        static ConnectionStringProvider()
        {
            var builder = new ConfigurationBuilder()
                         .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                         .AddJsonFile("appsettings.json");
            _configuration = builder.Build();
        }

        public static string GetConnectionString() => _configuration.GetConnectionString("DataConnection");
    }
}
