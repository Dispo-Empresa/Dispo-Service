namespace Dispo.Infra.Plugin.Hub.Mensager.Models
{
    public class VerifyEmailCodeRequestDto
    {
        public string Email { get; set; }
        public string InputedToken { get; set; }
    }
}