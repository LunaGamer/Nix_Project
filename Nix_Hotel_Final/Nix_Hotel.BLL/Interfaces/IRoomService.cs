using Nix_Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.BLL.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<RoomDTO> GetAllRooms();
        RoomDTO Get(int id);
        IEnumerable<RoomDTO> GetFreeRooms(DateTime startDate, DateTime endDate);
        IEnumerable<CategoryDTO> GetCategories();
        void Create(RoomDTO room);
        void Update (int id, RoomDTO room);
    }
}
