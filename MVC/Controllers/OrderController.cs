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
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            List<CreateOrderHeaderViewModel> orders = new List<CreateOrderHeaderViewModel>();
            using (HttpClient client = new HttpClient())
            {
                var AccessToken = HttpContext.Session.GetString("JWTToken");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
                client.BaseAddress = new Uri("http://localhost:19516/api/");
                var response = client.GetAsync("Order");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync().Result;
                    orders = JsonConvert.DeserializeObject < List < CreateOrderHeaderViewModel>> (data);
                }

            }
            
            return View(orders);
        }

        public IActionResult Create(CreateOrderHeaderViewModel orderViewModel)
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                if (ModelState.IsValid == true)
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:19516/api/");

                        var AccessToken = HttpContext.Session.GetString("JWTToken");
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);


                        string data = JsonConvert.SerializeObject(orderViewModel );
                        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = client.PostAsync("Order", content).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        return RedirectToAction("Index", "Cart");
                    }
            }
            return RedirectToAction("Login", "Account");
        }

    
        public IActionResult Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/");
                var deleteTask = client.DeleteAsync("Order/" + id);
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
