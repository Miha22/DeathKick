using System;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace DeathKick
{
    public class DeathKick : RocketPlugin<Configuration>
    {
        public static DeathKick Instance;

        protected override void Load()
        {
            Instance = this;
            Rocket.Unturned.Events.UnturnedPlayerEvents.OnPlayerDeath += UnturnedPlayerEvents_OnPlayerDeath;
            Logger.Log($"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name} loaded!", ConsoleColor.Green);
        }

        private void UnturnedPlayerEvents_OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            UnturnedChat.Say(Translate("server_kick_message", player.CharacterName), GetColor(), true);
            Provider.kick(player.CSteamID, Translate("player_kick_message"));
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList {
                    { "player_kick_message", "You have been found and removed from the event!" },
                    { "server_kick_message", "Player {0} has been found and removed from the event!" }
                };
            }
        }

        protected override void Unload()
        {
            Rocket.Unturned.Events.UnturnedPlayerEvents.OnPlayerDeath -= UnturnedPlayerEvents_OnPlayerDeath;
        }

        private Color GetColor()
        {
            switch (Configuration.Instance.AnnounceColor.ToLower())
            {
                case "red":
                    return Color.red;
                case "white" :
                    return Color.white;
                case "yellow":
                    return Color.yellow;
                case "cyan":
                    return Color.cyan;
                case "magenta":
                    return Color.magenta;
                default :
                    return Color.green;
            }
        }
    }
}
