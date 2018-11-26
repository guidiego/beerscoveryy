
using System;
using System.ComponentModel.DataAnnotations;

namespace beerscovery.Models
{
    public class Score
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Quality { get; set; }

        [Required]
        public int Bitterness { get; set; }

        [Required]
        public int Beer { get; set; }

        [Required]
        public string User { get; set; }
    }
}