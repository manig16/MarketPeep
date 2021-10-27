using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MarketPeep.Controllers.CoinGecko;
using MarketPeep.RestProviders.CoinGecko;
using MarketPeep.Services.CoinGecko;

namespace CoinGecko
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
            services.AddScoped<ICoinGeckoService, CoinGeckoService>();
            services.AddScoped<ICoinGeckoRestProvider, CoinGeckoRestProvider>();
            services.AddScoped<ICoinGeckoController, CoinGeckoController>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoinGecko", Version = "v1" });
            });

            AddCoinGeckoClient(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoinGecko v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddCoinGeckoClient(IServiceCollection services) {
            services.AddHttpClient("CoinGecko", c => {
                c.BaseAddress = new Uri(Configuration["CoinGecko_API_URL"]);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        }
    }
}
