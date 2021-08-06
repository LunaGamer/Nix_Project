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
    public class ClientControllerTests
    {
        Mock<IClientService> mock;
        ClientController controller;
        IMapper mapper;
        int idGet, idPost;

        [OneTimeSetUp]
        public void Init()
        {
            idGet = 1;
            idPost = 3;
            mock = new Mock<IClientService>();          
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
            controller = new ClientController(mock.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            mock.Setup(getall => getall.GetAllClients()).Returns(new List<ClientDTO>()
            {
                new ClientDTO()
                {
                    Id = 1,
                    Name = "TestClientName1",
                    Surname = "TestClientSurname1"

                },
                new ClientDTO()
                {
                    Id = 2,
                    Name = "TestClientName2",
                    Surname = "TestClientSurname2"

                }
            });
            mock.Setup(getid => getid.Get(idGet)).Returns(new ClientDTO()
            {
                Id = 1,
                Name = "TestClientName1",
                Surname = "TestClientSurname1"

            });
            mock.Setup(getid => getid.Get(idPost)).Returns(new ClientDTO()
            {
                Id = 3,
                Name = "TestClientName3",
                Surname = "TestClientSurname3"

            });

        }

        [Test]
        public void ClientsGetAllStatusCode()
        {

            var actual = controller.Get(controller.Request);
            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
        }

        [Test]
        public void ClientsGetAllResult()
        {
            var expected = mapper.Map<IEnumerable<ClientDTO>, IEnumerable<ClientModel>>(mock.Object.GetAllClients());
            var response = controller.Get(controller.Request);
            IEnumerable<ClientModel> actual;
            Assert.IsTrue(response.TryGetContentValue<IEnumerable<ClientModel>>(out actual));
            Assert.IsTrue(expected.ToList().SequenceEqual(actual.ToList()));
        }

        [Test]
        public void ClientsGetByIDStatusCode()
        {

            var actual = controller.Get(controller.Request, idGet);
            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
        }

        [Test]
        public void ClientsGetByIDResult()
        {
            var expected = mapper.Map<ClientDTO, ClientModel>(mock.Object.Get(idGet));
            var response = controller.Get(controller.Request, idGet);
            ClientModel actual;
            Assert.IsTrue(response.TryGetContentValue(out actual));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ClientPostStatusCode()
        {
            var acctual = controller.Post(controller.Request, new ClientModel
            {
                Id = 3,
                Name = "TestClientName3",
                Surname = "TestClientSurname3"
            });
            Assert.AreEqual(HttpStatusCode.OK, acctual.StatusCode);
        }

        [Test]
        public void ClientPostResult()
        {
            var expected = mapper.Map<ClientDTO, ClientModel>(mock.Object.Get(idPost));
            var response = controller.Post(controller.Request, new ClientModel
            {
                Id = 3,
                Name = "TestClientName3",
                Surname = "TestClientSurname3"
            });
            ClientModel actual;
            Assert.IsTrue(response.TryGetContentValue(out actual));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ClientPutStatusCode()
        {
            var actual = controller.Put(controller.Request, idGet, new ClientModel()
            {
                Id = 1,
                Name = "TestClientName1",
                Surname = "TestClientSurname1"
            });
            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
        }

        [Test]
        public void ClientPutResult()
        {
            var expected = mapper.Map<ClientDTO, ClientModel>(mock.Object.Get(idGet));
            var response = controller.Put(controller.Request, idGet, new ClientModel()
            {
                Id = 1,
                Name = "TestClientName1",
                Surname = "TestClientSurname1"
            });
            ClientModel actual;
            Assert.IsTrue(response.TryGetContentValue(out actual));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ClientPostNullReturnsBadRequest()
        {            
            var acctual = controller.Post(controller.Request, null);
            Assert.AreEqual(HttpStatusCode.BadRequest, acctual.StatusCode);
        }

        [Test]
        public void ClientPutNullReturnsNotFound()
        {
            var acctual = controller.Put(controller.Request, idGet, null);
            Assert.AreEqual(HttpStatusCode.NotFound, acctual.StatusCode);
        }
    }
}
