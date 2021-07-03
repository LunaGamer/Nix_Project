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

        private IMapper mapper;

        public ClientService(IWorkUnit database)
        {
            this.Database = database;
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
        }

        public IEnumerable<ClientDTO> GetAllClients()
        {
            return mapper.Map<IEnumerable<Client>, List<ClientDTO>>(Database.Clients.GetAll());
        }

        public ClientDTO Get(int id)
        {
            return mapper.Map<Client, ClientDTO>(Database.Clients.Get(id));
        }

        public void Create(ClientDTO client)
        {
            var mapperCreate = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, Client>()).CreateMapper();
            Database.Clients.Create(mapperCreate.Map<ClientDTO, Client>(client));
        }
    }
}
