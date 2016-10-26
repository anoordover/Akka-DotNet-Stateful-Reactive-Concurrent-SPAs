using Akka.Actor;
using Game.ActorModel.Actors;
using Game.ActorModel.ExternalSystems;
using System;
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

            ActorReferences.GameController =
                ActorSystem.ActorSelection("akka.tcp://GameSystem@127.0.0.1:8091/user/GameController")
                .ResolveOne(TimeSpan.FromSeconds(3))
                .Result;

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
