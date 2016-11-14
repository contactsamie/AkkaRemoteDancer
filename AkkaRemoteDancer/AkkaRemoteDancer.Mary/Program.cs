using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaRemoteDancer.MyMessages;
using NLog.Internal;

namespace AkkaRemoteDancer.Mary
{
   public class Program
    {
        static void Main(string[] args)
        {
            var actorSystem = ActorSystem.Create("Mary");
            {
                ReservationActor = actorSystem.ActorOf(Props.Create(() => new MaryStoreActor()),typeof(MaryStoreActor).Name);
                Console.ReadLine();
            }
        }

        public static IActorRef ReservationActor { get; set; }
    }
}
