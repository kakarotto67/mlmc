using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mlmc.EnterpriseServiceBus.RabbitMq.MessageBus;
using Mlmc.MGCC.Api.ChipSimulation;
using Mlmc.MGCC.Api.MessageBusConsumer;
using Mlmc.MGCC.Api.RealTime;
using RabbitMQ.Client;
using Microsoft.OpenApi.Models;

namespace Mlmc.MGCC.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup RabbitMQ client
            var connectionFactory = new ConnectionFactory
            {
                HostName = Configuration.GetValue<String>("MessageBusConfiguration:HostName")
            };
            services.AddSingleton<IConnectionFactory>(sp => connectionFactory);

            // Setup Message Bus
            services.AddSingleton<MessageBus>();

            // Setup Mgcc
            services.AddSingleton<MgccSimulator>();

            // Setup long running message bus consumer
            services.AddHostedService<ConsumeMessageBusHostedService>();

            services.AddHttpClient();

            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Configure Swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v0", new OpenApiInfo
                {
                    Title = "MLMC MGCC API",
                    Version = "v0",
                    Description = "MGCC service API of MLMC app.",
                });
            });

            // Configure cross - origin requests (CORS) so consumers can access this API
            services.AddCors(options => options.AddPolicy("Mlmc.Mgcc.Api",
            builder =>
            {
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       .AllowCredentials();
            }));

            // Add SignalR
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCors("Mlmc.Mgcc.Api");

            app.UseRouting();

            // Use SignalR
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MissileStatusHub>(MissileStatusHub.MissileStatusHubUri);
            });

            app.UseMvc();

            // Use Swagger
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v0/swagger.json", "MGCC API v0");
            });
        }
    }
}