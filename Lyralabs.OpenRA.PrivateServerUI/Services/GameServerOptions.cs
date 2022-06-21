namespace Lyralabs.OpenRA.PrivateServerUI.Services
{
    public class GameServerOptions
    {
        [ParameterPrefix("Engine")]
        public string EngineDir { get; set; } = String.Empty;

        [ParameterPrefix("Server")]
        public string Name { get; set; } = "Dedicated Server";

        [ParameterPrefix("Game")]
        public OpenRAMod Mod { get; set; } = OpenRAMod.ra;

        [ParameterPrefix("Server")]
        public int ListenPort { get; set; } = 1234;

        [ParameterPrefix("Server")]
        public bool AdvertiseOnline { get; set; } = true;

        [ParameterPrefix("Server")]
        public string Password { get; set; } = String.Empty;

        [ParameterPrefix("Server")]
        public bool RecordReplays { get; set; } = false;

        [ParameterPrefix("Server")]
        public bool RequireAuthentication { get; set; } = false;

        [ParameterPrefix("Server")]
        public string ProfileIDBlacklist { get; set; } = String.Empty;

        [ParameterPrefix("Server")]
        public string ProfileIDWhitelist { get; set; } = String.Empty;

        [ParameterPrefix("Server")]
        public bool EnableSingleplayer { get; set; } = false;

        [ParameterPrefix("Server")]
        public bool EnableSyncReports { get; set; } = false;

        [ParameterPrefix("Server")]
        public bool EnableGeoIP { get; set; } = true;

        [ParameterPrefix("Server")]
        public bool EnableLintChecks { get; set; } = true;

        [ParameterPrefix("Server")]
        public bool ShareAnonymizedIPs { get; set; } = true;

        [ParameterPrefix("Server")]
        public int JoinChatDelay { get; set; } = 5000;

        [ParameterPrefix("Engine")]
        public string SupportDir { get; set; } = String.Empty;

        public TimeSpan RunDuration { get; set; } = TimeSpan.FromHours(8);
    }
}
