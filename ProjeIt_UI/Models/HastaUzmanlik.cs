using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeIt_UI.Models
{
    public class HastaUzmanlik
    {
        [Key]
        public int HastaUzmanlikId { get; set; }
        public string HastaUzmanlikAdi { get; set; }   
    }
}
