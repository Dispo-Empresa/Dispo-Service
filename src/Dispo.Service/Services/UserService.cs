using Dispo.Domain.Entities;
using Dispo.Domain.Exceptions;
using Dispo.Infrastructure.Repositories.Interfaces;
using Dispo.Service.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Dispo.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMemoryCache memoryCache;

        public UserService(IUserRepository userRepository, IMemoryCache memoryCache)
        {
            this.userRepository = userRepository;
            this.memoryCache = memoryCache;
        }

        public User? GetById(long id)
        {
            return userRepository.GetById(id);
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await memoryCache.GetOrCreateAsync(id, async entry =>
            {
                entry.AbsoluteExpiration = DateTime.UtcNow.AddMinutes(10);
                return await userRepository.GetByIdAsync(id);
            });
        }

        /// <summary>
        /// Link one or more warehouses to an user.
        /// </summary>
        /// <param name="warehouseIds"></param>
        /// <param name="userId"></param>
        /// <exception cref="NotFoundException"></exception>
        public void LinkWarehouses(List<long> warehouseIds, long userId)
        {
            var user = userRepository.GetWithWarehousesById(userId) ?? throw new NotFoundException("Esse usuário não existe.");
            foreach (var warehouseId in warehouseIds)
            {
                if (user.UserWarehouses.Any(w => w.WarehouseId == warehouseId && w.UserId == userId))
                    continue;

                user.UserWarehouses.Add(new UserWarehouse(warehouseId, userId));
            }

            userRepository.Update(user);
        }

        /// <summary>
        /// Change User current Warehouse.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="warehouseId"></param>
        public void ChangeWarehouse(long userId, long warehouseId)
        {
            var user = userRepository.GetById(userId) ?? throw new NotFoundException("Esse usuário não existe.");
            user.CurrentWarehouseId = warehouseId;
            userRepository.Update(user);
        }
    }
}