using AzureSqlServerCrudOperation.Core;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(AzureSqlServerCrudOperation.Startup))]

namespace AzureSqlServerCrudOperation
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var cn = Environment.GetEnvironmentVariable("AzureSqlServerConnectionString");

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(cn);
            });
        }
    }
}
