
using System;
using System.ComponentModel.DataAnnotations;

namespace beerscovery.Models
{
    public class UserStats
    {
        [Key]
        public int Id { get; set; }
        public string Action { get; set; }


        public int BeerId { get; set; }
        public int PlaceId { get; set; }
        public string UserId { get; set; }
    }
}

