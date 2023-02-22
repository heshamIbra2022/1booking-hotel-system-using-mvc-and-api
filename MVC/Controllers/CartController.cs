using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MVC.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            List<CartViewModel> carts = null; 
            using (var client = new HttpClient())
            {
                var AccessToken = HttpContext.Session.GetString("JWTToken");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
                client.BaseAddress = new Uri("http://localhost:19516/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Cart");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync().Result;
                    carts= JsonConvert.DeserializeObject<List<CartViewModel>>(data);
                }
            }
            
            ViewBag.Carts = carts;

            CreateOrderHeaderViewModel createOrderHeaderViewModel = new CreateOrderHeaderViewModel
            {
                CustomerId = HttpContext.Session.GetString("UserId"),
                DateOfOrder = DateTime.Now,

            };
            foreach (var cart in carts)
            {
                createOrderHeaderViewModel.OrderTotal += cart.TotalPrice;
            }
            

            
            return View(createOrderHeaderViewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateCartViewModel createCartvm)
        {
            if(HttpContext.Session.GetString("UserId") != null) { 
            if(ModelState.IsValid==true)
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/");
                string data = JsonConvert.SerializeObject(createCartvm);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("Cart", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index","Home");
                }
                  return RedirectToAction("Details", "Room", new {id=createCartvm.RoomId});
            }
            }
            return RedirectToAction("Login", "Account");

        }


        public IActionResult Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/");
                var deleteTask = client.DeleteAsync("Cart/" + id);
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
            }
            return RedirectToAction("index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            CreateCartViewModel CartViewModel = null;
           

            using (var client1 = new HttpClient())
            {
                client1.BaseAddress = new Uri("http://localhost:19516/api/");
                //HTTP GET
                var responseTask1 = client1.GetAsync("Cart/" + id.ToString());
                responseTask1.Wait();

                var result1 = responseTask1.Result;
                if (result1.IsSuccessStatusCode)
                {
                    var data1 = result1.Content.ReadAsStringAsync().Result;
                    CartViewModel = JsonConvert.DeserializeObject<CreateCartViewModel>(data1);
                }
            }

            return View(CartViewModel);
        }


        [HttpPost]
        public IActionResult Edit(CreateCartViewModel cartViewModel)
        {

           






            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/");

                string data = JsonConvert.SerializeObject(cartViewModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = client.PutAsync("Cart", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Edit", new {id=cartViewModel.Id});
            }


        }



    }
}
