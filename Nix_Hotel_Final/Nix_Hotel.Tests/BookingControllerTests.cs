using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nix_Hotel.API.Controllers;
using Nix_Hotel.BLL.Interfaces;
using Nix_Hotel.BLL.DTO;
using Moq;
using AutoMapper;
using Nix_Hotel.API.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nix_Hotel.Tests
{
    [TestFixture]
    public class BookingControllerTests
    {
        Mock<IBookingService> mock;
        Mock<IRoomService> mockRoom;
        BookingController controller;
        IMapper mapperRead;
        IMapper mapperWrite;
        IMapper mapperRoomRead;
        IMapper mapperRoomWrite;
        IMapper mapperCategory;
        IMapper mapperClient;
        IMapper mapperStatus;
        int idGet, idPost, year, month;
        DateTime start, end;

        [OneTimeSetUp]
        public void Init()
        {
            idGet = 1;
            idPost = 3;
            year = 2021;
            month = 07;
            start = new DateTime(2021, 09, 15);
            end = new DateTime(2021, 09, 25);
            mock = new Mock<IBookingService>();
            mockRoom = new Mock<IRoomService>();
            mapperRead = new MapperConfiguration(cfg => cfg.CreateMap<BookingDTO, BookingModel>()
           .ForMember(b => b.Client, opt => opt.Ignore())
           .ForMember(b => b.Room, opt => opt.Ignore())
           .ForMember(b => b.Status, opt => opt.Ignore())).CreateMapper();
            mapperWrite = new MapperConfiguration(cfg => cfg.CreateMap<BookingModel, BookingDTO>()
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
            controller = new BookingController(mock.Object, mockRoom.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            mock.Setup(getall => getall.GetAllBookings()).Returns(new List<BookingDTO>()
            {
                new BookingDTO()
                {
                    Id = 1,
                    BookingDate = new DateTime(2021, 07, 05),
                    ArrivalDate = new DateTime(2021, 07, 15),
                    CheckoutDate = new DateTime(2021, 07, 25),
                    Client = new ClientDTO()
                    {
                        Id = 1,
                        Name = "TestClientName1",
                        Surname = "TestClientSurname1"
                    },
                    Room = new RoomDTO()
                    {
                        Id = 1,
                        Name = "Room1",
                        Category = new CategoryDTO()
                        {
                            Id = 1
                        },
                        Active = true
                    },
                    Status = new StatusDTO()
                    {
                        Id = 1,
                        Name = "Booking"
                    }
                },
                new BookingDTO()
                {
                    Id = 2,
                    BookingDate = new DateTime(2021, 08, 05),
                    ArrivalDate = new DateTime(2021, 08, 15),
                    CheckoutDate = new DateTime(2021, 08, 25),
                    Client = new ClientDTO()
                    {
                        Id = 2,
                        Name = "TestClientName2",
                        Surname = "TestClientSurname2"
                    },
                    Room = new RoomDTO()
                    {
                        Id = 2,
                        Name = "Room2",
                        Category = new CategoryDTO()
                        {
                            Id = 2
                        },
                        Active = true
                    },
                    Status = new StatusDTO()
                    {
                        Id = 1,
                        Name = "Booking"
                    }
                }
            });
            mock.Setup(getid => getid.Get(idGet)).Returns(new BookingDTO()
            {
                Id = 1,
                BookingDate = new DateTime(2021, 07, 05),
                ArrivalDate = new DateTime(2021, 07, 15),
                CheckoutDate = new DateTime(2021, 07, 25),
                Client = new ClientDTO()
                {
                    Id = 1,
                    Name = "TestClientName1",
                    Surname = "TestClientSurname1"
                },
                Room = new RoomDTO()
                {
                    Id = 1,
                    Name = "Room1",
                    Category = new CategoryDTO()
                    {
                        Id = 1
                    },
                    Active = true
                },
                Status = new StatusDTO()
                {
                    Id = 1,
                    Name = "Booking"
                }
            });
            mock.Setup(getid => getid.Get(idPost)).Returns(new BookingDTO()
            {
                Id = 3,
                BookingDate = new DateTime(2021, 09, 05),
                ArrivalDate = new DateTime(2021, 09, 15),
                CheckoutDate = new DateTime(2021, 09, 25),
                Client = new ClientDTO()
                {
                    Id = 1,
                    Name = "TestClientName1",
                    Surname = "TestClientSurname1"
                },
                Room = new RoomDTO()
                {
                    Id = 1,
                    Name = "Room1",
                    Category = new CategoryDTO()
                    {
                        Id = 1
                    },
                    Active = true
                },
                Status = new StatusDTO()
                {
                    Id = 1,
                    Name = "Booking"
                }
            });
            mock.Setup(getgain => getgain.Gain(new DateTime(year, month, 1))).Returns((decimal)1000);
            mockRoom.Setup(getfree => getfree.GetFreeRooms(start, end)).Returns(new List<RoomDTO>()
            {
                new RoomDTO()
                {
                    Id = 1,
                    Name = "Room1",
                    Category = new CategoryDTO()
                    {
                        Id = 1
                    },
                    Active = true
                },
                new RoomDTO()
                {
                    Id = 2,
                    Name = "Room2",
                    Category = new CategoryDTO()
                    {
                        Id = 2
                    },
                    Active = true
                },
            });
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
        }

        [Test]
        public void BookingGetAllStatusCode()
        {
            var actual = controller.Get(controller.Request);
            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
        }

        [Test]
        public void BookingGetAllResult()
        {
            var bookingsDTO = mock.Object.GetAllBookings();
            var expected = new List<BookingModel>();
            foreach(var bookingDTO in bookingsDTO)
            {
                var booking = mapperRead.Map<BookingDTO, BookingModel>(bookingDTO);
                booking.Room = mapperRoomRead.Map<RoomDTO, RoomModel>(bookingDTO.Room);
                booking.Room.Category = mapperCategory.Map<CategoryDTO, CategoryModel>(bookingDTO.Room.Category);
                booking.Client = mapperClient.Map<ClientDTO, ClientModel>(bookingDTO.Client);
                booking.Status = mapperStatus.Map<StatusDTO, StatusModel>(bookingDTO.Status);
                expected.Add(booking);
            }
            var response = controller.Get(controller.Request);
            IEnumerable<BookingModel> actual;
            Assert.IsTrue(response.TryGetContentValue<IEnumerable<BookingModel>>(out actual));
            Assert.IsTrue(expected.ToList().SequenceEqual(actual.ToList()));

        }

        [Test]
        public void BookingGetByIDStatusCode()
        {
            var actual = controller.Get(controller.Request, idGet);
            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
        }

        [Test]
        public void BookingGetByIdResult()
        {
            var bookingDTO = mock.Object.Get(idGet);
            var expected = mapperRead.Map<BookingDTO, BookingModel>(bookingDTO);
            expected.Room = mapperRoomRead.Map<RoomDTO, RoomModel>(bookingDTO.Room);
            expected.Room.Category = mapperCategory.Map<CategoryDTO, CategoryModel>(bookingDTO.Room.Category);
            expected.Client = mapperClient.Map<ClientDTO, ClientModel>(bookingDTO.Client);
            expected.Status = mapperStatus.Map<StatusDTO, StatusModel>(bookingDTO.Status);
            var response = controller.Get(controller.Request, idGet);
            BookingModel actual;
            Assert.IsTrue(response.TryGetContentValue(out actual));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BookingGetRevenueStatusCode()
        {
            var actual = controller.Get(controller.Request, year, month);
            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
        }

        [Test]
        public void BookingGetRevenueResult()
        {
            var expected = mock.Object.Gain(new DateTime(year, month, 1));
            var response = controller.Get(controller.Request, year, month);
            decimal actual;
            Assert.IsTrue(response.TryGetContentValue(out actual));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BookingtPostStatusCode()
        {
            var actual = controller.Post(controller.Request, new BookingModel()
            {
                Id = 3,
                BookingDate = new DateTime(2021, 09, 05),
                ArrivalDate = new DateTime(2021, 09, 15),
                CheckoutDate = new DateTime(2021, 09, 25),
                Client = new ClientModel()
                {
                    Id = 1,
                    Name = "TestClientName1",
                    Surname = "TestClientSurname1"
                },
                Room = new RoomModel()
                {
                    Id = 1,
                    Name = "Room1",
                    Category = new CategoryModel()
                    {
                        Id = 1
                    },
                    Active = true
                },
                Status = new StatusModel()
                {
                    Id = 1,
                    Name = "Booking"
                }
            });
            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
        }

        [Test]
        public void BookingPostResult()
        {
            var bookingDTO = mock.Object.Get(idPost);
            var expected = mapperRead.Map<BookingDTO, BookingModel>(bookingDTO);
            expected.Room = mapperRoomRead.Map<RoomDTO, RoomModel>(bookingDTO.Room);
            expected.Room.Category = mapperCategory.Map<CategoryDTO, CategoryModel>(bookingDTO.Room.Category);
            expected.Client = mapperClient.Map<ClientDTO, ClientModel>(bookingDTO.Client);
            expected.Status = mapperStatus.Map<StatusDTO, StatusModel>(bookingDTO.Status);
            var response = controller.Post(controller.Request, new BookingModel()
            {
                Id = 3,
                BookingDate = new DateTime(2021, 09, 05),
                ArrivalDate = new DateTime(2021, 09, 15),
                CheckoutDate = new DateTime(2021, 09, 25),
                Client = new ClientModel()
                {
                    Id = 1,
                    Name = "TestClientName1",
                    Surname = "TestClientSurname1"
                },
                Room = new RoomModel()
                {
                    Id = 1,
                    Name = "Room1",
                    Category = new CategoryModel()
                    {
                        Id = 1
                    },
                    Active = true
                },
                Status = new StatusModel()
                {
                    Id = 1,
                    Name = "Booking"
                }
            });
            BookingModel actual;
            Assert.IsTrue(response.TryGetContentValue(out actual));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BookingPutStatusCode()
        {
            var actual = controller.Put(controller.Request, idGet, new BookingModel()
            {
                Id = 1,
                BookingDate = new DateTime(2021, 07, 05),
                ArrivalDate = new DateTime(2021, 07, 15),
                CheckoutDate = new DateTime(2021, 07, 25),
                Client = new ClientModel()
                {
                    Id = 1,
                    Name = "TestClientName1",
                    Surname = "TestClientSurname1"
                },
                Room = new RoomModel()
                {
                    Id = 1,
                    Name = "Room1",
                    Category = new CategoryModel()
                    {
                        Id = 1
                    },
                    Active = true
                },
                Status = new StatusModel()
                {
                    Id = 1,
                    Name = "Booking"
                }
            });
            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
        }

        [Test]
        public void BookingPutResult()
        {
            var bookingDTO = mock.Object.Get(idGet);
            var expected = mapperRead.Map<BookingDTO, BookingModel>(bookingDTO);
            expected.Room = mapperRoomRead.Map<RoomDTO, RoomModel>(bookingDTO.Room);
            expected.Room.Category = mapperCategory.Map<CategoryDTO, CategoryModel>(bookingDTO.Room.Category);
            expected.Client = mapperClient.Map<ClientDTO, ClientModel>(bookingDTO.Client);
            expected.Status = mapperStatus.Map<StatusDTO, StatusModel>(bookingDTO.Status);
            var response = controller.Put(controller.Request, idGet, new BookingModel()
            {
                Id = 1,
                BookingDate = new DateTime(2021, 07, 05),
                ArrivalDate = new DateTime(2021, 07, 15),
                CheckoutDate = new DateTime(2021, 07, 25),
                Client = new ClientModel()
                {
                    Id = 1,
                    Name = "TestClientName1",
                    Surname = "TestClientSurname1"
                },
                Room = new RoomModel()
                {
                    Id = 1,
                    Name = "Room1",
                    Category = new CategoryModel()
                    {
                        Id = 1
                    },
                    Active = true
                },
                Status = new StatusModel()
                {
                    Id = 1,
                    Name = "Booking"
                }
            });
            BookingModel actual;
            Assert.IsTrue(response.TryGetContentValue(out actual));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BookingPostNullReturnsBadRequest()
        {
            var actual = controller.Post(controller.Request,  null);
            Assert.AreEqual(HttpStatusCode.BadRequest, actual.StatusCode);
        }

        [Test]
        public void BookingPutNullReturnsNotFoundt()
        {
            var actual = controller.Put(controller.Request, idGet, null);
            Assert.AreEqual(HttpStatusCode.NotFound, actual.StatusCode);
        }

        [Test]
        public void BookingGainRevenueImpossibleMonthReturnsBadRequest()
        {
            int month = 0;
            var actual = controller.Get(controller.Request, year, month);
            Assert.AreEqual(HttpStatusCode.BadRequest, actual.StatusCode);
        }

        [Test]
        public void BookingGainRevenueImpossibleYearReturnsBadRequest()
        {
            int year = 0;
            var actual = controller.Get(controller.Request, year, month);
            Assert.AreEqual(HttpStatusCode.BadRequest, actual.StatusCode);
        }
    }
}
