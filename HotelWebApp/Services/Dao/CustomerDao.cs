using HotelWebApp.Dto;
using HotelWebApp.Interfaces;

namespace HotelWebApp.Services.Dao
{
    public class CustomerDao : BaseDao, ICustomerDao
    {
        public CustomerDao(IConfiguration configuration) : base(configuration) { }
        public void AddCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }

        public List<CustomerDto> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public CustomerDto GetCustomerByCode(string fiscalCode)
        {
            throw new NotImplementedException();
        }
    }
}
