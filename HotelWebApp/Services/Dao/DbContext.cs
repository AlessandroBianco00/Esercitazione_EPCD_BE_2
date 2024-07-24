using HotelWebApp.Interfaces;

namespace HotelWebApp.Services.Dao
{
    public class DbContext
    {
        public ICustomerDao Customer { get; set; }

        public DbContext(ICustomerDao customerDao) 
        {
            Customer = customerDao;
        }
    }
}
