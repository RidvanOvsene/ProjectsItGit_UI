using ProjeIt_UI.Api;
using ProjeIt_UI.Helpers;
using ProjeIt_UI.Models;
using ProjeIt_UI.Models.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Services.Users;

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LinqToDB;

namespace ProjeIt_UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRequest _request;
        private readonly ISessionHelper _sessionhelper;
        public AccountController(ILogger<Kullanici> logger,
                                 IRequest request,
                                 ISessionHelper sessionhelper)

        {
            _request = request;
            _sessionhelper = sessionhelper;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Kullanici model)
        {
            SeesionModel _sessionModel = new SeesionModel();
            var User = _request.GetAsync<Kullanici>("", "Kullanici/Login?KullaniciAdi=" + model.KullaniciAdi + "&Sifre=" + model.Sifre).Result;

           if(User!=null)
            {
                var Role = _request.GetAsync<Rol>("", "Rol/getbyid?RolId=" + User.RolId).Result;
                var UserRole = new UserRole();
                if (Role != null)
                {
                    UserRole.RolAdi = Role.RolAdi;
                    UserRole.RolId = Role.RolId; 
                    _sessionModel.Role = UserRole;
                }
        
                _sessionModel.KullaniciId = User.KullaniciId;
                _sessionModel.KullaniciAdi = User.KullaniciAdi;
                _sessionhelper.SetSeesionModel(_sessionModel, "User");
                return RedirectToAction("ProjeTanimList", "ProjeTanim");
            }
            else
            {
                ViewBag.Error = "Lütfen kullanıcı adınızı ve şifrenizi kontrol ediniz";
                return View();
            }
        }
    
        [HttpGet]
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}

