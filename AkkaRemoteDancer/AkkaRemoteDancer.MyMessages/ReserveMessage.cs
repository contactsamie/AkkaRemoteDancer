namespace AkkaRemoteDancer.MyMessages
{
    public class ReserveMessage
    {
        public ReserveMessage(int reservationQuantity)
        {
            ReservationQuantity = reservationQuantity;
        }

        public int ReservationQuantity { get; }
    }
}