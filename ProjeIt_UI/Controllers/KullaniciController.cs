using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjeIt_UI.Api;
using ProjeIt_UI.Helpers;
using ProjeIt_UI.Models;
using ProjeIt_UI.Models.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace ProjeIt_UI.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly ILogger<KullaniciController> _logger;
        private readonly IRequest _request;
        private readonly ISessionHelper _sessionhelper;
        public KullaniciController(ILogger<KullaniciController> logger,
                  IRequest request,
                  ISessionHelper sessionhelper)
        {
            _logger = logger;
            _request = request;
            _sessionhelper = sessionhelper;

        }

        public IActionResult KullaniciList()
        {
            string SessionData = _sessionhelper.GetSessionModel("User");
            SeesionModel SessionObject = JsonConvert.DeserializeObject<SeesionModel>(SessionData);
            ViewData["UserRole"] = SessionObject;
            var KullaniciDto = _request.GetAsync<List<KullaniciDto>>("", "Kullanici/GetListKullanici").Result.ToList();

            return View(KullaniciDto);

        }
        public IActionResult KullaniciCreate(int Id)
        {
            var Kullanici = _request.GetAsync<Kullanici>("", "Kullanici/getbyid?KullaniciId=" + Id).Result;
            var Rol = _request.GetAsync<List<Rol>>("", "Rol/getall").Result.ToList();
            ViewBag.Rol = Rol;
            return View(Kullanici);
        }
        [HttpPost]
        public IActionResult KullaniciCreate(Kullanici kullanici)
        {
            var Kullanici = _request.GetAsync<Kullanici>("", "Kullanici/getbyid?KullaniciId=").Result;
            _request.PostAsync("", "Kullanici/add", kullanici);
            return RedirectToAction("KullaniciList");
        }
        public IActionResult KullaniciDelete(int Id)
        {

            var kullanici = _request.GetAsync<Kullanici>("", "Kullanici/getbyid?KullaniciId=" + Id).Result;
            _request.PostAsync("", "Kullanici/delete", kullanici);
            return RedirectToAction("KullaniciList");
        }
        public IActionResult KullaniciEdit(int Id)
        {
            var KullaniciDto = _request.GetAsync<List<KullaniciDto>>("", "Kullanici/GetListKullanici").Result.FirstOrDefault(x => x.KullaniciId == Id);
            var Kullanici = _request.GetAsync<Kullanici>("", "Kullanici/getbyid?KullaniciId=" + Id).Result;
            var Rol = _request.GetAsync<List<Rol>>("", "Rol/getall").Result.ToList();
            ViewBag.KullaniciDto=KullaniciDto;
            ViewBag.Rol = Rol;
            return View(Kullanici);

        }
        [HttpPost]
        public IActionResult KullaniciEdit(Kullanici kullanici,int RolId)
        {

            var _kullanici = _request.GetAsync<Kullanici>("", "Kullanici/getbyid?KullaniciId=" + kullanici.KullaniciId).Result;
            _kullanici.AdiSoyadi = kullanici.AdiSoyadi;
            _kullanici.KullaniciAdi = kullanici.KullaniciAdi;
            _kullanici.Sifre = kullanici.Sifre;
            _kullanici.RolId = RolId;
            _request.PostAsync("", "Kullanici/Update", _kullanici);
            return RedirectToAction("KullaniciList");

        }



    }
}
