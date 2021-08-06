using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nix_Hotel.WEB.Models
{
    public class ClientModel
    {
        [Required]
        public int Id
        {
            get;
            set;
        }

        [Required]
        public string Name
        {
            get;
            set;
        }

        [Required]
        public string Surname
        {
            get;
            set;
        }

        public string ClientFullName
        {
            get
            {
                return $"{Surname} {Name}";
            }
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