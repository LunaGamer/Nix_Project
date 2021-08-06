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
    public class RoomControllerTests
    {
        Mock<IRoomService> mock;
        RoomController controller;
        IMapper mapperRead;
        IMapper mapperWrite;
        IMapper mapperCategory;
        int id;
        DateTime start, end;

        [OneTimeSetUp]
        public void Init()
        {
            id = 1;
            start = new DateTime(2021, 10, 10);
            end = new DateTime(2021, 10, 20);
            mock = new Mock<IRoomService>();
            mapperRead = new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, RoomModel>()
            .ForMember(room => room.Category, opt => opt.Ignore())).CreateMapper();
            mapperWrite = new MapperConfiguration(cfg => cfg.CreateMap<RoomModel, RoomDTO>()
            .ForMember(room => room.Category, opt => opt.Ignore())).CreateMapper();
            mapperCategory = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap()).CreateMapper();
            controller = new RoomController(mock.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            mock.Setup(getall => getall.GetAllRooms()).Returns(new List<RoomDTO>()
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
            mock.Setup(getid => getid.Get(id)).Returns(new RoomDTO()
            {
                Id = 1,
                Name = "Room1",
                Category = new CategoryDTO()
                {
                    Id = 1
                },
                Active = true
            });
            mock.Setup(getfree => getfree.GetFreeRooms(start, end)).Returns(new List<RoomDTO>()
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
        }

        [Test]
        public void RoomGetAllStatusCode()
        {
            var actual = controller.Get(controller.Request);
            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
        }

        [Test]
        public void RoomGetAllResult()
        {
            var roomsDTO = mock.Object.GetAllRooms();
            var expected = new List<RoomModel>();
            foreach(var roomDTO in roomsDTO)
            {
                var room = mapperRead.Map<RoomDTO, RoomModel>(roomDTO);
                room.Category = mapperCategory.Map<CategoryDTO, CategoryModel>(roomDTO.Category);
                expected.Add(room);
            }
            var response = controller.Get(controller.Request);
            IEnumerable<RoomModel> actual;
            Assert.IsTrue(response.TryGetContentValue<IEnumerable<RoomModel>>(out actual));
            Assert.IsTrue(expected.ToList().SequenceEqual(actual.ToList()));
        }

        [Test]
        public void RoomGetByIdStatusCode()
        {
            var actual = controller.Get(controller.Request, id);
            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
        }

        [Test]
        public void RoomGetByIdResult()
        {
            var roomDTO = mock.Object.Get(id);
            var expected = mapperRead.Map<RoomDTO, RoomModel>(roomDTO);
            expected.Category = mapperCategory.Map<CategoryDTO, CategoryModel>(roomDTO.Category);
            var response = controller.Get(controller.Request, id);
            RoomModel actual;
            Assert.IsTrue(response.TryGetContentValue(out actual));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RoomGetFreeStatusCode()
        {
            
            var actual = controller.Get(controller.Request, start.ToString(), end.ToString());
            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
        }

        [Test]
        public void RoomGetFreeResult()
        {
            var roomsDTO = mock.Object.GetFreeRooms(start, end);
            var expected = new List<RoomModel>();
            foreach (var roomDTO in roomsDTO)
            {
                var room = mapperRead.Map<RoomDTO, RoomModel>(roomDTO);
                room.Category = mapperCategory.Map<CategoryDTO, CategoryModel>(roomDTO.Category);
                expected.Add(room);
            }
            var response = controller.Get(controller.Request, start.ToString(), end.ToString());
            IEnumerable<RoomModel> actual;
            Assert.IsTrue(response.TryGetContentValue<IEnumerable<RoomModel>>(out actual));
            Assert.IsTrue(expected.ToList().SequenceEqual(actual.ToList()));
        }

        [Test]
        public void RoomPutStatusCode()
        {
            var actual = controller.Put(controller.Request, id, new RoomModel()
            {
                Id = 1,
                Name = "Room1",
                Category = new CategoryModel()
                {
                    Id = 1
                },
                Active = false
            });
            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
        }

        [Test]
        public void RoomPutResult()
        {
            var roomDTO = mock.Object.Get(id);
            var expected = mapperRead.Map<RoomDTO, RoomModel>(roomDTO);
            expected.Category = mapperCategory.Map<CategoryDTO, CategoryModel>(roomDTO.Category);
            var response = controller.Put(controller.Request, id, new RoomModel()
            {
                Id = 1,
                Name = "Room1",
                Category = new CategoryModel()
                {
                    Id = 1
                },
                Active = false
            });
            RoomModel actual;
            Assert.IsTrue(response.TryGetContentValue(out actual));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RoomPutNullReturnsNotFound()
        {
            var actual = controller.Put(controller.Request, id, null);
            Assert.AreEqual(HttpStatusCode.NotFound, actual.StatusCode);
        }
    }
}
