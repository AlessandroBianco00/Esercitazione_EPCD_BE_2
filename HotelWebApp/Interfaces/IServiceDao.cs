using HotelWebApp.Dto;

namespace HotelWebApp.Interfaces
{
    public interface IServiceDao
    {
        void AddService(ServiceDto customer);
        ServiceDto GetServiceById(int id);
        List<ServiceDto> GetAllServices();
    }
}
