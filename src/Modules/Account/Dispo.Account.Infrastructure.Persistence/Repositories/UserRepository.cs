using Dispo.Shared.Core.Domain.Entities;
using Dispo.Shared.Core.Domain.Interfaces;
using Dispo.Shared.Infrastructure.Persistence.Context;
using Dispo.Shared.Infrastructure.Persistence.Repositories;
using System.Linq.Expressions;

namespace Dispo.Account.Infrastructure.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DispoContext _dispoContext;

        public UserRepository(DispoContext dispoContext) : base(dispoContext)
        {
            _dispoContext = dispoContext;
        }

        private Expression<Func<User, bool>> ExpExistsByCpfCnpj(string cpfCnpj)
            => w => w.Cpf.Equals(cpfCnpj);

        public bool ExistsByCpfCnpj(string cpfCnpj)
            => _dispoContext.Users
                            .Any(ExpExistsByCpfCnpj(cpfCnpj));
    }
}