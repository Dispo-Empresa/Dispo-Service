using Dispo.Shared.Core.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Dispo.Movement.Core.Application.Services.Interfaces;
using Dispo.Movement.Core.Application.Services;
using Dispo.Movement.Infrastructure.Persistence.Repositories;

namespace Dispo.Movement.Infrastructure.IoC
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
            serviceColletion.AddScoped<IMovementRepository, MovementRepository>();
            serviceColletion.AddScoped<IBatchMovementRepository, BatchMovementRepository>();
            serviceColletion.AddScoped<IBatchRepository, BatchRepository>();
        }

        private static void InjectServices(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped<IMovementService, MovementService>();
            serviceColletion.AddScoped<IBatchMovementService, BatchMovementService>();
            serviceColletion.AddScoped<IBatchService, BatchService>();
        }
    }
}