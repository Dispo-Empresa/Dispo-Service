namespace Dispo.Shared.Core.Domain.Models
{
    public class ResponseModel
    {
        public object? Data { get; set; } = null;
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
    }
}