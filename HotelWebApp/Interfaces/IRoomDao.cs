using HotelWebApp.Dto;

namespace HotelWebApp.Interfaces
{
    public interface IRoomDao
    {
        void AddRoom(RoomDto customer);
        RoomDto GetRoomByNumber(int roomNumber);
        List<RoomDto> GetAllRooms();
    }
}
