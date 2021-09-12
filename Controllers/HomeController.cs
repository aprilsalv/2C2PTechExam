using _2C2PTechExam.Entity;
using _2C2PTechExam.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace _2C2PTechExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IRepository<Invoice> repository;

        private IWebHostEnvironment _env;

        private readonly InvoiceContext context;

        public HomeController(ILogger<HomeController> logger, IRepository<Invoice> repository, InvoiceContext context,  IWebHostEnvironment env)
        {
            _logger = logger;
            this.context = context;
            this.repository = repository;
            this._env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public  IActionResult ProcessForm()
        {
            if (ModelState.IsValid)
            {
                //ViewBag.TotalStudents = repository.UploadFile(Request.Form.Files[0]); ;
                // ViewBag.Message = repository.UploadFile(Request.Form.Files[0]);

                //ViewBag.Countries = new List<NewsStyleUriParser>;
                repository.RootFolder = this._env.ContentRootPath;
                ViewBag.Message = repository.UploadFile(Request.Form.Files[0]);
                return View("Index");
            }
            else
               
                return View("Index");
        }

    }
}
