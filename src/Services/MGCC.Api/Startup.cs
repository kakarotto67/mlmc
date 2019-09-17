using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mlmc.EnterpriseServiceBus.RabbitMq.MessageBus;
using Mlmc.MGCC.Api.ChipSimulation;
using Mlmc.MGCC.Api.MessageBusConsumer;
using Mlmc.MGCC.Api.RealTime;
using RabbitMQ.Client;

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Add SignalR
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseMvc();

            // Use SignalR
            app.UseSignalR(routes =>
            {
                routes.MapHub<MissileStatusHub>(MissileStatusHub.MissileStatusHubUri);
            });
        }
    }
}