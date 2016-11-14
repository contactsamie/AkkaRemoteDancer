namespace AkkaRemoteDancer.MyMessages
{
    public class ReserveCompletedMessage
    {
        public ReserveCompletedMessage(string reservationId)
        {
            ReservationId = reservationId;
        }

        public string ReservationId { get; }
    }
}