﻿using Nix_Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.BLL.Interfaces
{
    interface IRoomService
    {
        IEnumerable<RoomDTO> GetAllRooms();

        RoomDTO Get(int id);

        IEnumerable<RoomDTO> GetFreeRooms(DateTime startDate, DateTime endDate);
    }
}
