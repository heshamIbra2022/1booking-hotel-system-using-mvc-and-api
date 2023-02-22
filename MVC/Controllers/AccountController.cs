using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MVC.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:19516/api/Account/");
                    string data = JsonConvert.SerializeObject(registerViewModel);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    var responseTask = client.PostAsync("Register", content);
                    responseTask.Wait();
                    var response = responseTask.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string JWTToken = response.Content.ReadAsStringAsync().Result;
                        SetTokens(JWTToken);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                       
                        ModelState.AddModelError(string.Empty, "server error problem");

                        return View("Register", registerViewModel);
                    }
                }
            }
            else
            {

                return View("Register", registerViewModel);
            }

        }

        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddAdmin(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:19516/api/Account/");
                    string data = JsonConvert.SerializeObject(registerViewModel);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    var responseTask = client.PostAsync("RegisterAdmin", content);
                    responseTask.Wait();
                    var response = responseTask.Result;

                    if (response.IsSuccessStatusCode)
                    {
                       
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {

                        ModelState.AddModelError(string.Empty, "server error problem");

                        return View("AddAdmin", registerViewModel);
                    }
                }
            }
            else
            {

                return View("Register", registerViewModel);
            }
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {

            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:19516/api/Account/");
                    string data = JsonConvert.SerializeObject(loginViewModel);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    var responseTask = client.PostAsync("Login", content);
                    responseTask.Wait();
                    var response = responseTask.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string JWTToken = response.Content.ReadAsStringAsync().Result;
                        SetTokens( JWTToken);

                        return RedirectToAction("Index","Home");
                    }
                    ModelState.AddModelError("", "Password or Name is wrong");
                    return View("Login", loginViewModel);
                }

              
            }
            else
            {
                ModelState.AddModelError("", "Password or Name is wrong");
                return View("Login", loginViewModel);
            }




        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");

        }

        private void SetTokens(string JWTToken)
        {
            HttpContext.Session.SetString("JWTToken", JWTToken);

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(JWTToken);
            var Role = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role).Value;
            HttpContext.Session.SetString("Role",Role);

            var UserId = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            HttpContext.Session.SetString("UserId", UserId);
            var UserName = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name).Value;
            HttpContext.Session.SetString("UserName", UserName);

        }
    }
}

