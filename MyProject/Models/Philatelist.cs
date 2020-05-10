using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhilatelistOnMVC.Models
{
    public class Philatelist
    {
        [Key]
        public int Id { get; set; }
        [Required]

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Страна")]
        public string Country { get; set; }

        [DisplayName("Контактные координаты")]
        public string ContactCoordinates { get; set; }

        [DisplayName("Наличие редких марок")]
        public string Availability { get; set; }

        
    }
}
