using HotelWebApp.Interfaces;

namespace HotelWebApp.Services.Dao
{
    public class DbContext
    {
        public ICustomerDao Customer { get; set; }
        public IReservationDao Reservation { get; set; }
        public IReservationService ReservationService { get; set; }
        public IRoomDao Room { get; set; }
        public IServiceDao Service { get; set; }

        public DbContext(ICustomerDao customerDao, IReservationDao reservationDao, IReservationService reservationServiceDao, IRoomDao roomDao, IServiceDao serviceDao) 
        {
            Customer = customerDao;
            Reservation = reservationDao;
            ReservationService = reservationServiceDao;
            Room = roomDao;
            Service = serviceDao;
        }
    }
}
