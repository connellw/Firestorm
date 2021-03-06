﻿using Firestorm.Endpoints;
using Firestorm.Host;
using Firestorm.Testing.Http;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Firestorm.AspNetCore2.IntegrationTests
{
    public class CorsStartup
    {
        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            
            services.Configure<EndpointConfiguration>(config =>
            {
                config.Response.ShowDeveloperErrors = true;
                //
            });
            
            services.AddFirestorm()
                .AddEndpoints(sp => sp.GetService<IOptions<EndpointConfiguration>>().Value)
                .AddStartResourceFactory(new IntegratedStartResourceFactory());
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod()
            );
            
            app.UseFirestorm();
        }
    }
}