using Akka.Actor;
using Game.ActorModel.Actors;
using Game.ActorModel.ExternalSystems;
using System.Threading.Tasks;

namespace Game.Web.Models
{
    public static class GameActorSystem
    {
        private static ActorSystem ActorSystem;
        private static IGameEventsPusher GameEventsPusher;

        public static void Create()
        {
            GameEventsPusher = new SignalRGameEventsPusher();
            ActorSystem = ActorSystem.Create("GameSystem");
            ActorReferences.GameController = ActorSystem.ActorOf<GameControllerActor>();
            ActorReferences.SignalRBridge = ActorSystem.ActorOf(Props.Create(() => new SignalRBridgeActor(GameEventsPusher, ActorReferences.GameController)), "SignalRBridge");
        }

        public static void Shutdown()
        {
            Task.WaitAll(ActorSystem.Terminate());
        }

        public static class ActorReferences
        {
            public static IActorRef GameController { get; set; }
            public static IActorRef SignalRBridge { get; set; }
        }
    }
}
