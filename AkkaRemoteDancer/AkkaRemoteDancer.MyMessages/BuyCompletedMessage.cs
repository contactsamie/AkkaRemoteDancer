namespace AkkaRemoteDancer.MyMessages
{
    public class BuyCompletedMessage
    {
        public BuyCompletedMessage(string reservationId)
        {
            ReservationId = reservationId;
        }

        public string ReservationId { get; }
    }
}