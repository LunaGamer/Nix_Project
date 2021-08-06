using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.BLL.DTO
{
    public class AdministratorDTO
    {
        public int Id
        {
            get;
            set;
        }

        [Required]
        public string Login
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }
    }
}
