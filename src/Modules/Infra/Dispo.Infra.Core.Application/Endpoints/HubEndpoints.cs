namespace Dispo.Infra.Core.Application.Endpoints
{
    public class HubEndpoints
    {
        private const string _baseUrl = "http://localhost:5191/api/v1/licences";

        public static string GetLicence = _baseUrl;
    }
}