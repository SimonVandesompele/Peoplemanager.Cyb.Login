using Vives.Services.Model;

namespace Vives.Security.Model
{
    public class JwtAuthenticationResult: ServiceResult
    {
        public string? Token { get; set; }
    }
}
