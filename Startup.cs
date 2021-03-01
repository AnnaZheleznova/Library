using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;

namespace Library
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string conHuman = "Server=(localdb)\\mssqllocaldb;Database=humandbstore;Trusted_Connection=True;";
            string conBook = "Server=(localdb)\\mssqllocaldb;Database=bookdbstore;Trusted_Connection=True;";
            string conBook_Human = "Server=(localdb)\\mssqllocaldb;Database=book_humandbstore;Trusted_Connection=True;";

            // устанавливаем контекст данных
            services.AddDbContext<HumanContext>(options => options.UseSqlServer(conHuman));
            services.AddDbContext<BookContext>(options => options.UseSqlServer(conBook));
            services.AddDbContext<Book_HumanContext>(options => options.UseSqlServer(conBook_Human));


            services.AddControllers(); // используем контроллеры без представлений
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
            });
        }
    }
}
