using HotelWebApp.Dto;

namespace HotelWebApp.Interfaces
{
    public interface IServiceDao
    {
        void AddService(ServiceDto service);
        ServiceDto GetServiceById(int id);
        List<ServiceDto> GetAllServices();
    }
}
