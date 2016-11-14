using Akka.Actor;
using Akka.Event;
using AkkaRemoteDancer.MyMessages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AkkaRemoteDancer.Mary
{
    public class MaryStoreActor : ReceiveActor
    {
      //  public readonly ILoggingAdapter Logger = Context.GetLogger();
        public Dictionary<string, Tuple<IActorRef, string>> Reservations { get; set; }

        public MaryStoreActor()
        {
            Reservations = new Dictionary<string, Tuple<IActorRef, string>>();
            Receive<ReserveMessage>(message =>
            {
             //   Logger.Debug("Reserving .......");
                //var reservationId = Guid.NewGuid().ToString();
                //var reservationActor = Context.ActorOf(Props.Create(() => new ReservationActor(message.ReservationQuantity, reservationId)));
                //Reservations.Add(reservationId, new Tuple<IActorRef, string>(reservationActor, reservationActor.Path.ToStringWithoutAddress()));
                ////Context.Watch(reservationActor);
                Sender.Tell(new ReserveCompletedMessage(Guid.NewGuid().ToString()));
            });
            Receive<BuyMessage>(message =>
            {
               // Logger.Debug("Buuying .......");
                //if (Reservations.ContainsKey(message.ReservationId))
                //    Reservations[message.ReservationId].Item1.Forward(message);
                //else
                //    Sender.Tell(new UnableToBuyMessage());

                Sender.Tell(new BuyCompletedMessage(message.ReservationId));
            });
            Receive<Terminated>(t =>
            {
                //var entry = Reservations.FirstOrDefault(x => x.Value.Item2 == t.ActorRef.Path.ToStringWithAddress());
                //Logger.Debug("Removing "+entry.Value.Item2);
                //if (!string.IsNullOrEmpty(entry.Key))
                //    Reservations.Remove(entry.Key);
            });
        }
    }
}