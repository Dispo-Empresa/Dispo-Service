namespace Dispo.Infra.Core.Application.Models.Request
{
    public class CreateEmployeeRequestModel
    {
        public string Email { get; set; }
        public long RoleId { get; set; }
        public IList<long> WarehousesId { get; set; }
    }
}