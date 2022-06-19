using Lyralabs.OpenRA.PrivateServerUI.Services;

namespace Lyralabs.OpenRA.PrivateServerUI.Pages
{
    public class IndexViewModel : ComponentBase
    {
        public GameServerUserConfiguration Model { get; set; } = new();

        [Inject]
        public GameServerService GameServerService { get; set; }

        private int serverPort;
        protected bool wasServerCreated = false;

        public void CreateServer()
        {
            this.serverPort = this.GameServerService.StartNewInstance(new GameServerOptions()
            {
                Name = this.Model.Name,
                Password = this.Model.Password,
                AdvertiseOnline = this.Model.AdvertiseOnline,
                EnableSingleplayer = this.Model.EnableSingleplayer,
                Mod = this.Model.Mod
            });

            this.wasServerCreated = true;
        }
    }
}
