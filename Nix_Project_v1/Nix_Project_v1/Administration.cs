using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Nix_Project_v1
{
    class Administration //Class for Hotel Administration methods and data
    {
        private List<Room> rooms;

        private List<Reservation> reservations;

        public Administration()
        {

            rooms = new List<Room>();
            reservations = new List<Reservation>();
            ReadRoomsFromFile(); //Reading Room data from bin file
            ReadReservationsFromFile(); //Reading Reservations/Registrations data from bin file
        }

        ~Administration() //Saving changes to files on object death
        {
            SaveRoomsToFile(); 
            SaveReservationsToFile();
        }
        

        public void AddRoom(int number, int cost, int places, string category)
        {
            if (rooms.Any(x => x.RoomNumber == number))
            {
                throw new Exception();
            }
            else
            {
                rooms.Add(new Room(number, cost, places, category));
            }
        }

        public void DeleteRoom(int roomNumber)
        {
            var room = rooms.Find(x => x.RoomNumber == roomNumber);
            if (!(room is null))
            {
                rooms.Remove(room);
            }
            else
            {
                throw new Exception();
            }
        }

        public IEnumerable<Room> CheckFreeRooms(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new Exception();
            }
            else
            {
                List<Room> freeRooms = new List<Room>();
                freeRooms.AddRange(rooms);
                foreach (var reserv in reservations)
                {
                    if (reserv.Status < Status.CheckedOut && !(reserv.StartDate > end || reserv.EndDate < start))
                    {
                        freeRooms.Remove(reserv.Room);
                    }
                }
                return freeRooms;
            }
        }

        public void Reservation(int roomNumber, string Name, DateTime birthdate, string address, DateTime start, DateTime end, Status status)
        {
            var room = rooms.Find(x => x.RoomNumber == roomNumber);
            if (room is null || !(CheckFreeRooms(start, end).Contains(room)))
            {
                throw new Exception();
            }
            else
            {
                reservations.Add(new Reservation(room, new Guest(Name, birthdate, address), start, end, status));
            }
        }  
        
        public void RegistrationFromBooking(Reservation reservation)
        {
            if (reservation.Status == Status.Booking)
            {
                reservation.ChangeStatus(Status.Occupied);
            }
            else
            {
                throw new Exception();
            }
        }

        public void CheckOut(Reservation reservation)
        {
            if (reservation.Status == Status.Occupied)
            {
                reservation.ChangeStatus(Status.CheckedOut);
            }
            else
            {
                throw new Exception();
            }
        }

        public IEnumerable<Reservation> BookedRooms()
        {
            return reservations.Where(x => x.Status == Status.Booking).ToList();
        }

        public IEnumerable<Reservation> OccupiedRooms()
        {
            return reservations.Where(x => x.Status == Status.Occupied).ToList();
        }

        public void ReadRoomsFromFile() //Reading Rooms data from bin file
        {
            int number;
            int cost;
            int places;
            string category;
            using (var roomFile = File.Open("Room.bin", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
            {
                using (var roomReader = new BinaryReader(roomFile))
                {
                    while (roomFile.Position <= roomFile.Length - 1)
                    {
                        number = roomReader.ReadInt32();
                        cost = roomReader.ReadInt32();
                        places = roomReader.ReadInt32();
                        category = roomReader.ReadString();
                        rooms.Add(new Room(number, cost, places, category));
                    }
                }
            }
        }

        public void SaveRoomsToFile() //Saving Room data to bin file
        {
            using (var roomFile = File.Open("Room.bin", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (var roomWriter = new BinaryWriter(roomFile))
                {
                    foreach(var room in rooms)
                    {
                        roomWriter.Write(room.RoomNumber);
                        roomWriter.Write(room.Cost);
                        roomWriter.Write(room.Places);
                        roomWriter.Write(room.Category);
                    }
                }
            }
        }

        public void ReadReservationsFromFile() //Reading Reservations/Registrations data from bin file
        {
            int number;
            int cost;
            int places;
            string category;
            string name;
            DateTime birthdate;
            string address;
            DateTime start;
            DateTime end;
            Status status;
            using (var reservFile = File.Open("Reservations.bin", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
            {
                using (var reservReader = new BinaryReader(reservFile))
                {
                    while (reservFile.Position <= reservFile.Length - 1)
                    {
                        number = reservReader.ReadInt32();
                        name = reservReader.ReadString();
                        birthdate = new DateTime(reservReader.ReadInt64());
                        address = reservReader.ReadString();
                        start = new DateTime(reservReader.ReadInt64());
                        end = new DateTime(reservReader.ReadInt64());
                        status = (Status)reservReader.ReadInt32();
                        reservations.Add(new Reservation(rooms.Find(x => x.RoomNumber == number), new Guest(name, birthdate, address), start, end, status));
                    }
                }
            }
        }

        public void SaveReservationsToFile() //Saving Reservations/Registrations data to bin file
        {
            using (var reservFile = File.Open("Reservations.bin", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (var reservWriter = new BinaryWriter(reservFile))
                {
                    foreach (var reserv in reservations)
                    {
                        reservWriter.Write(reserv.Room.RoomNumber);
                        reservWriter.Write(reserv.Guest.FullName);
                        reservWriter.Write(reserv.Guest.Birthdate.ToBinary());
                        reservWriter.Write(reserv.Guest.FullName);
                        reservWriter.Write(reserv.StartDate.ToBinary());
                        reservWriter.Write(reserv.EndDate.ToBinary());
                        reservWriter.Write((int)reserv.Status);

                    }
                }
            }
        }
    }
}
