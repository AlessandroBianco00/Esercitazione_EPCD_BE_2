using HotelWebApp.Interfaces;
using HotelWebApp.Services.Dao;

namespace HotelWebApp
{
    public static class Helpers
    {
        public static IServiceCollection RegisterDAOs(this IServiceCollection services)
        {
            return services
                .AddScoped<ICustomerDao, CustomerDao>()
                .AddScoped<IReservationDao, ReservationDao>()
                .AddScoped<IReservationService, ReservationServiceDao>()
                .AddScoped<IRoomDao, RoomDao>()
                .AddScoped<IServiceDao, ServiceDao>()
                ;
        }
    }
}
