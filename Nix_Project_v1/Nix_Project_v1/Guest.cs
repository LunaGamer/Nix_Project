using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Project_v1
{
    class Guest //Class for guest information
    {
        public string FullName
        {
            get;
            private set;
        }

        public DateTime Birthdate
        {
            get;
            private set;
        }

        public string Address
        {
            get;
            private set;
        }

        public Guest(string name, DateTime birthdate, string address)
        {
            if (name != "" && address != "")
            {
                this.FullName = name;
                this.Birthdate = birthdate;
                this.Address = address;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
