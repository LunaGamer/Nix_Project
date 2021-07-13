using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nix_Hotel.API.Models
{
    public class ClientModel
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Surname
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (obj is ClientModel)
            {
                var client = obj as ClientModel;
                return this.Id == client.Id &&
                    this.Name == client.Name &&
                    this.Surname == client.Surname;
            }
            return base.Equals(obj);
        }
    }
}