using Dispo.Infra.Plugin.Hub.Mensager.DTOs;

namespace Dispo.Infra.Plugin.Hub.Mensager.Models
{
    public class EmailSenderRequestDto
    {
        public string Subject { get; set; }
        public string EmailTo { get; set; }
        public string Body { get; set; }
        public string Observation { get; set; }
    }
}