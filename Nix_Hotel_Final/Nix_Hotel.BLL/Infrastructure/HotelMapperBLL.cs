using AutoMapper;
using Nix_Hotel.BLL.DTO;
using Nix_Hotel.DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.BLL.Infrastructure
{
    public static class HotelMapperBLL
    {
        private static IMapper mapperBookingRead;
        private static IMapper mapperBookingWrite;
        private static IMapper mapperRoomRead;
        private static IMapper mapperRoomWrite;
        public static IMapper mapperCategory;
        public static IMapper mapperClient;
        public static IMapper mapperStatus;
        public static IMapper mapperAdministrator;

        static HotelMapperBLL()
        {
            mapperBookingRead = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingDTO>()
            .ForMember(b => b.Client, opt => opt.Ignore())
            .ForMember(b => b.Room, opt => opt.Ignore())
            .ForMember(b => b.Status, opt => opt.Ignore())).CreateMapper();
            mapperBookingWrite = new MapperConfiguration(cfg => cfg.CreateMap<BookingDTO, Booking>()
            .ForMember(b => b.ClientId, opt => opt.MapFrom(c => c.Client.Id))
            .ForMember(b => b.RoomId, opt => opt.MapFrom(r => r.Room.Id))
            .ForMember(b => b.StatusId, opt => opt.MapFrom(s => s.Status.Id))).CreateMapper();
            mapperRoomRead = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDTO>()
            .ForMember(room => room.Category, opt => opt.Ignore())).CreateMapper();
            mapperRoomWrite = new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, Room>()
            .ForMember(room => room.CategoryId, opt => opt.MapFrom(c => c.Category.Id))).CreateMapper();
            mapperCategory = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>().ReverseMap()).CreateMapper();
            mapperClient = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>().ReverseMap()).CreateMapper();
            mapperStatus = new MapperConfiguration(cfg => cfg.CreateMap<Status, StatusDTO>().ReverseMap()).CreateMapper();
            mapperAdministrator = new MapperConfiguration(cfg => cfg.CreateMap<Administrator, AdministratorDTO>().ReverseMap()).CreateMapper();
        }

        public static RoomDTO MapRoomReadSingle(Room room)
        {
            if (room != null)
            {
                var roomDTO = mapperRoomRead.Map<Room, RoomDTO>(room);
                roomDTO.Category = mapperCategory.Map<Category, CategoryDTO>(room.RoomCategory);
                return roomDTO;
            }
            return null;
        }

        public static List<RoomDTO> MapRoomReadList(IEnumerable<Room> rooms)
        {
            if (rooms != null && rooms.Any())
            {
                List<RoomDTO> roomsDTO = new List<RoomDTO>();
                foreach (var room in rooms)
                {
                    roomsDTO.Add(MapRoomReadSingle(room));
                }
                return roomsDTO;
            }
            return null;
        }

        public static Room MapRoomWrite(RoomDTO roomDTO)
        {
            if (roomDTO != null)
            {
                var room = mapperRoomWrite.Map<RoomDTO, Room>(roomDTO);
                return room;
            }
            return null;
        }

        public static BookingDTO MapBookingReadSingle(Booking booking)
        {
            if (booking != null)
            {
                var bookingDTO = mapperBookingRead.Map<Booking, BookingDTO>(booking);
                bookingDTO.Room = MapRoomReadSingle(booking.BookingRoom);
                bookingDTO.Client = mapperClient.Map<Client, ClientDTO>(booking.BookingClient);
                bookingDTO.Status = mapperStatus.Map<Status, StatusDTO>(booking.BookingStatus);
                return bookingDTO;
            }
            return null;
        }

        public static List<BookingDTO> MapBookingReadList(IEnumerable<Booking> bookings)
        {
            if (bookings != null && bookings.Any())
            {
                List<BookingDTO> bookingsDTO = new List<BookingDTO>();
                foreach (var booking in bookings)
                {
                    bookingsDTO.Add(MapBookingReadSingle(booking));
                }
                return bookingsDTO;
            }
            return null;
        }

        public static Booking MapBookingWrite(BookingDTO bookingDTO)
        {
            if (bookingDTO != null)
            {
                var booking = mapperBookingWrite.Map<BookingDTO, Booking>(bookingDTO);
                return booking;
            }
            return null;
        }

        public static ClientDTO MapClientReadSingle(Client client)
        {
            if (client != null)
            {
                var clientDTO = mapperClient.Map<Client, ClientDTO>(client);
                return clientDTO;
            }
            return null;
        }

        public static List<ClientDTO> MapClientReadList(IEnumerable<Client> clients)
        {
            if (clients != null && clients.Any())
            {
                var clientsDTO = mapperClient.Map<IEnumerable<Client>, List<ClientDTO>>(clients);
                return clientsDTO;
            }
            return null;
        }

        public static Client MapClientWrite(ClientDTO clientDTO)
        {
            if (clientDTO != null)
            {
                var client = mapperClient.Map<ClientDTO, Client>(clientDTO);
                return client;
            }
            return null;
        }
    }
}
