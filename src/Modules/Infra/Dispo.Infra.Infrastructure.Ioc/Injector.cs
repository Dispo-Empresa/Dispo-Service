﻿using Dispo.Infra.Core.Application.Interfaces;
using Dispo.Infra.Core.Application.Services;
using Dispo.Infra.Infrastructure.Persistence.Repositories;
using Dispo.Shared.Core.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dispo.Infra.Infrastructure.Ioc
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
            serviceColletion.AddScoped<IPasswordRecoveryService, PasswordRecoveryService>();
            serviceColletion.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            serviceColletion.AddScoped<IUserAccountService, UserAccountService>();
        }
    }
}