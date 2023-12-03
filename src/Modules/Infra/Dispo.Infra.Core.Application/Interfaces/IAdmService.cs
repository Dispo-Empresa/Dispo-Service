using Dispo.Infra.Core.Application.Models.Request;

namespace Dispo.Infra.Core.Application.Interfaces
{
    public interface IAdmService
    {
        void CreateEmployee(CreateEmployeeRequestModel createEmployeeRequestDto);
    }
}