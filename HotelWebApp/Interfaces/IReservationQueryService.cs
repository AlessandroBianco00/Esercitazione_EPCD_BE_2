namespace HotelWebApp.Interfaces
{
    public interface IReservationQueryService
    {
        List<IReservationDao> GetReservationByFC(string fc);
        List<IReservationDao> GetReservationFullBoard(int roomtype);
    }
}
