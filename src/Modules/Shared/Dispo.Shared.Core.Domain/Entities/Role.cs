namespace Dispo.Shared.Core.Domain.Entities
{
    public class Role : EntityBase
    {
        public string Name { get; set; }
        public string Key { get; set; }

        public IList<Account> Accounts { get; set; }
    }
}