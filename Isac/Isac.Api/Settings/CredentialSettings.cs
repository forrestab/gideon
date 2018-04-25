namespace Isac.Api.Settings
{
    public class CredentialSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string AsBasicAuthorization()
        {
            return $"{this.UserName}:{this.Password}";
        }
    }
}
