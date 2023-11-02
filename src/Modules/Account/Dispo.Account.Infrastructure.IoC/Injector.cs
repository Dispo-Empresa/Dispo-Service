using Dispo.Account.Core.Application.Services;
using Dispo.Account.Core.Application.Services.Interfaces;
using Dispo.Account.Infrastructure.Persistence.Repositories;
using Dispo.Shared.Core.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dispo.Account.Infrastructure.IoC
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
            serviceColletion.AddScoped<IAccountRepository, AccountRepository>();
            serviceColletion.AddScoped<IRoleRepository, RoleRepository>();
            serviceColletion.AddScoped<IUserRepository, UserRepository>();
            serviceColletion.AddScoped<IWarehouseAccountRepository, WarehouseAccountRepository>();
        }

        private static void InjectServices(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped<IAccountService, AccountService>();
            serviceColletion.AddScoped<IAccountResolverService, AccountResolverService>();
        }
    }
}