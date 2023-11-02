using Dispo.Shared.Core.Domain.Entities;

namespace Dispo.Shared.Core.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        bool ExistsByCpfCnpj(string cpfCnpj);
    }
}