using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SitefinityForums.Data;
using SitefinityForums.Web.Models;

namespace SitefinityForums.Web.Controllers
{
    public class HomeController : Controller
    {
        private IThreadsRepository threadsRepository;

        public HomeController(IThreadsRepository repo)
        {
            this.threadsRepository = repo;
        }

        public IActionResult Index()
        {
            var threads = threadsRepository.GetTodoThreads();
            return View(threads);
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
