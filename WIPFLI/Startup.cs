using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using WIPFLI.Infrastructure.Repositories;
using WIPFLI.Infrastructure.Services;

namespace WIPFLI
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
            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                x.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddControllers(mvcOptions =>
                mvcOptions.EnableEndpointRouting = false);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Store APIs",
                    Version = "v1",
                    Description = "WIPFLI store service",
                });
            });

            services.AddSingleton<IDiscountRepository, DiscountRepository>();
            services.AddSingleton<IDiscountService, DiscountService>();
            services.AddSingleton<IItemService, ItemService>();
            services.AddSingleton<IItemRepository, ItemRepository>();
            services.AddTransient<IBillingService, BillingService>();
            services.AddSingleton<IItemDiscountService, Billing>();
            services.AddSingleton<IWeekDaysDiscountService, Billing>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Services"));
        }
    }
}
