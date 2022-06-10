
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;
using ProjeIt_UI.Api;
using ProjeIt_UI.Helpers;
using ProjeIt_UI.Models;
using ProjeIt_UI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;



namespace ProjeIt_UI.Controllers
{
    public class ProjeTanimController : Controller
    {
        private readonly ILogger<ProjeTanimController> _logger;
        private readonly IRequest _request;
        private readonly ISessionHelper _sessionhelper;
        protected NavigationManager navigation { get; set; }

        public ProjeTanimController(ILogger<ProjeTanimController> logger,
                  IRequest request,
                  ISessionHelper sessionhelper)
        {
            _logger = logger;
            _request = request;
            _sessionhelper = sessionhelper;
        }

        public IActionResult ProjeTanimList()
        {
            string SessionData = _sessionhelper.GetSessionModel("User");
            SeesionModel SessionObject = JsonConvert.DeserializeObject<SeesionModel>(SessionData);
            ViewData["UserRole"] = SessionObject;
            var ProjeTanimDto = _request.GetAsync<List<ProjeTanimDto>>("", "ProjeTanim/GetListProje").Result.ToList();

            return View(ProjeTanimDto);
        }
        public IActionResult ProjeTanimCreate(int Id)
        {
            var ProjeTanim = _request.GetAsync<ProjeTanim>("", "ProjeTanim/getbyid?ProjeTanimId=" + Id).Result;
            var Sponsor = _request.GetAsync<List<Sponsor>>("", "Sponsor/getall").Result.ToList();
            var HastaUzmanlik = _request.GetAsync<List<HastaUzmanlik>>("", "HastaUzmanlik/getall").Result.ToList();
            var KanserTur = _request.GetAsync<List<KanserTur>>("", "KanserTur/getall").Result.ToList();
            var MateryalTip = _request.GetAsync<List<MateryalTip>>("", "MateryalTip/getall").Result.ToList();
            var TüpCins = _request.GetAsync<List<TüpCins>>("", "TüpCins/getall").Result.ToList();
            ViewBag.TüpCins = TüpCins;
            ViewBag.MateryalTip = MateryalTip;
            ViewBag.KanserTur = KanserTur;
            ViewBag.HastaUzmanlik = HastaUzmanlik;
            ViewBag.Sponsor = Sponsor;
            return View(ProjeTanim);
        }
        [HttpPost]
        public IActionResult ProjeTanimCreate(ProjeTanim projeTanim)
        {
            var ProjeTanim = _request.GetAsync<ProjeTanim>("", "ProjeTanim/getbyid?ProjeTanimId=").Result;
            _request.PostAsync("", "ProjeTanim/add", projeTanim);
            return RedirectToAction("ProjeTanimList");
        }
        public IActionResult ProjeTanimDelete(int Id)
        {
            var projeTanim = _request.GetAsync<ProjeTanim>("", "ProjeTanim/getbyid?ProjeTanimId=" + Id).Result;
            _request.PostAsync("", "ProjeTanim/delete", projeTanim);
            return RedirectToAction("ProjeTanimList");
        }
        public IActionResult ProjeTanimEdit(int Id)
        {
            var ProjeTanimDto = _request.GetAsync<List<ProjeTanimDto>>("", "ProjeTanim/GetListProje").Result.FirstOrDefault(x => x.ProjeTanimId == Id);
            var Sponsor = _request.GetAsync<List<Sponsor>>("", "Sponsor/getall").Result.ToList();
            var HastaUzmanlik = _request.GetAsync<List<HastaUzmanlik>>("", "HastaUzmanlik/getall").Result.ToList();
            var KanserTur = _request.GetAsync<List<KanserTur>>("", "KanserTur/getall").Result.ToList();
            var MateryalTip = _request.GetAsync<List<MateryalTip>>("", "MateryalTip/getall").Result.ToList();
            var TüpCins = _request.GetAsync<List<TüpCins>>("", "TüpCins/getall").Result.ToList();
            ViewBag.TüpCins = TüpCins;
            ViewBag.MateryalTip = MateryalTip;
            ViewBag.KanserTur = KanserTur;
            ViewBag.HastaUzmanlik = HastaUzmanlik;
            ViewBag.Sponsor = Sponsor;
            ViewBag.ProjeTanimDto = ProjeTanimDto;
            return View(ProjeTanimDto);

        }
        [HttpPost]
        public IActionResult ProjeTanimEdit(ProjeTanim projeTanim, int SponsorId, int HastaUzmanlikId, int KanserTurId, int MateryalTipId, int TüpCinsId)
        {
            var _projeTanim = _request.GetAsync<ProjeTanim>("", "ProjeTanim/getbyid?ProjeTanimId=" + projeTanim.ProjeTanimId).Result;
            _projeTanim.ProjeTanimAdi = projeTanim.ProjeTanimAdi;
            _projeTanim.SponsorId = SponsorId;
            _projeTanim.HastaUzmanlikId = HastaUzmanlikId;
            _projeTanim.KanserTurId = KanserTurId;
            _projeTanim.MateryalTipId = MateryalTipId;
            _projeTanim.TüpCinsId = TüpCinsId;
            _request.PostAsync("", "ProjeTanim/Update", _projeTanim);
            return RedirectToAction("ProjeTanimList");

        }
        [HttpGet]
        public IActionResult ExcelDisaAktar()
        {
            var ProjeTanimDto = _request.GetAsync<List<ProjeTanimDto>>("", "ProjeTanim/GetListProje").Result.ToList();
            string ExcelName = DateTime.Now.Date.Day + "." + DateTime.Now.Date.Month + "." + DateTime.Now.Date.Year;
            #region Excel
            byte[] fileContents;
            using (var package = new ExcelPackage())
            {
                #region Header
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells[1, 1].Value = "Proje Adı";
                worksheet.Cells[1, 2].Value = "Sponsor Adı";
                worksheet.Cells[1, 3].Value = "Hasta Uzmanlık";
                worksheet.Cells[1, 4].Value = "Kanser Türler";
                worksheet.Cells[1, 5].Value = "Materyal Tipi";
                worksheet.Cells[1, 6].Value = "Tüp Cinsi";


                worksheet.Cells[1, 1].Style.Font.Size = 12;
                worksheet.Column(1).Width = 13;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 2].Style.Font.Size = 12;
                worksheet.Column(2).Width = 20;
                worksheet.Cells[1, 2].Style.Font.Bold = true;
                worksheet.Cells[1, 3].Style.Font.Size = 12;
                worksheet.Column(3).Width = 20;
                worksheet.Cells[1, 3].Style.Font.Bold = true;
                worksheet.Cells[1, 4].Style.Font.Size = 12;
                worksheet.Column(4).Width = 20;
                worksheet.Cells[1, 4].Style.Font.Bold = true;
                worksheet.Cells[1, 5].Style.Font.Size = 12;
                worksheet.Column(5).Width = 20;
                worksheet.Cells[1, 5].Style.Font.Bold = true;
                worksheet.Cells[1, 6].Style.Font.Size = 12;
                worksheet.Column(6).Width = 20;
                worksheet.Cells[1, 6].Style.Font.Bold = true;


                #endregion

                int i = 2;

                foreach (var item in ProjeTanimDto)
                {
                    worksheet.Cells[i, 1].Value = item.ProjeTanimAdi;
                    worksheet.Cells[i, 2].Value = item.SponsorAdi;
                    worksheet.Cells[i, 3].Value = item.HastaUzmanlikAdi;
                    worksheet.Cells[i, 4].Value = item.KanserTurAdi;
                    worksheet.Cells[i, 5].Value = item.MateryalTipAdi;
                    worksheet.Cells[i, 6].Value = item.TüpCinsAdi;

                    i = i + 1;
                }
                fileContents = package.GetAsByteArray();
            }
            if (fileContents == null || fileContents.Length == 0)
            {
                return NotFound();
            }
            return File(
            fileContents: fileContents,
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: ExcelName + "ProjeListesi.xlsx"
            );
            #endregion
        }

    }
}
