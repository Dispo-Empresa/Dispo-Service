namespace Dispo.Infra.Core.Application.Models.Request
{
    public class ResetPasswordRequestModel
    {
        public long AccountId { get; set; }
        public string NewPassword { get; set; }
    }
}