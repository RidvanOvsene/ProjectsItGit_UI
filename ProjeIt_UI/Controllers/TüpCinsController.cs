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
    public class TüpCinsController : Controller
    {
        private readonly ILogger<TüpCinsController> _logger;
        private readonly IRequest _request;
        private readonly ISessionHelper _sessionhelper;
        public TüpCinsController(ILogger<TüpCinsController> logger,
                  IRequest request, ISessionHelper sessionhelper)
        {
            _logger = logger;
            _request = request;
            _sessionhelper = sessionhelper;

        }
        public IActionResult TüpCinsList()
        {
            string SessionData = _sessionhelper.GetSessionModel("User");
            SeesionModel SessionObject = JsonConvert.DeserializeObject<SeesionModel>(SessionData);
            ViewData["UserRole"] = SessionObject;
            var TüpCins = _request.GetAsync<List<TüpCins>>("", "TüpCins/getall").Result.ToList();

            return View(TüpCins);

        }
        public IActionResult TüpCinsCreate(int Id)
        {
            var TüpCins = _request.GetAsync<TüpCins>("", "TüpCins/getbyid?TüpCinsId=" + Id).Result;

            return View(TüpCins);
        }
        [HttpPost]
        public IActionResult TüpCinsCreate(TüpCins tüpCins)
        {
            var TüpCins = _request.GetAsync<TüpCins>("", "TüpCins/getbyid?TüpCinsId=").Result;
            _request.PostAsync("", "TüpCins/add", tüpCins);
            return RedirectToAction("TüpCinsList");
        }
        public IActionResult TüpCinsDelete(int Id)
        {
            var tüpCins = _request.GetAsync<TüpCins>("", "TüpCins/getbyid?TüpCinsId=" + Id).Result;
            _request.PostAsync("", "TüpCins/delete", tüpCins);
            return RedirectToAction("TüpCinsList");
        }
        public IActionResult TüpCinsEdit(int Id)
        {
            var TüpCins = _request.GetAsync<TüpCins>("", "TüpCins/getbyid?TüpCinsId=" + Id).Result;

            return View(TüpCins);
        }
        [HttpPost]
        public IActionResult TüpCinsEdit(TüpCins tüpCins)
        {
            var _tüpCins = _request.GetAsync<TüpCins>("", "TüpCins/getbyid?TüpCinsId=" + tüpCins.TüpCinsId).Result;
            _tüpCins.TüpCinsAdi = tüpCins.TüpCinsAdi;
            _request.PostAsync("", "TüpCins/Update", _tüpCins);
            return RedirectToAction("TüpCinsList");
        }



    }
}
