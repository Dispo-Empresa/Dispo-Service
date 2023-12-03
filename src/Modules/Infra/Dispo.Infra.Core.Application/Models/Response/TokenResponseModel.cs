namespace Dispo.Infra.Core.Application.Models.Response
{
    public class TokenInfoDto
    {
        public string Token { get; set; }
        public DateTime? TokenExpirationTime { get; set; }
    }
}