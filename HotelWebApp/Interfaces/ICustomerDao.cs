using HotelWebApp.Dto;

namespace HotelWebApp.Interfaces
{
    public interface ICustomerDao
    {
        void AddCustomer(CustomerDto customer);
        CustomerDto GetCustomerByCode(string fiscalCode);
        List<CustomerDto> GetAllCustomers();
    }
}
