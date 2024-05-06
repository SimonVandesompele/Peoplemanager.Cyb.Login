using Vives.Services.Model.Enums;

namespace Vives.Services.Model
{
    public class ServiceMessage
    {
        public required string Code { get; set; }
        public required string Message { get; set; }
        public ServiceMessageType Type { get; set; }
    }
}
