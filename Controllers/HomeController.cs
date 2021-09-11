using _2C2PTechExam.Entity;
using _2C2PTechExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _2C2PTechExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IRepository<Invoice> repository;

        private readonly InvoiceContext context;


        public HomeController(ILogger<HomeController> logger, IRepository<Invoice> repository, InvoiceContext context)
        {
            _logger = logger;
            this.context = context;
            this.repository = repository;
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

                repository.UploadFile(Request.Form.Files[0]);

                //ViewBag.Countries = new List<NewsStyleUriParser>;
            
                return View("Index");
            }
            else
               
                return View("Index");
        }

    }
}
