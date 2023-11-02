using Dispo.Product.Core.Application.Services;
using Dispo.Product.Core.Application.Services.Interfaces;
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
            serviceColletion.AddScoped<IAddressRepository, AddressRepository>();
        }

        private static void InjectServices(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped<IProductService, ProductService>();
            serviceColletion.AddScoped<IManufacturerService, ManufacturerService>();
            serviceColletion.AddScoped<IAddressService, AddressService>();
            serviceColletion.AddScoped<IWarehouseService, WarehouseService>();
        }
    }
}