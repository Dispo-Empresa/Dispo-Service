using Dispo.Domain.DTOs;
using Dispo.Domain.Entities;
using Dispo.Infrastructure.Context;
using Dispo.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dispo.Infrastructure.Repositories
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
            return _context.Batches.Where(w => w.ProductId == productId)
                                   .Select(s => new BatchDetailsDto
                                   {
                                       Id = s.ProductId,
                                       Key = s.Key,
                                       Quantity = s.ProductQuantity
                                   }).ToList();
        }
    }
}
