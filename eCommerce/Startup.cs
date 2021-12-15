using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eCommerce.SelfService.Services;
using eCommerce.SelfService.Data;

namespace eCommerce
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

            services.AddControllers();
            services.AddSingleton<SelfServeService>((service) =>
            {
                var options = new DbContextOptionsBuilder<SelfServiceDbContext>();
                options.UseSqlServer(Configuration.GetConnectionString("AzureDB"));
                var ctx = new SelfServiceDbContext(options.Options);
                SQLServerSelfServiceRepository repo = new SQLServerSelfServiceRepository(ctx);
                return new SelfServeService(repo);
                
                // For in memory
                //return new SelfServeService(new InMemorySelfServiceRepository());
            });
            //services.AddDbContext<SelfService.Data.SelfServiceDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CapstoneDB")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eCommerce", Version = "v1" });
            });
        }

      

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // force to always use Swagger
            if (true)
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "eCommerce v1"));
            }
   
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
