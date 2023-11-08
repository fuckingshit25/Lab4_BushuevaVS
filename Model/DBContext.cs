using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
namespace ClassLibrary_forDB.Model
{
    

    public partial class DBContext : DbContext
    {
        public DBContext(string ConectionString)
            : base("name=Lab4")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public DbSet<User> User { get; set; }
    }
}
