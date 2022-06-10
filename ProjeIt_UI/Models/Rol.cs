using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeIt_UI.Models
{
    public class Rol
    {
        [Key]
        public int RolId { get; set; }
        public string RolAdi { get; set; }
    }
}
