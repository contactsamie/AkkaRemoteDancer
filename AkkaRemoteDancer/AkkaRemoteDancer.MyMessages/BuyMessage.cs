namespace AkkaRemoteDancer.MyMessages
{
    public class BuyMessage
    {
        public BuyMessage(string reservationId)
        {
            ReservationId = reservationId;
        }
        public string ReservationId { get; }
    }
}