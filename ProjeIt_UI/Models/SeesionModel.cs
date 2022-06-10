using System.Collections.Generic;

namespace ProjeIt_UI.Models
{
    public class SeesionModel
    {
        public int? KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public UserRole Role { get; set; }
        public string Token { get; set; }
    }
    public class UserRole
    {
        public int? RolId { get; set; }
        public string RolAdi { get; set; }
     
    }

}
