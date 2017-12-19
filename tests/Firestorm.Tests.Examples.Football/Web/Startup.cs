using Firestorm.Endpoints;
using Firestorm.Endpoints.AspNetCore;
using Firestorm.Endpoints.AspNetCore.Middleware;
using Firestorm.Endpoints.Responses;
using Firestorm.Extensions.AspNetCore;
using Firestorm.Tests.Examples.Football.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Firestorm.Tests.Examples.Football.Web
{
    public class Startup
    {
        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<FootballDbContext>(builder =>
                {
                    const string connection = @"Server=(localdb)\mssqllocaldb;Database=Firestorm.Tests.Examples.Football;Trusted_Connection=True;ConnectRetryCount=0";
                    builder.UseSqlServer(connection);
                });

            services.AddFirestorm()
                .AddEntityFramework<FootballDbContext>()
                .AddFluent<FootballApiContext>()
                //.AddStems()
                ;
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var dbContext = app.ApplicationServices.GetService<FootballDbContext>())
            {
                dbContext.Database.EnsureCreated();
            }

            app.UseFirestorm(new RestEndpointConfiguration
            {
                ResponseConfiguration =
                {
                    StatusField = ResponseStatusField.StatusCode,
                    PageConfiguration =
                    {
                        UseLinkHeaders = true
                    }
                }
            });
        }
    }
}