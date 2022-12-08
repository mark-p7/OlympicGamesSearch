using System;
using AzureFunc.Data;
using AzureFunc.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzureFunc.StartUp))]
namespace AzureFunc
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<OlympicsContext>(options =>
                options.UseMySql(Helper.GetSetting("MYSQL_DATABASE_CONNECTION_STRING"), new MySqlServerVersion(new Version(8, 0, 29))));
                // options.UseSqlServer(Helper.GetSetting("SQL-SERVER_DATABASE_CONNECTION_STRING")));
        }
    }
}
