using Dispo.Infra.Domain.Enums;

namespace Dispo.Infra.Domain.Models
{
    public class LicenceModel
    {
        public string Key { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public eLicenceType Type { get; set; }
    }
}