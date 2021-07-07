using Nix_Hotel.DAL.EF;
using Nix_Hotel.DAL.Enteties;
using Nix_Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.DAL.Repositories
{
    class ClientRepository : IRepository<Client>
    {
        private HotelModel db;

        public ClientRepository(HotelModel db)
        {
            this.db = db;
        }

        public IEnumerable<Client> GetAll()
        {
            return db.Clients;
        }

        public Client Get(int id)
        {
            return db.Clients.Find(id);
        }

        public void Create(Client client)
        {
            db.Clients.Add(client);
        }

        public void Delete(int id)
        {
            Client client = Get(id);
            if (client != null)
                db.Clients.Remove(client);
        }

        public void Update(int id, Client item)
        {
            Client client = Get(id);
            if (client != null)
            {
                client.Name = item.Name;
                client.Surname = item.Surname;
            }
        }
    }
}
