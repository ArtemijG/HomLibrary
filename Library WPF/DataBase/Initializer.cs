using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Library_WPF.DataBase
{
    class Initializer:DropCreateDatabaseIfModelChanges<Context> //DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context db)
        {
            Author a1 = new Author { Id = 1, Name = "Cory" };
            Author a2 = new Author { Id = 2, Name = "Arny" };
            db.Authors.Add(a1);
            db.Authors.Add(a2);
            db.SaveChanges();

            Genres g1 = new Genres { Id = 1, Name = "Приключение" };
            Genres g2 = new Genres { Id = 2, Name = "Хорор" };
            Genres g3 = new Genres { Id = 3, Name = "Драма" };
            Genres g4 = new Genres { Id = 4, Name = "Фантастика" };
            db.Genres.AddRange(new List<Genres>() { g1, g2, g3, g4 });
            db.SaveChanges();

            Book b1 = new Book { Id = 1, Name = "Groovy", Year = 20015, Description = "", Genre = g1, Quantity = 1, Author = a1 };
            Book b2 = new Book { Id = 2, Name = "My cute nynya", Year = 2017, Description = "", Genre = g3, Quantity = 1, Author = a2 };
            db.Books.AddRange(new List<Book>() { b1,b2});
            //db.Books.Add(b1);
            db.SaveChanges();


            Reader r1 = new Reader { Id = 1, Name = "Joe" };
            Reader r2 = new Reader { Id = 2, Name = "Bro" };
            db.Readers.Add(r1);
            db.Readers.Add(r2);
            db.SaveChanges();

            
            DateTime d1 = new DateTime(2016, 7, 20);
            DateTime d2 = new DateTime(2016, 11, 09);

            Library l1 = new Library { Id = 1, DateOfTook = d1, DateOfReturn = d2, Book = b1, Reader = r1 };

            d1 = new DateTime(2016, 7, 23);
            d2 = new DateTime(2016, 11, 10);
            Library l2 = new Library { Id = 2, DateOfTook = d1, DateOfReturn = d2, Book = b2, Reader = r1 };

            db.Libraries.Add(l1);
            db.Libraries.Add(l2);
            db.SaveChanges();
            
            
        }
    }
}
