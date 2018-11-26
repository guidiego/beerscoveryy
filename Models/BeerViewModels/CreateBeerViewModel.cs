using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace beerscovery.Models.BeerViewModels
{
    public class CreateBeerViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Photo { get; set; }

        [Required]
        public string Kind { get; set; }
    }
}
