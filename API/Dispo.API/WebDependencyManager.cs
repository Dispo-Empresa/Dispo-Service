using Dispo.Account.Core.Application.Services;
using Dispo.Account.Core.Application.Services.Interfaces;
using Dispo.Shared.Caching;
using Dispo.Shared.Caching.Interfaces;
using Dispo.Shared.Queue.Publishers;
using Dispo.Shared.Queue.Publishers.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Dispo.API
{
    public static class WebDependencyManager
    {
        public static void InjectModules(this IServiceCollection services)
        {
            Account.Infrastructure.IoC.Injector.InjectIoCServices(services);
            PurchaseOrder.Infrastructure.IoC.Injector.InjectIoCServices(services);
            Product.Infrastructure.IoC.Injector.InjectIoCServices(services);
            Movement.Infrastructure.IoC.Injector.InjectIoCServices(services);

            InjectGenerics(services);
        }

        private static void InjectGenerics(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped<ITokenGenerator, TokenGenerator>();
            serviceColletion.AddScoped<ICacheManager, CacheManager>();
            serviceColletion.AddScoped<IEmailSenderPublisher, EmailSenderPublisher>();
            serviceColletion.AddSingleton(MappingProfile.CreateMappingProfile().CreateMapper());
            serviceColletion.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceColletion.AddMemoryCache();
        }
    }
}
