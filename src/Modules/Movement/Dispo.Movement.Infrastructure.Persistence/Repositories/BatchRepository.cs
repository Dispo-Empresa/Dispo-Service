using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence;
using Dispo.Shared.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Dispo.Movement.Infrastructure.Persistence.Repositories
{
    public class BatchRepository : BaseRepository<Batch>, IBatchRepository
    {
        private readonly DispoContext _context;

        public BatchRepository(DispoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByKeyAsync(string key)
        {
            return await _context.Batches.AnyAsync(w => w.Key == key);
        }

        public List<BatchDetailsDto> GetWithQuantityByProduct(long productId)
        {
            return _context.Batches.Include(x => x.Order)
                                   .Where(w => w.Order.ProductId == productId)
                                   .Select(s => new BatchDetailsDto
                                   {
                                       Id = s.Id,
                                       Key = s.Key,
                                       Quantity = s.ProductQuantity,
                                       ExpirationDate = s.ExpirationDate,
                                       ManufacturingDate = s.ManufacturingDate
                                   }).ToList();
        }

        public async Task<Batch?> GetByKeyAsync(string key)
        {
            return await _context.Batches.FirstOrDefaultAsync(w => w.Key == key);
        }

        public int GetTotalQuantityOfProduct(long productId)
            => _context.Batches.Include(x => x.Order)
                               .ThenInclude(x => x.Product)
                               .Where(x => x.Order.ProductId == productId)
                               .Sum(s => s.ProductQuantity);
    }
}