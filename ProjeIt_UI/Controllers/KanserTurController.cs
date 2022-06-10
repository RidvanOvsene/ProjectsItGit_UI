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
    public class KanserTurController : Controller
    {
        private readonly ILogger<KanserTurController> _logger;
        private readonly IRequest _request;
        private readonly ISessionHelper _sessionhelper;
        public KanserTurController(ILogger<KanserTurController> logger,
                  IRequest request,
                  ISessionHelper sessionhelper)
        {
            _logger = logger;
            _request = request;
            _sessionhelper = sessionhelper;

        }

        public IActionResult KanserTurList()
        {
            string SessionData = _sessionhelper.GetSessionModel("User");
            SeesionModel SessionObject = JsonConvert.DeserializeObject<SeesionModel>(SessionData);
            ViewData["UserRole"] = SessionObject;
            var KanserTur = _request.GetAsync<List<KanserTur>>("", "KanserTur/getall").Result.ToList();

            return View(KanserTur);
        }
        public IActionResult KanserTurCreate(int Id)
        {
            var KanserTur = _request.GetAsync<KanserTur>("", "KanserTur/getbyid?KanserTurId=" + Id).Result;

            return View(KanserTur);
        }
        [HttpPost]
        public IActionResult KanserTurCreate(KanserTur kanserTur)
        {
            var KanserTur = _request.GetAsync<KanserTur>("", "KanserTur/getbyid?KanserTurId=").Result;
            _request.PostAsync("", "KanserTur/add", kanserTur);
            return RedirectToAction("KanserTurList");
        }
        public IActionResult KanserTurDelete(int Id)
        {
            var kanserTur = _request.GetAsync<KanserTur>("", "KanserTur/getbyid?KanserTurId=" + Id).Result;
            _request.PostAsync("", "KanserTur/delete", kanserTur);
            return RedirectToAction("KanserTurList");
        }
        public IActionResult KanserTurEdit(int Id)
        {
            var KanserTur = _request.GetAsync<KanserTur>("", "KanserTur/getbyid?KanserTurId=" + Id).Result;

            return View(KanserTur);
        }
        [HttpPost]
        public IActionResult KanserTurEdit(KanserTur kanserTur)
        {
            var _kanserTur = _request.GetAsync<KanserTur>("", "KanserTur/getbyid?KanserTurId=" + kanserTur.KanserTurId).Result;
            _kanserTur.KanserTurAdi = kanserTur.KanserTurAdi;
            _request.PostAsync("", "KanserTur/Update", _kanserTur);
            return RedirectToAction("KanserTurList");

        }

    }
}
