namespace Dispo.Shared.Core.Domain.Exceptions
{
    public class PasswordRecoveryException : Exception
    {
        public PasswordRecoveryException(string message)
            : base(message)
        { }
    }
}