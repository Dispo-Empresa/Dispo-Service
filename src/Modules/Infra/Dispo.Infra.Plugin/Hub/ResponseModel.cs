namespace Dispo.Infra.Plugin.Hub
{
    public class ResponseModel
    {
        public object? Data { get; set; } = null;
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
    }
}