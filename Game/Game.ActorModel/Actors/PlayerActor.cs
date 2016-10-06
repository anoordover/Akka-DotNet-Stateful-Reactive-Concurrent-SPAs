using System;
using System.Threading.Tasks;
using Akka.Actor;
using Game.ActorModel.Messages;

namespace Game.ActorModel.Actors
{
    public class PlayerActor : ReceiveActor
    {
        private readonly string _playerName;
        private int _health;

        public PlayerActor(string playerName)
        {
            _playerName = playerName;
            _health = 100;

            Receive<AttackPlayerMessage>(m => HandleAttackPlayer(m));
            Receive<RefreshPlayerStatusMessage>(m => HandleRefreshPlayerStatus(m));
        }

        private void HandleRefreshPlayerStatus(RefreshPlayerStatusMessage m)
        {
            Sender.Tell(new PlayerStatusMessage(_playerName, _health));
        }

        private void HandleAttackPlayer(AttackPlayerMessage m)
        {
            _health -= 20;
            Sender.Tell(new PlayerHealthChangedMessage(_playerName, _health));
        }
    }
}
