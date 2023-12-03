using Dispo.Infra.Plugin.Hub.Mensager.Models;

namespace Dispo.Infra.Core.Application.Interfaces
{
    public interface IPasswordRecoveryService
    {
        void SendRecoveryToken(string emailTo);

        void ValidateInputedToken(VerifyEmailCodeRequestDto verifyEmailCodeRequestDto);

        EmailSenderRequestDto CreateRecoveryTokenRequest(string emailTo, string recoveryToken);
    }
}