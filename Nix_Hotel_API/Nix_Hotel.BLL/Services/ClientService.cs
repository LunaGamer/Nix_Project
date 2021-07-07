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

namespace Nix_Hotel.BLL.Services
{
    public class ClientService : IClientService
    {
        private IWorkUnit Database
        {
            get;
            set;
        }

        private IMapper mapperRead;
        private IMapper mapperWrite;

        public ClientService(IWorkUnit database)
        {
            this.Database = database;
            mapperRead = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            mapperWrite = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, Client>()).CreateMapper();
        }

        public IEnumerable<ClientDTO> GetAllClients()
        {
            return mapperRead.Map<IEnumerable<Client>, List<ClientDTO>>(Database.Clients.GetAll());
        }

        public ClientDTO Get(int id)
        {
            var client = Database.Clients.Get(id);
            if (client != null)
            {
                return mapperRead.Map<Client, ClientDTO>(Database.Clients.Get(id));
            }
            return null;
        }

        public void Create(ClientDTO client)
        {
            if (client != null)
            {
                Database.Clients.Create(mapperWrite.Map<ClientDTO, Client>(client));
                Database.Save();
            }
        }

        public void Update(int id, ClientDTO client)
        {
            if (client != null)
            {
                var newClient = mapperWrite.Map<ClientDTO, Client>(client);
                Database.Clients.Update(id, newClient);
                Database.Save();
            }
        }
    }
}
