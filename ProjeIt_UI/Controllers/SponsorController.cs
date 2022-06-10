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
    public class SponsorController : Controller
    {
        private readonly ILogger<SponsorController> _logger;
        private readonly IRequest _request;
        private readonly ISessionHelper _sessionhelper;
        public SponsorController(ILogger<SponsorController> logger,
                  IRequest request, ISessionHelper sessionhelper)
        {
            _logger = logger;
            _request = request;
            _sessionhelper = sessionhelper;
        }

        public IActionResult SponsorList()
        {
            string SessionData = _sessionhelper.GetSessionModel("User");
            SeesionModel SessionObject = JsonConvert.DeserializeObject<SeesionModel>(SessionData);
            ViewData["UserRole"] = SessionObject;
            var Sponsor = _request.GetAsync<List<Sponsor>>("", "Sponsor/getall").Result.ToList();

            return View(Sponsor);
        }
        public IActionResult SponsorCreate(int Id)
        {
            var Sponsor = _request.GetAsync<Sponsor>("", "Sponsor/getbyid?SponsorId=" + Id).Result;
            return View(Sponsor);
        }
        [HttpPost]
        public IActionResult SponsorCreate(Sponsor sponsor)
        {
            var Sponsor = _request.GetAsync<Sponsor>("", "Sponsor/getbyid?SponsorId=").Result;
            _request.PostAsync("", "Sponsor/add", sponsor);
            return RedirectToAction("SponsorList");
        }
        public IActionResult SponsorDelete(int Id)
        {
            var sponsor = _request.GetAsync<Sponsor>("", "Sponsor/getbyid?SponsorId=" + Id).Result;
            _request.PostAsync("", "Sponsor/delete", sponsor);
            return RedirectToAction("SponsorList");
        }
        public IActionResult SponsorEdit(int Id)
        {
            var Sponsor = _request.GetAsync<Sponsor>("", "Sponsor/getbyid?SponsorId=" + Id).Result;

            return View(Sponsor);

        }
        [HttpPost]
        public IActionResult SponsorEdit(Sponsor sponsor)
        {
            var _sponsor = _request.GetAsync<Sponsor>("", "Sponsor/getbyid?SponsorId=" + sponsor.SponsorId).Result;
            _sponsor.SponsorAdi = sponsor.SponsorAdi;

            _request.PostAsync("", "Sponsor/Update", _sponsor);
            return RedirectToAction("SponsorList");

        }

    }
}
