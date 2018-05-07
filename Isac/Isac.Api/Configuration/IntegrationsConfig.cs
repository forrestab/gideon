namespace Isac.Api.Configuration
{
    public class IntegrationsConfig
    {
        public ClientConfig<BaseUrlsConfig> Bitbucket { get; set; }
        public ClientConfig<CrucibleUrlsConfig> Crucible { get; set; }
        public ClientConfig<BaseUrlsConfig> FishEye { get; set; }
    }
}
