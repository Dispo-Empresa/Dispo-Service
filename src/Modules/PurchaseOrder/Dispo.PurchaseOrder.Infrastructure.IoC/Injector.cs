using Dispo.PurchaseOrder.Core.Application.Services;
using Dispo.PurchaseOrder.Core.Application.Services.Interfaces;
using Dispo.PurchaseOrder.Infrastructure.Persistence.Repositories;
using Dispo.Shared.Core.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dispo.PurchaseOrder.Infrastructure.IoC
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
            serviceColletion.AddScoped<ISupplierRepository, SupplierRepository>();
            serviceColletion.AddScoped<IPurchaseOrderAttachmentRepository, PurchaseOrderAttachmentRepository>();
            serviceColletion.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            serviceColletion.AddScoped<IOrderRepository, OrderRepository>();
            serviceColletion.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            serviceColletion.AddScoped<IPurchaseOrderAttachmentRepository, PurchaseOrderAttachmentRepository>();
            serviceColletion.AddScoped<ISupplierRepository, SupplierRepository>();
        }

        private static void InjectServices(IServiceCollection serviceColletion)
        {
            serviceColletion.AddScoped<ISupplierService, SupplierService>();
            serviceColletion.AddScoped<IPurchaseOrderAttachmentService, PurchaseOrderAttachmentService>();
            serviceColletion.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            serviceColletion.AddScoped<IOrderService, OrderService>();
        }
    }
}