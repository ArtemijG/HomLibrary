using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;


namespace Library_WPF.DataBase
{
    
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public Genres Genre { get; set; }
        public int Quantity { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        


    }
}
