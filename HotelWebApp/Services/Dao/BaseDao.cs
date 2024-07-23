namespace HotelWebApp.Services.Dao
{
    public class BaseDao
    {
        protected readonly string connectionString;

        public BaseDao(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("HotelServer")!;
        }
    }
}
