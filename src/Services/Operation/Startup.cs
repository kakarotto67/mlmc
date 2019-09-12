using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Operation.MongoDb;

namespace Operation
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Configure cross-origin requests (CORS) so consumers can access this API
            services.AddCors(x => x.AddPolicy("Operation.Missiles",
               builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

            services.AddCors(x => x.AddPolicy("Operation.DeploymentPlatforms",
               builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
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

            // Initialize Database with default data
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