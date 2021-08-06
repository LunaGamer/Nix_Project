using Nix_Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Nix_Hotel.BLL.DTO;
using Nix_Hotel.DAL.Enteties;
using Nix_Hotel.BLL.Interfaces;
using Nix_Hotel.BLL.Infrastructure;

namespace Nix_Hotel.BLL.Services
{
    public class ClientService : IClientService
    {
        private IWorkUnit Database
        {
            get;
            set;
        }

        public ClientService(IWorkUnit database)
        {
            this.Database = database;
        }

        public IEnumerable<ClientDTO> GetAllClients()
        {
            var clients = Database.Clients.GetAll();
            return HotelMapperBLL.MapClientReadList(clients);
        }

        public ClientDTO Get(int id)
        {
            var client = Database.Clients.Get(id);
            return HotelMapperBLL.MapClientReadSingle(client);
        }

        public void Create(ClientDTO clientDTO)
        {
            if (clientDTO != null)
            {
                var client = HotelMapperBLL.MapClientWrite(clientDTO);
                Database.Clients.Create(client);
                Database.Save();
            }
        }

        public void Update(int id, ClientDTO clientDTO)
        {
            if (clientDTO != null)
            {
                var client = HotelMapperBLL.MapClientWrite(clientDTO);
                Database.Clients.Update(id, client);
                Database.Save();
            }
        }
    }
}
