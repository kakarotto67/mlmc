using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Mlmc.EnterpriseServiceBus.RabbitMq.MessageBus;
using Mlmc.Operation.MongoDb;
using RabbitMQ.Client;
using System;

namespace Mlmc.Operation
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
            // Setup Database
            services.Configure<OperationDatabaseSettings>(
                    Configuration.GetSection(nameof(OperationDatabaseSettings)));
            services.AddSingleton<IOperationDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<OperationDatabaseSettings>>().Value);

            services.AddScoped<IUnitOfWork, OperationDatabaseContext>();
            services.AddScoped<MissileService>();
            services.AddScoped<DeploymentPlatformService>();

            // Setup RabbitMQ client
            var connectionFactory = new ConnectionFactory
            {
                HostName = Configuration.GetValue<String>("MessageBusConfiguration:HostName")
            };
            services.AddSingleton<IConnectionFactory>(sp => connectionFactory);

            // Setup Message Bus
            services.AddScoped<MessageBus>();

            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Configure cross-origin requests (CORS) so consumers can access this API
            services.AddCors(x => x.AddPolicy("Mlmc.Operation.Missiles",
               builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

            services.AddCors(x => x.AddPolicy("Mlmc.Operation.DeploymentPlatforms",
               builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

            services.AddCors(x => x.AddPolicy("Mlmc.Operation.LaunchMissile",
               builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
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
            app.UseMvc();

            // Initialize Database with default data
            SeedDatabase(app);
        }

        private void SeedDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var dbSettings = services.GetService<IOperationDatabaseSettings>();
                var unitOfWork = services.GetService<IUnitOfWork>();

                OperationDatabaseSeedHelper.SeedDatabase(dbSettings, unitOfWork);
            }
        }
    }
}