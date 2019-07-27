using SRCD.Models;
using SRCD.Models.Interfaces;
using SRCD.Models.Repositorio;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRCD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClinicaRepositorio clinicaRepositorio = new ClinicaRepositorio(
            new Models.ApplicationDbContext());
        private readonly DrogariaRepositorio drogariaRepositorio = new DrogariaRepositorio(
            new Models.ApplicationDbContext());

        public HomeController()
        {
        }
        public async Task<ActionResult> Index()
        {
            var clinicas = await clinicaRepositorio.GetTop3();
            ViewBag.Drogarias = await drogariaRepositorio.GetTop3();
            return View(clinicas);
        }

        public ActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public ActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
