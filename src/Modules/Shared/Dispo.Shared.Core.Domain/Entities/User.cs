namespace Dispo.Shared.Core.Domain.Entities
{
    public class User : EntityBase
    {
        public bool Ativo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }

        public Account Account { get; set; }

        public string GetFullName()
            => $"{FirstName} {LastName}";
    }
}