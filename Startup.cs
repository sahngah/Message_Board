using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using userDashboard.Models;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace userDashboard
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
        string Server = "localhost";
        string Port = "8889"; // or 8889 on Mac
        string Database = "newUserDashboard";
        string UserId = "root";
        string Password = "root";
        string Connection = $"Server={Server};port={Port};database={Database};uid={UserId};pwd={Password};";
        services.AddDbContext<DataContext>(options => options.UseMySQL(Connection));
        services.AddMvc();
        services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    }
}
