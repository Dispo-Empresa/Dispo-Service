using Dispo.Infra.Core.Application.Interfaces;
using Dispo.Infra.Plugin.Hub.Mensager.Models;
using Dispo.Shared.Caching.Interfaces;
using Dispo.Shared.Core.Domain.Exceptions;
using Dispo.Shared.Queue.Publishers.Interfaces;
using Newtonsoft.Json;

namespace Dispo.Infra.Core.Application.Services
{
    public class PasswordRecoveryService : IPasswordRecoveryService
    {
        private readonly ICacheManager _cacheManager;
        private readonly ITokenGeneratorService _codeGeneratorService;
        private readonly IEmailSenderPublisher _emailSenderPublisher;

        public PasswordRecoveryService(ICacheManager cacheManager, ITokenGeneratorService codeGeneratorService, IEmailSenderPublisher emailSenderPublisher)
        {
            _cacheManager = cacheManager;
            _codeGeneratorService = codeGeneratorService;
            _emailSenderPublisher = emailSenderPublisher;
        }

        public void SendRecoveryToken(string emailTo)
        {
            var recoveryToken = _codeGeneratorService.GenerateRecoveryTokenNumbers();
            var request = CreateRecoveryTokenRequest(emailTo, recoveryToken);

            _cacheManager.Add(emailTo, recoveryToken, 300);

            var requestJson = JsonConvert.SerializeObject(request);

            _emailSenderPublisher.Publish(requestJson);
        }

        public void ValidateInputedToken(VerifyEmailCodeRequestDto verifyEmailCodeRequestDto)
        {
            var tokenInCache = _cacheManager.Get(verifyEmailCodeRequestDto.Email);

            if (string.IsNullOrEmpty(tokenInCache))
                throw new PasswordRecoveryException("O token informado está expirado, solicite um novo token.");

            if (!tokenInCache.Equals(verifyEmailCodeRequestDto.InputedToken))
                throw new PasswordRecoveryException("O token para recuperação de senha está incorreto.");

            _cacheManager.Remove(verifyEmailCodeRequestDto.Email);
        }

        public EmailSenderRequestDto CreateRecoveryTokenRequest(string emailTo, string recoveryToken)
        {
            var emailBody = $"Segue código para redefinição da senha: <b style=\"color: red\">{recoveryToken}</b>";
            var emailObservation = "Caso a redefinição de senha não tenha sido solicitada, desconsidere este email!";

            return new EmailSenderRequestDto
            {
                Subject = "Dispo - Redefinição de Senha",
                EmailTo = emailTo,
                Body = emailBody,
                Observation = emailObservation
            };
        }
    }
}