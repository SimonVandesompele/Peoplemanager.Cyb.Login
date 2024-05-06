namespace PeopleManager.RestApi.Settings
{
    public class JwtSettings
    {
        public string? Secret { get; set; }
        public TimeSpan? Expiry { get; set; }
    }
}
