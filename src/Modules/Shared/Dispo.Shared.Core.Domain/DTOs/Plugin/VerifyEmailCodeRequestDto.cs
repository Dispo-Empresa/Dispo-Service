namespace Dispo.Shared.Core.Domain.DTOs.Plugin
{
    public class VerifyEmailCodeRequestDto
    {
        public string Email { get; set; }
        public string InputedToken { get; set; }
    }
}