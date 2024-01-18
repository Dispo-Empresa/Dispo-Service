using Dispo.Infra.Core.Application.Interfaces;
using Dispo.Infra.Core.Application.Services;
using Dispo.Product.Core.Application.Interfaces;
using Dispo.Product.Core.Application.Services;
using Dispo.Product.Infrastructure.Persistence.Repositories;
using Dispo.Shared.Core.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dispo.Product.Infrastructure.IoC
{
    public class Injector
    {
        public static void InjectIoCServices(IServiceCollection serviceColletion)
        {
            InjectRepositories(serviceColletion);
            InjectServices(serviceColletion);
        }

        private static void InjectRepositories(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped<IProductRepository, ProductRepository>();
            serviceColletion.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            serviceColletion.AddScoped<IWarehouseRepository, WarehouseRepository>();
        }

        private static void InjectServices(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped<IAdmService, AdmService>();
            serviceColletion.AddScoped<IProductService, ProductService>();
            serviceColletion.AddScoped<IManufacturerService, ManufacturerService>();
            serviceColletion.AddScoped<IWarehouseService, WarehouseService>();
        }
    }
}