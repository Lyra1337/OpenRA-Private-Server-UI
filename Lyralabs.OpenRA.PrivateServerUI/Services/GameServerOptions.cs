namespace Lyralabs.OpenRA.PrivateServerUI.Services
{
    public class GameServerOptions
    {
        public string Name { get; set; } = "Dedicated Server";
        public OpenRAMod Mod { get; set; } = OpenRAMod.ra;
        public int ListenPort { get; set; } = 1234;
        public bool AdvertiseOnline { get; set; } = true;
        public string Password { get; set; } = String.Empty;
        public bool RecordReplays { get; set; } = false;

        public bool RequireAuthentication { get; set; } = false;
        public string ProfileIDBlacklist { get; set; } = String.Empty;
        public string ProfileIDWhitelist { get; set; } = String.Empty;

        public bool EnableSingleplayer { get; set; } = false;
        public bool EnableSyncReports { get; set; } = false;
        public bool EnableGeoIP { get; set; } = true;
        public bool EnableLintChecks { get; set; } = true;
        public bool ShareAnonymizedIPs { get; set; } = true;

        public int JoinChatDelay { get; set; } = 5000;

        public string SupportDir { get; set; } = String.Empty;

        public TimeSpan RunDuration { get; set; } = TimeSpan.FromHours(8);
    }
}
