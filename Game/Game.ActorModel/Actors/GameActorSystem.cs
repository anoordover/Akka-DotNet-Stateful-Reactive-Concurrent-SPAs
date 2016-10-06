using Akka.Actor;
using System.Threading.Tasks;

namespace Game.ActorModel.Actors
{
    public static class GameActorSystem
    {
        private static ActorSystem ActorSystem;

        public static void Create()
        {
            ActorSystem = ActorSystem.Create("GameSystem");
            ActorReferences.GameController = ActorSystem.ActorOf<GameControllerActor>();
        }

        public static void Shutdown()
        {
            Task.WaitAll(ActorSystem.Terminate());
        }

        public static class ActorReferences
        {
            public static IActorRef GameController { get; set; }
        }
    }
}
