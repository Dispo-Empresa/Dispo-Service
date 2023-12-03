namespace Dispo.Shared.Core.Domain.Entities
{
    public class Manufacturer : EntityBase
    {
        public bool Ativo { get; set; }
        public string Name { get; set; }
        public byte[] Logo { get; set; }

        public IList<ProductManufacturer> ProductManufacturers { get; set; }
    }
}