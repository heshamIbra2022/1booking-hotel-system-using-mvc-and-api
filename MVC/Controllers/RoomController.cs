using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace MVC.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            List<RoomViewModel> rooms = new List<RoomViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/Room/");
                var response = client.GetAsync("getRooms");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync().Result;
                    rooms = JsonConvert.DeserializeObject<List<RoomViewModel>>(data);
                }

            }
            return View(rooms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            BranchesWithRoomTypesViewModel branchesWithTypes = new BranchesWithRoomTypesViewModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/Room/");
                var response = client.GetAsync("getBranchesWithRoomTypes");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync().Result;
                    branchesWithTypes = JsonConvert.DeserializeObject<BranchesWithRoomTypesViewModel>(data);
                }

            }
            ViewBag.Branches = new SelectList(branchesWithTypes.ListBranches, "Id", "Name");
            ViewBag.RoomTypes= new SelectList(branchesWithTypes.ListRoomTypes, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(RoomViewModel roomViewModel)
        {
            if (ModelState.IsValid == true)
            {
                var files = Request.Form.Files;
                byte[] photo = null;
                using(var fileStream = files[0].OpenReadStream())
                {
                    using(var memoryStream=new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        photo=memoryStream.ToArray();
                    }
                }

                roomViewModel.PictureImg=photo;




                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:19516/api/Room/");
                    string data = JsonConvert.SerializeObject(roomViewModel);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("addnewRoom", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Create", roomViewModel);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
           RoomViewModel roomViewModel = null;
            BranchesWithRoomTypesViewModel branchesWithTypes = new BranchesWithRoomTypesViewModel();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/Room/");
                var response = client.GetAsync("getBranchesWithRoomTypes");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync().Result;
                    branchesWithTypes = JsonConvert.DeserializeObject<BranchesWithRoomTypesViewModel>(data);
                }

            }
            ViewBag.Branches = branchesWithTypes.ListBranches;
            ViewBag.RoomTypes = branchesWithTypes.ListRoomTypes;

            using (var client1 = new HttpClient())
            {
                client1.BaseAddress = new Uri("http://localhost:19516/api/");
                //HTTP GET
                var responseTask1 = client1.GetAsync("Room/" + id.ToString());
                responseTask1.Wait();

                var result1 = responseTask1.Result;
                if (result1.IsSuccessStatusCode)
                {
                    var data1 = result1.Content.ReadAsStringAsync().Result;
                    roomViewModel = JsonConvert.DeserializeObject<RoomViewModel>(data1);
                }
            }

            return View(roomViewModel);
        }

        [HttpPost]
        public IActionResult Edit(RoomViewModel roomViewModel)
        {

            var files = Request.Form.Files;
            byte[] photo = null;
            using (var fileStream = files[0].OpenReadStream())
            {
                using (var memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    photo = memoryStream.ToArray();
                }
            }

            roomViewModel.PictureImg = photo;







            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/");

                string data = JsonConvert.SerializeObject(roomViewModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = client.PutAsync("Room", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Create");
            }


        }


        public IActionResult Details(int id)
        {
            RoomViewModel roomViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Room/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync().Result;
                    roomViewModel = JsonConvert.DeserializeObject<RoomViewModel>(data);
                }
            }

            ViewBag.Room=roomViewModel;
            CreateCartViewModel cartViewModel = new CreateCartViewModel
            {
                RoomId = id,
                CustomerId = HttpContext.Session.GetString("UserId"),


            };
           
            return View(cartViewModel);  
        }

        public IActionResult Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/");
                var deleteTask = client.DeleteAsync("Room/" + id);
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
            }
            return RedirectToAction("index");
        }
    }
}
