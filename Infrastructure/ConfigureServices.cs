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
            services.AddTransient<ISqlDataAccess, SqlDataAccess>(_ => new SqlDataAccess(new MySqlConnection(configuration.GetConnectionString("Default"))));
            return services;
        }
    }
}
