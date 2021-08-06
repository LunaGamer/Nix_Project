using Nix_Hotel.BLL.DTO;
using Nix_Hotel.BLL.Infrastructure;
using Nix_Hotel.BLL.Interfaces;
using Nix_Hotel.DAL.EF.AuthContext;
using Nix_Hotel.DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.BLL.Services
{
    public class AdministratorService : IAdministratorService
    {
        private AuthContext Database
        {
            get;
            set;
        }

        public AdministratorService()
        {
            this.Database = new AuthContext();
        }

        public AdministratorDTO Login(AdministratorDTO adminDTO)
        {
            var admin = Database.Administrators.FirstOrDefault(x => x.Login == adminDTO.Login && x.Password == adminDTO.Password);
            if (admin != null)
            {
                return HotelMapperBLL.mapperAdministrator.Map<Administrator, AdministratorDTO>(admin);
            }
            return null;
        }
    }
}
