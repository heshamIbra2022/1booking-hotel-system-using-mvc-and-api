using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models;
using MVC.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<BranchViewModel> branches = new List<BranchViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/Branch/");
                var response = client.GetAsync("getAllBranches");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync().Result;
                    branches = JsonConvert.DeserializeObject<List<BranchViewModel>>(data);
                }

                ViewBag.Branches = branches;
                List<RoomViewModel> rooms = new List<RoomViewModel>();
                using (HttpClient client1 = new HttpClient())
                {
                    client1.BaseAddress = new Uri("http://localhost:19516/api/Room/");
                    var response1 = client1.GetAsync("getRooms");
                    response1.Wait();
                    var result1 = response1.Result;
                    if (result1.IsSuccessStatusCode)
                    {
                        var data1 = result1.Content.ReadAsStringAsync().Result;
                        rooms = JsonConvert.DeserializeObject<List<RoomViewModel>>(data1);
                    }
                }


                return View(rooms);
            }
        }

        [HttpPost]
        public IActionResult Index(int? BranchId)
        {
            List<BranchViewModel> branches = new List<BranchViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/Branch/");
                var response = client.GetAsync("getAllBranches");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync().Result;
                    branches = JsonConvert.DeserializeObject<List<BranchViewModel>>(data);
                }

                ViewBag.Branches = branches;
                List<RoomViewModel> rooms = new List<RoomViewModel>();
                using (HttpClient client1 = new HttpClient())
                {
                    client1.BaseAddress = new Uri("http://localhost:19516/api/Room/");
                    var response1 = client1.GetAsync("getRooms/"+ BranchId);
                    response1.Wait();
                    var result1 = response1.Result;
                    if (result1.IsSuccessStatusCode)
                    {
                        var data1 = result1.Content.ReadAsStringAsync().Result;
                        rooms = JsonConvert.DeserializeObject<List<RoomViewModel>>(data1);
                    }
                }


                return View(rooms);
            }
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
    }
}
