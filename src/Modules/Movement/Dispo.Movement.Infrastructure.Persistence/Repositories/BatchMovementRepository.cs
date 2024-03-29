﻿using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence;
using Dispo.Shared.Infrastructure.Persistence.Context;

namespace Dispo.Movement.Infrastructure.Persistence.Repositories
{
    public class BatchMovementRepository : BaseRepository<BatchMovement>, IBatchMovementRepository
    {
        private readonly DispoContext _dispoContext;

        public BatchMovementRepository(DispoContext dispoContext) : base(dispoContext)
        {
            _dispoContext = dispoContext;
        }
    }
}