using BookService.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public virtual IList<Author> Authors { get; set; }

        public string FullNames => Authors.Select(x => x.FullName).Aggregate((x,y)=> x + "\n " + y);

        [Range(1,10000)]
        public int Pages { get; set; }

        [MaxLength(30)]
        public string Publisher { get; set; }

        [Min(1980)]
        public int Year { get; set; }

        [Isbn]
        public string Isbn { get; set; }

        public  byte[] Image { get; set; }
    }
}
