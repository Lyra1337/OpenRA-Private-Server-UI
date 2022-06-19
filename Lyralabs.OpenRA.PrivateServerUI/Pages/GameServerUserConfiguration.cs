using Lyralabs.OpenRA.PrivateServerUI.Services;
using System.ComponentModel.DataAnnotations;

namespace Lyralabs.OpenRA.PrivateServerUI.Pages
{
    public class GameServerUserConfiguration
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public OpenRAMod Mod { get; set; } = OpenRAMod.ra;

        public bool AdvertiseOnline { get; set; } = true;

        public bool EnableSingleplayer { get; set; } = true;
    }
}
