using HotelWebApp.Interfaces;

namespace HotelWebApp.Services.Dao
{
    public class DbContext
    {
        public ICustomerDao Customer { get; set; }
        public IReservationDao Reservation { get; set; }
        public IRoomDao Room { get; set; }
        public IServiceDao Service { get; set; }

        public DbContext(ICustomerDao customerDao, IReservationDao reservationDao, IRoomDao roomDao, IServiceDao serviceDao) 
        {
            Customer = customerDao;
            Reservation = reservationDao;
            Room = roomDao;
            Service = serviceDao;
        }
    }
}
