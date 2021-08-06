using Nix_Hotel.DAL.Enteties;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Nix_Hotel.DAL.EF.AuthContext
{
    public partial class AuthContext : DbContext
    {
        public AuthContext()
            : base("name=AuthContext")
        {
        }

        public DbSet<Administrator> Administrators { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
