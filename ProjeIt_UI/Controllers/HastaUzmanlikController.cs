using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjeIt_UI.Api;
using ProjeIt_UI.Helpers;
using ProjeIt_UI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjeIt_UI.Controllers
{
    public class HastaUzmanlikController : Controller
    {
        private readonly ILogger<HastaUzmanlikController> _logger;
        private readonly IRequest _request;
        private readonly ISessionHelper _sessionhelper;
        public HastaUzmanlikController(ILogger<HastaUzmanlikController> logger,
                  IRequest request,
                ISessionHelper sessionhelper)
        {
            _logger = logger;
            _request = request;
            _sessionhelper = sessionhelper;

        }

        public IActionResult HastaUzmanlikList()
        {
            string SessionData = _sessionhelper.GetSessionModel("User");
            SeesionModel SessionObject = JsonConvert.DeserializeObject<SeesionModel>(SessionData);
            ViewData["UserRole"] = SessionObject;
            var HastaUzmanlik = _request.GetAsync<List<HastaUzmanlik>>("", "HastaUzmanlik/getall").Result.ToList();
            return View(HastaUzmanlik);

        }
        public IActionResult HastaUzmanlikCreate(int Id)
        {
            var HastaUzmanlik = _request.GetAsync<HastaUzmanlik>("", "HastaUzmanlik/getbyid?HastaUzmanlikId=" + Id).Result;
            return View(HastaUzmanlik);
        }
        [HttpPost]
        public IActionResult HastaUzmanlikCreate(HastaUzmanlik hastaUzmanlik)
        {
            var HastaUzmanlik = _request.GetAsync<HastaUzmanlik>("", "HastaUzmanlik/getbyid?HastaUzmanlikId=").Result;
            _request.PostAsync("", "HastaUzmanlik/add", hastaUzmanlik);
            return RedirectToAction("HastaUzmanlikList");
        }
        public IActionResult HastaUzmanlikDelete(int Id)
        {
            var hastaUzmanlik = _request.GetAsync<HastaUzmanlik>("", "HastaUzmanlik/getbyid?HastaUzmanlikId=" + Id).Result;
            _request.PostAsync("", "HastaUzmanlik/delete", hastaUzmanlik);
            return RedirectToAction("HastaUzmanlikList");
        }
        public IActionResult HastaUzmanlikEdit(int Id)
        {
            var HastaUzmanlik = _request.GetAsync<HastaUzmanlik>("", "HastaUzmanlik/getbyid?HastaUzmanlikId=" + Id).Result;
            return View(HastaUzmanlik);
        }
        [HttpPost]
        public IActionResult HastaUzmanlikEdit(HastaUzmanlik hastaUzmanlik)
        {
            var _hastaUzmanlik = _request.GetAsync<HastaUzmanlik>("", "HastaUzmanlik/getbyid?HastaUzmanlikId=" + hastaUzmanlik.HastaUzmanlikId).Result;
            _hastaUzmanlik.HastaUzmanlikAdi = hastaUzmanlik.HastaUzmanlikAdi;
            _request.PostAsync("", "HastaUzmanlik/Update", _hastaUzmanlik);
            return RedirectToAction("HastaUzmanlikList");

        }

    }
}
