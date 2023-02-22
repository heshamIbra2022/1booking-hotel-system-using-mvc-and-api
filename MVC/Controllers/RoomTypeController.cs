using Microsoft.AspNetCore.Mvc;
using MVC.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MVC.Controllers
{
    public class RoomTypeController : Controller
    {
        public IActionResult Index()
        {
            List<RoomTypeViewModel> roomTypes = new List<RoomTypeViewModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/RoomType/");
                var response = client.GetAsync("getAllRomTypes");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync().Result;
                    roomTypes = JsonConvert.DeserializeObject<List<RoomTypeViewModel>>(data);
                }

            }


            return View(roomTypes);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(RoomTypeViewModel roomTypeViewModel)
        {
            if (ModelState.IsValid == true)
            {

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:19516/api/RoomType/");
                    string data = JsonConvert.SerializeObject(roomTypeViewModel);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("addnewRoomType", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Create", roomTypeViewModel);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
          RoomTypeViewModel roomTypeViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/");
                //HTTP GET
                var responseTask = client.GetAsync("RoomType/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync().Result;
                    roomTypeViewModel = JsonConvert.DeserializeObject<RoomTypeViewModel>(data);
                }
            }

            return View(roomTypeViewModel);
        }

        public IActionResult Edit(RoomTypeViewModel roomTypeViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/");

                string data = JsonConvert.SerializeObject(roomTypeViewModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = client.PutAsync("RoomType", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Create");
            }

        }
            public IActionResult Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/");
                var deleteTask = client.DeleteAsync("RoomType/"+id);
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
