using Dispo.Shared.Core.Domain.DTOs;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence;
using Dispo.Shared.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Dispo.Movement.Infrastructure.Persistence.Repositories
{
    public class MovementRepository : BaseRepository<Shared.Core.Domain.Entities.Movement>, IMovementRepository
    {
        private readonly DispoContext _context;

        public MovementRepository(DispoContext dispoContext) : base(dispoContext) 
        {
            _context = dispoContext;
        }

        public List<MovimentationDetailsDto> GetDetails()
            => _context.Movements.Include(x => x.BatchMovements)
                                 .ThenInclude(x => x.Batch)
                                 .ThenInclude(x => x.Order)
                                 .ThenInclude(x => x.Product)
                                 .Select(s => new MovimentationDetailsDto()
                                 {
                                     Id = s.Id,
                                     Type = s.Type,
                                     ProductName = s.BatchMovements.Where(x => x.MovementId == s.Id).First().Batch.Order.Product.Name,
                                     Date = s.Date,
                                     Quantity = s.Quantity
                                 })
                                .ToList();
    }
}