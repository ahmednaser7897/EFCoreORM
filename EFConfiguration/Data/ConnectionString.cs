using Microsoft.Extensions.Configuration;

namespace EFConfiguration.Data
{
    internal static class ConnectionString
    {
        public static string LoadConnectionStringV1()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            var connectionString = configuration.GetSection("connectStringsv1").Value;

            return connectionString ?? "";
        }

        public static string LoadConnectionStringV2()
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            var connectionString = configuration.GetSection("connectStringsv2").Value;

            return connectionString ?? "";
        }

    }
}
