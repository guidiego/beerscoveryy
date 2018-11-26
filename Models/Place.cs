
using System;
using System.ComponentModel.DataAnnotations;

namespace beerscovery.Models
{
    public class Place
    {
        [Key]
        public int Id { get; set; }
        public string LocalName { get; set; }
        public string Address { get; set; }

        public string Lat { get; set;}
        public string Lon { get; set;}

    }
}