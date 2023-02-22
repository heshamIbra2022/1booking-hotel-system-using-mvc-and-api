using Microsoft.AspNetCore.Mvc;
using MVC.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MVC.Controllers
{
    public class BranchController : Controller
    {
        
       
        public IActionResult Index()
        {

            List<BranchViewModel> branches = new List<BranchViewModel>();
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/Branch/");
                var response = client.GetAsync("getAllBranches");
                response.Wait();
                var result=response.Result;
                if(result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync().Result;
                    branches = JsonConvert.DeserializeObject<List<BranchViewModel>>(data);
                }

            }

           
            return View(branches);
        }
       [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create(BranchViewModel branchViewModel)
        {
            if (ModelState.IsValid == true)
            {

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:19516/api/Branch/");
                    string data = JsonConvert.SerializeObject(branchViewModel);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync("addnewBranch", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Create", branchViewModel);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            BranchViewModel branchViewModel = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Branch/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync().Result;
                   branchViewModel = JsonConvert.DeserializeObject<BranchViewModel>(data);
                }
            }

            return View(branchViewModel);
        }

        public IActionResult Edit(BranchViewModel branchViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/");

                string data = JsonConvert.SerializeObject(branchViewModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = client.PutAsync("Branch",content).Result;
               
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Create");
            }


            }
            [HttpDelete]
        public IActionResult Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19516/api/");
            var deleteTask=  client.DeleteAsync("Branch/"+id);
                deleteTask.Wait();
                var result=deleteTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
            }
            return RedirectToAction("index");
        }
    }
}
