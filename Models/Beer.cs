
using System;
using System.ComponentModel.DataAnnotations;

namespace beerscovery.Models
{
    public class Beer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Kind { get; set; }

        public string CreatedBy { get; set; }
    }
}