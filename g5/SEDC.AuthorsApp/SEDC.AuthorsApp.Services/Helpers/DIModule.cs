using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using SEDC.AuthorsApp.DataAccess;
using SEDC.AuthorsApp.Domain;

namespace SEDC.AuthorsApp.Services
{
    public class DIModule
    {
        public static IServiceCollection RegisterRepositories(IServiceCollection services, string connectionString)
        {
            //services.AddDbContext<PizzaDbContext>(x =>
            //x.UseSqlServer("Server=.\\SQLExpress;Database=PizzaDb;Trusted_Connection=True")
            //);
            services.AddTransient<IUnitOfWork, UnitOfWorkAdo>();
            services.AddTransient<IRepository<Author>, AuthorsAdoRepository>();
            services.AddTransient<IRepository<Novel>, NovelsAdoRepository>();
            services.AddTransient<IDbConnection>(x => new SqlConnection(connectionString));
            return services;
        }
    }
}
