namespace Dispo.Infra.Core.Application.Interfaces
{
    public interface ITokenGeneratorService
    {
        string GenerateRecoveryTokenNumbers();

        string GenerateRecoveryTokenLetters();
    }
}