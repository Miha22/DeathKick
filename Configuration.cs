using Rocket.API;

namespace DeathKick
{
    public class Configuration : IRocketPluginConfiguration
    {
        public string AnnounceColor;

        public void LoadDefaults()
        {
            AnnounceColor = "Green";
        }
    }
}