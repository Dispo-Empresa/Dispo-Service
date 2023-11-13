using Dispo.Account.Domain.Enums;

namespace Dispo.Account.Domain.Models
{
    public class LicenceModel
    {
        public string Key { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public eLicenceType Type { get; set; }
    }
}