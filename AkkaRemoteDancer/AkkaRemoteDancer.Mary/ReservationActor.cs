using System;
using Akka.Actor;
using Akka.Event;
using AkkaRemoteDancer.MyMessages;

namespace AkkaRemoteDancer.Mary
{
    public class ReservationActor : ReceiveActor
    {
        private readonly string _reservationId;
        private int _reservationQuantity;
        public readonly ILoggingAdapter Logger = Context.GetLogger();
        public ReservationActor(int reservationQuantity, string reservationId)
        {
            _reservationQuantity = reservationQuantity;
            _reservationId = reservationId;
            Receive<BuyMessage>(message =>
            {
                 Sender.Tell(new BuyCompletedMessage(message.ReservationId));
                 Context.Stop(Self);
            });
        }
    }
}