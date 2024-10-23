
using Sempi5.Domain.SurgeryRoomAggregate;
using Sempi5.Domain.TodoItem;
using Sempi5.Infrastructure.Databases;
using Sempi5.Infrastructure.Shared;
using Sempi5.Infrastructure.SurgeryRoomRepository;

namespace Sempi5.Infrastructure.SurgeryRoomRepository
{
    public class SurgeryRoomRepository : BaseRepository<SurgeryRoom, RoomNumber>, ISurgeryRoomRepository
    {
        public SurgeryRoomRepository(DBContext context):base(context.SurgeryRooms)
        {
           
        }


    }
}