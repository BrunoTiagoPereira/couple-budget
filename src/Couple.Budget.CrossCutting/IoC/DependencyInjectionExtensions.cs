using Couple.Budget.Core.Services;
using Couple.Budget.Core.Transaction;
using Couple.Budget.Domain.Budgets.Repositories;
using Couple.Budget.Domain.Users.Repositories;
using Couple.Budget.Infra;
using Couple.Budget.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Couple.Budget.CrossCutting.IoC
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IDatesManager, DatesManager>()
                ;

            return services;
        }

        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<DbContext, DatabaseContext>(builder =>
            {
                builder.UseSqlServer(configuration.GetConnectionString("SqlServer"), op => op.EnableRetryOnFailure(3));
            }, ServiceLifetime.Scoped);

            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IBudgetRepository, BudgetRepository>()
                ;

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddCoreServices()
                .AddDomainServices()
                .AddDataServices(configuration)
                ;

            return services;
        }
    }
}