using Vives.Services.Model.Enums;

namespace Vives.Services.Model
{
    public class ServiceResult
    {
        public bool IsSuccessful => Messages.All(m => m.Type != ServiceMessageType.Error);

        public IList<ServiceMessage> Messages { get; set; } = new List<ServiceMessage>();
    }

    
}
