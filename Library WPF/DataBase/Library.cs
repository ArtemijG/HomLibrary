using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library_WPF.DataBase
{
    public class Library
    {
        public int Id { get; set; }

        
        public DateTime DateOfTook { get; set; }
        [Required(AllowEmptyStrings = true)]
        public DateTime DateOfReturn { get; set; }

        public Book Book { get; set; }
        public Reader Reader { get; set; }

        
    }
}
