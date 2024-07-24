using HotelWebApp.Dto;

namespace HotelWebApp.Interfaces
{
    public interface ICustomerDao
    {
        void CreateCustomer(CustomerDto customer);
        CustomerDto GetCustomerByCode(string fiscalCode);
        List<CustomerDto> GetAllCustomers();
        void DeleteCustomer(string fiscalCode);
    }
}
