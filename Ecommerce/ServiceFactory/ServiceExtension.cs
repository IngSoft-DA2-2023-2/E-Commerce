using BusinessLogic;
using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using LogicInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace ServiceFactory
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<ISessionRepository, SessionRepository>();
            serviceCollection.AddScoped<IBrandRepository, BrandRepository>();
            serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
            serviceCollection.AddScoped<IColourRepository, ColourRepository>();
            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            serviceCollection.AddScoped<IPurchaseRepository, PurchaseRepository>();

            serviceCollection.AddScoped<IUserLogic, UserLogic>();
            serviceCollection.AddScoped<IProductLogic, ProductLogic>();
            serviceCollection.AddScoped<IPurchaseLogic, PurchaseLogic>();
            serviceCollection.AddScoped<ISessionLogic, SessionLogic>();
        }
        public static void AddConnectionString(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<DbContext, ECommerceContext>(o => o.UseSqlServer(connectionString));
        }
    }
}