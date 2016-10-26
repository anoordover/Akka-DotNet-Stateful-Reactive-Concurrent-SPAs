using Game.ActorModel.ExternalSystems;
using Microsoft.AspNet.SignalR;

namespace Game.Web.Models
{
    public class SignalRGameEventsPusher : IGameEventsPusher
    {
        private static readonly IHubContext _gameHubContext;

        static SignalRGameEventsPusher()
        {
            _gameHubContext = GlobalHost.ConnectionManager.GetHubContext<GameHub>();
        }

        public void PlayerJoined(string playerName, int playerHealth)
        {
            _gameHubContext.Clients.All.playerJoined(playerName, playerHealth);
        }

        public void UpdatePlayerHealth(string playerName, int playerHealth)
        {
            _gameHubContext.Clients.All.updatePlayerHealth(playerName, playerHealth);
        }
    }
}