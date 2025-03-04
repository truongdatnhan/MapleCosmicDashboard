using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISqlDataAccess, SqlDataAccess>(_ => {
                var connectionString = configuration.GetConnectionString("Default");

                var dbDockerHost = configuration["DB_HOST"];
                var dbDockerPort = configuration["DB_PORT"] ?? "3306";
                var dbDockerDb = configuration["DB_DATABASE"];
                var dbDockerUser = configuration["DB_USER"];
                var dbDockerPass = configuration["DB_PASS"];

                if (dbDockerHost != null && dbDockerDb != null && dbDockerUser != null && dbDockerPass != null)
                {
                    connectionString = @$"server={dbDockerHost}; port={dbDockerPort}; database={dbDockerDb}; user={dbDockerUser}; password={dbDockerPass}";
                }

                return new SqlDataAccess(new MySqlConnection(connectionString));
            });
            return services;
        }
    }
}
