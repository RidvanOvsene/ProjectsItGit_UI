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
    public class MateryalTipController : Controller
    {
        private readonly ILogger<MateryalTipController> _logger;
        private readonly IRequest _request;
        private readonly ISessionHelper _sessionhelper;
        public MateryalTipController(ILogger<MateryalTipController> logger,
                  IRequest request, ISessionHelper sessionhelper)

        {
            _logger = logger;
            _request = request;
            _sessionhelper = sessionhelper;
        }

        public IActionResult MateryalTipList()
        {
            string SessionData = _sessionhelper.GetSessionModel("User");
            SeesionModel SessionObject = JsonConvert.DeserializeObject<SeesionModel>(SessionData);
            ViewData["UserRole"] = SessionObject;
            var MateryalTip = _request.GetAsync<List<MateryalTip>>("", "MateryalTip/getall").Result.ToList();

            return View(MateryalTip);
        }
        public IActionResult MateryalTipCreate(int Id)
        {
            var MateryalTip = _request.GetAsync<MateryalTip>("", "MateryalTip/getbyid?MateryalTipId=" + Id).Result;

            return View(MateryalTip);
        }
        [HttpPost]
        public IActionResult MateryalTipCreate(MateryalTip materyalTip)
        {
            var MateryalTip = _request.GetAsync<MateryalTip>("", "MateryalTip/getbyid?MateryalTipId=").Result;
            _request.PostAsync("", "MateryalTip/add", materyalTip);
            return RedirectToAction("MateryalTipList");
        }
        public IActionResult MateryalTipDelete(int Id)
        {
            var materyalTip = _request.GetAsync<MateryalTip>("", "MateryalTip/getbyid?MateryalTipId=" + Id).Result;
            _request.PostAsync("", "MateryalTip/delete", materyalTip);
            return RedirectToAction("MateryalTipList");
        }
        public IActionResult MateryalTipEdit(int Id)
        {
            var MateryalTip = _request.GetAsync<MateryalTip>("", "MateryalTip/getbyid?MateryalTipId=" + Id).Result;
            return View(MateryalTip);

        }
        [HttpPost]
        public IActionResult MateryalTipEdit(MateryalTip materyalTip)
        {
            var _materyalTip = _request.GetAsync<MateryalTip>("", "MateryalTip/getbyid?MateryalTipId=" + materyalTip.MateryalTipId).Result;
            _materyalTip.MateryalTipAdi = materyalTip.MateryalTipAdi;
            _request.PostAsync("", "MateryalTip/Update", _materyalTip);
            return RedirectToAction("MateryalTipList");
        }

    }
}
