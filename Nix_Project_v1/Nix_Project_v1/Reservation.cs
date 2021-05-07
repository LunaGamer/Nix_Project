using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Project_v1
{
    class Reservation //Class for Reservation/Registration information
    {
        public Status Status
        {
            get;
            private set;
        }

        public Room Room
        {
            get;
            private set;
        }

        public Guest Guest
        {
            get;
            private set;
        }

        public DateTime StartDate
        {
            get;
            private set;
        }

        public DateTime EndDate
        {
            get;
            private set;
        }

        public Reservation(Room room, Guest guest, DateTime start, DateTime end, Status status)
        {
            if (!(room is null) && !(guest is null) && end > start && status < Status.CheckedOut)
            {
                this.Room = room;
                this.Guest = guest;
                this.StartDate = start;
                this.EndDate = end;
                this.Status = status;
            }
            else
            {
                throw new Exception();
            }
        }

        public void ChangeStatus(Status newStatus)
        {
            if (newStatus > Status)
            {
                Status = newStatus;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
