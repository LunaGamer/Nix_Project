using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Project_v1
{
    class ConsoleInterface //Class for console Interface methods
    {
        private Administration administrator;

        public ConsoleInterface(Administration admin)
        {
            this.administrator = admin;
        }

        public int MainMenu()
        {
            int i;
            Console.Clear();
            Console.WriteLine("Choose option:");
            Console.WriteLine("1. Add Room");
            Console.WriteLine("2. Delete Room");
            Console.WriteLine("3. Find free Rooms");
            Console.WriteLine("4. Book the Room");
            Console.WriteLine("5. Registration of the Booked room");
            Console.WriteLine("6. New Register");
            Console.WriteLine("7. CheckOut");
            Console.WriteLine("0. Press 0 for Exit");
            i = IntParse();
            if (i < 0 || i > 7)
            {
                throw new Exception();
            }
            switch(i)
            {
                case 1:
                    AddRoom(administrator);
                    Console.WriteLine("Press Any key to continue.");
                    Console.ReadKey();
                    break;
                case 2:
                    DeleteRoom(administrator);
                    Console.WriteLine("Press Any key to continue.");
                    Console.ReadKey();
                    break;
                case 3:
                    FindFreeRooms(administrator);
                    Console.WriteLine("Press Any key to continue.");
                    Console.ReadKey();
                    break;
                case 4:
                    Reservation(administrator, Status.Booking);
                    Console.WriteLine("Press Any key to continue.");
                    Console.ReadKey();
                    break;
                case 5:
                    RegisterBookedRoom(administrator);
                    Console.WriteLine("Press Any key to continue.");
                    Console.ReadKey();
                    break;
                case 6:
                    Reservation(administrator, Status.Occupied);
                    Console.WriteLine("Press Any key to continue.");
                    Console.ReadKey();
                    break;
                case 7:
                    CheckOut(administrator);
                    Console.WriteLine("Press Any key to continue.");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Press Any key to continue.");
                    Console.ReadKey();
                    break;
            }
            return i;
        }

        public void AddRoom(Administration admin)
        {
            int number;
            int cost;
            int places;
            string category;
            Console.Clear();
            Console.WriteLine("Adding new Room:");
            Console.WriteLine("Write Room number");
            number = IntParse();
            Console.WriteLine("Write Room Cost");
            cost = IntParse();
            Console.WriteLine("Write number of sleep places");
            places = IntParse();
            Console.WriteLine("Write Room category");
            category = StringParse();
            admin.AddRoom(number, cost, places, category);
        }

        public void DeleteRoom(Administration admin)
        {
            int number;
            Console.Clear();
            Console.WriteLine("Deleting Room:");
            Console.WriteLine("Write Room number");
            number = IntParse();
            admin.DeleteRoom(number);
        }

        public void FindFreeRooms(Administration admin)
        {
            DateTime start;
            DateTime end;
            Console.Clear();
            Console.WriteLine("Check free rooms for date period:");
            Console.WriteLine("Write arrival date");
            start = WriteDate();
            Console.WriteLine("Write Check-Out date");
            end = WriteDate();
            if (!admin.CheckFreeRooms(start, end).Any())
            {
                Console.WriteLine("Sorry, no free rooms for that period");
            }
            else
            {
                foreach (var room in admin.CheckFreeRooms(start, end))
                {
                    Console.WriteLine($"Room {room.RoomNumber} with {room.Places} sleep places " +
                        $"for {room.Cost}$ per night, category {room.Category}");
                }
            }
        }

        public void Reservation(Administration admin, Status status)
        {
            int number;
            string name;
            DateTime birthdate;
            string address;
            DateTime start;
            DateTime end;
            Console.Clear();
            if (status == Status.Booking)
            {
                Console.WriteLine("Booking the Room:");
            }
            else
            {
                Console.WriteLine("Registration:");
            }
            Console.WriteLine("Write Room number");
            number = IntParse();
            Console.WriteLine("Write guest FullName");
            name = StringParse();
            Console.WriteLine("Write guest Birthdate");
            birthdate = WriteDate();
            Console.WriteLine("Write guest Address");
            address = StringParse();
            Console.WriteLine("Write arrival date");
            start = WriteDate();
            Console.WriteLine("Write Check-Out date");
            end = WriteDate();
            admin.Reservation(number, name, birthdate, address, start, end, status);

        }

        public void RegisterBookedRoom(Administration admin)
        {
            int i = 0;
            int stringNumber;
            Console.Clear();
            Console.WriteLine("Registration of the booked room:");
            if (!admin.BookedRooms().Any())
            {
                Console.WriteLine("No Booked rooms found");
            }
            else
            {
                foreach (var booked in admin.BookedRooms())
                {
                    ++i;
                    Console.WriteLine($"{i}. Room# {booked.Room.RoomNumber}, booked by {booked.Guest.FullName}" +
                        $" for period from {booked.StartDate.ToShortDateString()} to {booked.EndDate.ToShortDateString()}");
                }
                Console.WriteLine("Write number of Booking to Register");
                stringNumber = IntParse();
                if (stringNumber > i)
                {
                    throw new Exception();
                }
                else
                {
                    admin.RegistrationFromBooking(admin.BookedRooms().ToList()[stringNumber - 1]);
                }
            }
        }

        public void CheckOut(Administration admin)
        {
            int i = 0;
            int stringNumber;
            Console.Clear();
            Console.WriteLine("Checking-Out from the room:");
            if (!admin.OccupiedRooms().Any())
            {
                Console.WriteLine("No currently Occupied rooms found");
            }
            else
            {
                foreach (var occupied in admin.OccupiedRooms())
                {
                    ++i;
                    Console.WriteLine($"{i}. Room# {occupied.Room.RoomNumber}, occupied by {occupied.Guest.FullName}" +
                        $" for period from {occupied.StartDate.ToShortDateString()} to {occupied.EndDate.ToShortDateString()}");
                }
                Console.WriteLine("Write number of Registration to Check-Out");
                stringNumber = IntParse();
                if (stringNumber > i)
                {
                    throw new Exception();
                }
                else
                {
                    admin.CheckOut(admin.OccupiedRooms().ToList()[stringNumber - 1]);
                }
            }
        }

        public int IntParse()
        {
            int i;
            if (!int.TryParse(Console.ReadLine(), out i))
            {
                throw new Exception();
            }
            return i;
        }

        public string StringParse()
        {
            string s = Console.ReadLine();
            if (!(s.Length > 0))
            {
                throw new Exception();
            }
            return s;
        }

        public DateTime WriteDate()
        {
            int year;
            int month;
            int day;
            Console.WriteLine("Write day");
            if (!int.TryParse(Console.ReadLine(), out day))
            {
                throw new Exception();
            }
            Console.WriteLine("Write month");
            if (!int.TryParse(Console.ReadLine(), out month))
            {
                throw new Exception();
            }
            Console.WriteLine("Write year");
            if (!int.TryParse(Console.ReadLine(), out year))
            {
                throw new Exception();
            }
            return (new DateTime(year, month, day));
        }
    }
}
