using BookDoctor_appMvc.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace BookDoctor_appMvc.Controllers
{
    public class LoginController : Controller
    {

        Uri baseaddress = new Uri("https://localhost:44314/api/");
        HttpClient client;

        public LoginController()
        {
            client = new HttpClient();
            client.BaseAddress = baseaddress;

        }
        public ActionResult Index()
        {

            return View();

        }
        public ActionResult Signup(Loginuser li)
        {
            return View(li);
        }

        public ActionResult SubmitData(Loginuser li)
        {
            string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                   @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(emailRegex);
            if (string.IsNullOrEmpty(li.Name) || string.IsNullOrEmpty(li.Emailid) || string.IsNullOrEmpty(li.Userpassword) || string.IsNullOrEmpty(li.c_pwd) || li.Userpassword != li.c_pwd|| !re.IsMatch(li.Emailid)|| li.Userpassword.Length < 5)
            {
                if (string.IsNullOrEmpty(li.Name))
                {
                    ModelState.AddModelError("Name", "Please Enter your Name");
                }
                if (string.IsNullOrEmpty(li.Emailid))
                {
                    ModelState.AddModelError("Emailid", "Please enter your Email Id");
                                   
                }

                else if(!re.IsMatch(li.Emailid))
                {
                    ModelState.AddModelError("Email", "Please enter correct email address");
                }
                if (string.IsNullOrEmpty(li.Userpassword))
                {
                    ModelState.AddModelError("Userpassword", "Please enter your password");
                }
                else if (li.Userpassword.Length < 5)
                {
                    ModelState.AddModelError("Userpassword", "Minimum length should be 5");
                }
                if (string.IsNullOrEmpty(li.c_pwd))
                {
                    ModelState.AddModelError("c_pwd", "Please enter your password again");
                }
               


              else  if (li.Userpassword != li.c_pwd)
                {
                    ModelState.AddModelError("c_pwd", "Password and confirm password not match please try again..");
                }

              


                return View("Signup");
            }

            else
            {
                string data = JsonConvert.SerializeObject(li);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage r = client.PostAsync(client.BaseAddress + "/Hospital/Signup", content).Result;

                if (r.IsSuccessStatusCode)
                {
                    @ViewBag.name = li.Emailid;


                    ViewBag.Message = "Sucessfully Registered Please Login to proced further..";
                    return View("login");
                }
            }
            return View("login");

        }
        [HttpGet]
        public ActionResult login(Loginuser li)
        {
            return View(li);
        }

        [HttpGet]
        public ActionResult LoginDoctor(Logindoctor li)
        {
            return View(li);
        }

        [HttpPost]
        public ActionResult LoginDoctors(Logindoctor li)
        {
            if (string.IsNullOrEmpty(li.Userpassword) || string.IsNullOrEmpty(li.Emailid))
            {
                if (string.IsNullOrEmpty(li.Emailid))
                {
                    ModelState.AddModelError("Emailid", "Please Enter your email id");
                }
                if (string.IsNullOrEmpty(li.Userpassword))
                {
                    ModelState.AddModelError("Userpassword", "Please enter your password");
                }
                return View("LoginDoctor");

            }

            else
            {
                
                string data = JsonConvert.SerializeObject(li);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage r = client.PostAsync(client.BaseAddress + "/Hospital/LoginDoctor", content).Result;
                int id = r.Content.ReadAsAsync<int>().Result;
                if (r.IsSuccessStatusCode)
                {
                    @ViewBag.name = li.Emailid;
                    ViewBag.Id = id;
                    return RedirectToAction("Patientappointments", "Hospital", new { id = id });
                }
                else
                    @ViewBag.Message = "Invalid Doctor account Please try again..";
                return View("LoginDoctor");

            }
        }
        public ActionResult Loginsearch(Loginuser li)
        {

            if (string.IsNullOrEmpty(li.Userpassword) || string.IsNullOrEmpty(li.Emailid))
            {
                if (string.IsNullOrEmpty(li.Emailid))
                {
                    ModelState.AddModelError("Emailid", "Please Enter your email id");
                }
                if (string.IsNullOrEmpty(li.Userpassword))
                {
                    ModelState.AddModelError("Userpassword", "Please enter your password");
                }

                return View("login");

            }

            else
            {

                string data = JsonConvert.SerializeObject(li);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage r = client.PostAsync(client.BaseAddress + "/Hospital/Login", content).Result;
                int id = r.Content.ReadAsAsync<int>().Result;
                if (r.IsSuccessStatusCode)
                {
                    @ViewBag.name = li.Emailid;
                    ViewBag.Id = id;

                    return View("loadlogin");

                }
                else
                {
                    ViewBag.data = "invalid user account";
                }

                return View("login");
            }
        }

        public ActionResult loadlogin()
        {

            return View();
        }

    }
}