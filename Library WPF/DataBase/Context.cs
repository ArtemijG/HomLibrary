using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Library_WPF.DataBase
{
    class Context:DbContext
    {
        public Context():base("DbConnection")
        { }
        static Context()
        {
            Database.SetInitializer<Context>(new Initializer());
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Genres> Genres { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }



    }
}
