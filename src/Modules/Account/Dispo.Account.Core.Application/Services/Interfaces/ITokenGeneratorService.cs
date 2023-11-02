namespace Dispo.Account.Core.Application.Services.Interfaces
{
    public interface ITokenGeneratorService
    {
        string GenerateRecoveryTokenNumbers();

        string GenerateRecoveryTokenLetters();
    }
}