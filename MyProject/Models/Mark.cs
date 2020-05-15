using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class Mark
    {
        [Key]
        public int Id { get; set; }
        [Required]

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Страна")]
        public string Country { get; set; }

        [DisplayName("Стоимость")]
        public string NominalValue { get; set; }

        [DisplayName("Год")]
        public string Year { get; set; }

        [DisplayName("Количество")]
        public string Count { get; set; }

        [DisplayName("Особенности")]
        public string Features { get; set; }
        
    }
}
