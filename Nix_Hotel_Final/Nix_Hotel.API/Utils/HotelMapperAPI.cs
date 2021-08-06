using AutoMapper;
using Nix_Hotel.API.Models;
using Nix_Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nix_Hotel.API.Utils
{
    public static class HotelMapperAPI
    {
        private static IMapper mapperBookingRead;
        private static IMapper mapperBookingWrite;
        private static IMapper mapperRoomRead;
        private static IMapper mapperRoomWrite;
        private static IMapper mapperCategory;
        private static IMapper mapperClient;
        private static IMapper mapperStatus;

        static HotelMapperAPI()
        {
            mapperBookingRead = new MapperConfiguration(cfg => cfg.CreateMap<BookingDTO, BookingModel>()
            .ForMember(b => b.Client, opt => opt.Ignore())
            .ForMember(b => b.Room, opt => opt.Ignore())
            .ForMember(b => b.Status, opt => opt.Ignore())).CreateMapper();
            mapperBookingWrite = new MapperConfiguration(cfg => cfg.CreateMap<BookingModel, BookingDTO>()
            .ForMember(b => b.Client, opt => opt.Ignore())
            .ForMember(b => b.Room, opt => opt.Ignore())
            .ForMember(b => b.Status, opt => opt.Ignore())).CreateMapper();
            mapperRoomRead = new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, RoomModel>()
            .ForMember(room => room.Category, opt => opt.Ignore())).CreateMapper();
            mapperRoomWrite = new MapperConfiguration(cfg => cfg.CreateMap<RoomModel, RoomDTO>()
           .ForMember(room => room.Category, opt => opt.Ignore())).CreateMapper();
            mapperCategory = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap()).CreateMapper();
            mapperClient = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>().ReverseMap()).CreateMapper();
            mapperStatus = new MapperConfiguration(cfg => cfg.CreateMap<StatusDTO, StatusModel>().ReverseMap()).CreateMapper();
        }

        public static RoomModel MapRoomReadSingle(RoomDTO roomDTO)
        {
            if (roomDTO != null)
            {
                var roomModel = mapperRoomRead.Map<RoomDTO, RoomModel>(roomDTO);
                roomModel.Category = mapperCategory.Map<CategoryDTO, CategoryModel>(roomDTO.Category);
                return roomModel;
            }
            return null;
        }

        public static List<RoomModel> MapRoomReadList(IEnumerable<RoomDTO> roomsDTO)
        {
            if (roomsDTO != null && roomsDTO.Any())
            {
                List<RoomModel> roomsModel = new List<RoomModel>();
                foreach (var room in roomsDTO)
                {
                    roomsModel.Add(MapRoomReadSingle(room));
                }
                return roomsModel;
            }
            return null;
        }

        public static RoomDTO MapRoomWrite(RoomModel roomModel)
        {
            if (roomModel != null)
            {
                var roomDTO = mapperRoomWrite.Map<RoomModel, RoomDTO>(roomModel);
                roomDTO.Category = mapperCategory.Map<CategoryModel, CategoryDTO>(roomModel.Category);
                return roomDTO;
            }
            return null;
        }

        public static BookingModel MapBookingReadSingle(BookingDTO bookingDTO)
        {
            if (bookingDTO != null)
            {
                var bookingModel = mapperBookingRead.Map<BookingDTO, BookingModel>(bookingDTO);
                bookingModel.Room = MapRoomReadSingle(bookingDTO.Room);
                bookingModel.Client = MapClientReadSingle(bookingDTO.Client);
                bookingModel.Status = mapperStatus.Map<StatusDTO, StatusModel>(bookingDTO.Status);
                return bookingModel;
            }
            return null;
        }

        public static List<BookingModel> MapBookingReadList(IEnumerable<BookingDTO> bookingsDTO)
        {
            if (bookingsDTO != null && bookingsDTO.Any())
            {
                List<BookingModel> bookingsModel = new List<BookingModel>();
                foreach (var booking in bookingsDTO)
                {
                    bookingsModel.Add(MapBookingReadSingle(booking));
                }
                return bookingsModel;
            }
            return null;
        }

        public static BookingDTO MapBookingWrite(BookingModel bookingModel)
        {
            if (bookingModel != null)
            {
                var bookingDTO = mapperBookingWrite.Map<BookingModel, BookingDTO>(bookingModel);
                bookingDTO.Room = MapRoomWrite(bookingModel.Room);
                bookingDTO.Client = MapClientWrite(bookingModel.Client);
                bookingDTO.Status = mapperStatus.Map<StatusModel, StatusDTO>(bookingModel.Status);
                return bookingDTO;
            }
            return null;
        }

        public static ClientModel MapClientReadSingle(ClientDTO clientDTO)
        {
            if (clientDTO != null)
            {
                var clientModel = mapperClient.Map<ClientDTO, ClientModel>(clientDTO);
                return clientModel;
            }
            return null;
        }

        public static List<ClientModel> MapClientReadList(IEnumerable<ClientDTO> clientsDTO)
        {
            if (clientsDTO != null && clientsDTO.Any())
            {
                var clientsModel = mapperClient.Map<IEnumerable<ClientDTO>, List<ClientModel>>(clientsDTO);
                return clientsModel;
            }
            return null;
        }

        public static ClientDTO MapClientWrite(ClientModel clientModel)
        {
            if (clientModel != null)
            {
                var clientDTO = mapperClient.Map<ClientModel, ClientDTO>(clientModel);
                return clientDTO;
            }
            return null;
        }
    }
}