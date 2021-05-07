using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Project_v1
{
    class Room //Class for Hotel Room
    {
        public int RoomNumber
        {
            get;
            private set;
        }

        public int Cost
        {
            get;
            private set;
        }

        public int Places
        {
            get;
            private set;
        }

        public string Category
        {
            get;
            private set;
        }

        public Room(int number, int cost, int places, string category)
        {
            if (number > 0 && cost > 0 && places > 0 && category != "")
            {
                this.RoomNumber = number;
                this.Cost = cost;
                this.Places = places;
                this.Category = category;
            }
            else
            {
                throw new Exception();
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is null) && (obj is Room))
            {
                var room = obj as Room;
                return this.RoomNumber.Equals(room.RoomNumber);
            }
            return false;
        }
    }
}
