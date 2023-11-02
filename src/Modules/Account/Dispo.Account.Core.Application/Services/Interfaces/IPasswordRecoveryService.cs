using Dispo.Shared.Core.Domain.DTOs.Plugin;

namespace Dispo.Account.Core.Application.Services.Interfaces
{
    public interface IPasswordRecoveryService
    {
        void SendRecoveryToken(string emailTo);

        void ValidateInputedToken(VerifyEmailCodeRequestDto verifyEmailCodeRequestDto);

        EmailSenderRequestDto CreateRecoveryTokenRequest(string emailTo);
    }
}