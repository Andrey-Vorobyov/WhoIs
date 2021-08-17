using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WhoIs.Infrastructure;
using WhoIs.Infrastructure.Repositories;
using WhoIs.TcpListeners;

namespace WhoIs
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "WhoIs API", Version = "v1"}); });
            services.AddHealthChecks();

            services.AddHostedService<DomainTcpListener>();

            services.AddScoped<IDomainRepository, DomainRepository>();

            //Out of scope: sensitive info like db credentials should be retrieve from secret vault
            var connectionString = Configuration.GetConnectionString("Postgres");
            services.AddDbContext<DomainDbContext>(options => options.UseNpgsql(connectionString));
        }

       public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WhoIs v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHealthChecks("/status");
                });
        }
    }
}
