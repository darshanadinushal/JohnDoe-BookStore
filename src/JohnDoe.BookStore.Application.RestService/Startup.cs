using JohnDoe.BookStore.Application.Core.service.Repository;
using JohnDoe.BookStore.Application.Manager.Manager;
using JohnDoe.BookStore.Application.Shared.Contract.Manager;
using JohnDoe.BookStore.Application.Shared.Contract.Repository;
using JohnDoe.BookStore.Application.Shared.Infra.MapperProfile;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JohnDoe.BookStore.Application.RestService
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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("*");
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddControllers();
            services.AddAutoMapper(typeof(BookProfile));

            services.AddScoped<IBookStoreManager, BookStoreManager>();
            services.AddScoped<IBookOrderManager, BookOrderManager>();

            services.AddScoped<IBookStoreRepository, BookStoreRepository>();
            services.AddScoped<IBookOrderRepository, BookOrderRepository>();
           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
