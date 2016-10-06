using Akka.Actor;
using Game.ActorModel.Messages;
using System.Collections.Generic;

namespace Game.ActorModel.Actors
{
    public class GameControllerActor : ReceiveActor
    {
        private readonly Dictionary<string, IActorRef> _players;

        public GameControllerActor()
        {
            _players = new Dictionary<string, IActorRef>();

            Receive<JoinGameMessage>(m => JoinGame(m));
            Receive<AttackPlayerMessage>(m => _players[m.PlayerName].Forward(m));
        }

        private void JoinGame(JoinGameMessage message)
        {
            var playerNeedsCreating = !_players.ContainsKey(message.PlayerName);

            if (playerNeedsCreating)
            {
                IActorRef newPlayerActor = Context.ActorOf(Props.Create(() => new PlayerActor(message.PlayerName)), message.PlayerName);
                _players.Add(message.PlayerName, newPlayerActor);

                foreach(var player in _players.Values)
                {
                    player.Tell(new RefreshPlayerStatusMessage(), Sender);
                }
            }
        }
    }
}
