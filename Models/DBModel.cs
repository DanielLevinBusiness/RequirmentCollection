using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;

namespace OpsRequirmentCollectionWeb.Models
{
    public class DBModel
    {
        public SqlConnection Connection { get; set; }
        public DBModel(string dbName)
        {
            var configuration = GetConfiguration();
            Connection = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection(dbName).Value);
        }

        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional:true, reloadOnChange : true);
            return builder.Build();
        }
    }
}
