using Nix_Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.BLL.Interfaces
{
    public interface IClientService
    {
        IEnumerable<ClientDTO> GetAllClients();
        ClientDTO Get(int id);
        void Create(ClientDTO client);
        void Update(int id, ClientDTO client);
    }
}
