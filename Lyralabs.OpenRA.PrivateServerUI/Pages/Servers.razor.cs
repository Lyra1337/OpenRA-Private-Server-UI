using CommunityToolkit.Mvvm.Messaging;
using Lyralabs.OpenRA.PrivateServerUI.Services;

namespace Lyralabs.OpenRA.PrivateServerUI.Pages
{
    public class ServersViewModel : ComponentBase, IRecipient<ServerUpdateMessage>
    {
        [Inject]
        public GameServerService GameServerService { get; set; }

        [Inject]
        public AppSettings AppSettings { get; set; }

        [Inject]
        public IMessenger Messenger { get; set; }

        protected override void OnInitialized()
        {
            this.Messenger.Register(this);
        }

        public void Receive(ServerUpdateMessage message)
        {
            _ = this.InvokeAsync(() => this.StateHasChanged());
        }
    }
}
