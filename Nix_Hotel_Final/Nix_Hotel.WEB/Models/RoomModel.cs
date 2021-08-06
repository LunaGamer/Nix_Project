using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nix_Hotel.WEB.Models
{
    public class RoomModel
    {
        [Required]
        public int Id
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Room")]
        public string Name
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Room Category")]
        public CategoryModel Category
        {
            get;
            set;
        }

        [DisplayName("Room Free")]
        public bool Active
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (obj is RoomModel)
            {
                var room = obj as RoomModel;
                return this.Id == room.Id &&
                    this.Name == room.Name &&
                    this.Category.Id == room.Category.Id &&
                    this.Active == room.Active;
            }
            return base.Equals(obj);
        }
    }
}