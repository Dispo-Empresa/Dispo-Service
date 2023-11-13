namespace Dispo.Shared.Core.Domain.Endpoints
{
    public class HubEndpoints
    {
        private const string _baseUrl = "https://localhost:7265/api/v1/licences";

        public static string GetLicence = _baseUrl;
    }
}