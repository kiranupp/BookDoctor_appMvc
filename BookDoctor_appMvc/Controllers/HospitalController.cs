using BookDoctor_appMvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BookDoctor_appMvc.Controllers
{
    public class HospitalController : Controller
    {

        Uri baseaddress = new Uri("https://localhost:44314/api");
        HttpClient client;

        public HospitalController()
        {
            client = new HttpClient();
            client.BaseAddress = baseaddress;

        }
        public ActionResult Index(int? id)
        {
            List<SelectListItem> locationlist = GetLocations();
            List<SelectListItem> Specialtylist = GetSpecialties();
            ViewBag.location = locationlist;
            ViewBag.Specialty = Specialtylist;
            ViewBag.Userid = id;
            return View();
        }
        public List<SelectListItem> GetLocations()
        {
            List<SelectListItem> locationlist = new List<SelectListItem>();
            HttpResponseMessage r = client.GetAsync(client.BaseAddress + "/Hospital/GetLocations").Result;
            string data = r.Content.ReadAsStringAsync().Result;
            locationlist = JsonConvert.DeserializeObject<List<SelectListItem>>(data);
            return locationlist;
        }

        public List<SelectListItem> GetSpecialties()
        {
            List<SelectListItem> Specialtylist = new List<SelectListItem>();
            HttpResponseMessage r = client.GetAsync(client.BaseAddress + "/Hospital/GetSpecialties").Result;
            string data = r.Content.ReadAsStringAsync().Result;
            Specialtylist = JsonConvert.DeserializeObject<List<SelectListItem>>(data);
            return Specialtylist;
        }

        public ActionResult AllDctor()
        {
            List<Alldoctor> listuser = new List<Alldoctor>();
            HttpResponseMessage r = client.GetAsync(client.BaseAddress + "/Hospital/GetAllDctor").Result;
            string data = r.Content.ReadAsStringAsync().Result;
            listuser = JsonConvert.DeserializeObject<List<Alldoctor>>(data);
            return View(listuser);
        }
        public ActionResult GetListofDoctorforselected(int locid, int spid, int? userid)
        {
            Session["userid"] = userid;
            ViewBag.Userid = userid;
            TempData["Userid"] = userid;
            List<Alldoctor> listofdoctor = new List<Alldoctor>();
            string url = $"/Hospital/GetListofDoctorforselected/?locid={locid}&spid={spid}&userid={userid}";
            HttpResponseMessage r = client.GetAsync(client.BaseAddress + url).Result;
            string data = r.Content.ReadAsStringAsync().Result;
            listofdoctor = JsonConvert.DeserializeObject<List<Alldoctor>>(data);
            return View(listofdoctor);
        }




        public ActionResult Bookappointment(int id)
        {
            object Userid = Session["Userid"];
            int ids = (int)Userid;

            List<SelectListItem> Timeslot = Gettimeslot();
            ViewBag.Timeslot = Timeslot;
            ViewBag.Userid = ids;
            ViewBag.DoctorId = id;

            List<Appointments> listofdoctor = new List<Appointments>();
            string url = $"/Hospital/GetBookappointment/?id={id}";
            string f = client.BaseAddress + url;
            HttpResponseMessage r = client.GetAsync(f).Result;
            string data = r.Content.ReadAsStringAsync().Result;
            listofdoctor = JsonConvert.DeserializeObject<List<Appointments>>(data);
            return View(listofdoctor);
        }

        private static List<SelectListItem> Gettimeslot()
        {
            HospitalApplicationEntities entities = new HospitalApplicationEntities();
            List<SelectListItem> Specialtylist = (from p in entities.Timeslots.AsEnumerable()
                                                  select new SelectListItem
                                                  {
                                                      Text = p.timeslot1,
                                                      Value = p.id.ToString()
                                                  }).ToList();
            Specialtylist.Insert(0, new SelectListItem { Text = "--Select TimeSlot--", Value = "" });

            return Specialtylist;
        }

        public ActionResult Gettimeslotbyavailability(int doctorid, DateTime Date)
        {
            List<SelectListItem> availabletimeslots = new List<SelectListItem>();
            string url = $"/Hospital/Gettimeslotbyavailability/?doctorid={doctorid}&Date={Date}";
            string f = client.BaseAddress + url;
            HttpResponseMessage r = client.GetAsync(client.BaseAddress + url).Result;

            string data = r.Content.ReadAsStringAsync().Result;
            availabletimeslots = JsonConvert.DeserializeObject<List<SelectListItem>>(data);
            return Json(availabletimeslots, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Myappointments(int id)
        {
            Session["userid"] = id;
            ViewBag.Userid = id;
            List<Myappointments> my_pp = new List<Myappointments>();
            string url = $"/Hospital/GetMyappointments/?id={id}";
            HttpResponseMessage r = client.GetAsync(client.BaseAddress + url).Result;
            string data = r.Content.ReadAsStringAsync().Result;
            my_pp = JsonConvert.DeserializeObject<List<Myappointments>>(data);
            return View(my_pp);

        
        }
        public ActionResult BookappointmentSucess(DateTime date, int userid, int doctorid, int timeslotid)
        {
            Session["userid"] = userid;
            ViewBag.Id = userid;
           
            ArrayList  b = new ArrayList();
            Bookappointmentsucess ba = new Bookappointmentsucess { userid = userid, timeslotid = timeslotid, Date = date,doctorid= doctorid };
            b.Add(ba);
            string data = JsonConvert.SerializeObject(b);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage r = client.PostAsync(client.BaseAddress + "/Hospital/BookappointmentSucess", content).Result;
            ViewBag.Message = "Appointment Booked Sucessfully Please check My Appointments for more details";
            return View("BookappointmentSucess");
        }

        public ActionResult CancelAppointment(int id)
        {
            object Userid = Session["Userid"];
            int ids = (int)Userid;
            ViewBag.Id = ids;
            string url = $"/Hospital/CancelAppointment/?id={id}";
            HttpResponseMessage r = client.DeleteAsync(client.BaseAddress + url).Result;
            string data = r.Content.ReadAsStringAsync().Result;

            ViewBag.Message = "Appointment Cancelled sucessfully..";
            return View("CancelAppointment");


        }
        public ActionResult Patientappointments(int id)
        {
            Session["userid"] = id;
            ViewBag.Userid = id;
            List<Myappointments> my_pp = new List<Myappointments>();
            string url = $"/Hospital/GetPatientappointments/?id={id}";
            string f = client.BaseAddress + url;
            HttpResponseMessage r = client.GetAsync(f).Result;
            string data = r.Content.ReadAsStringAsync().Result;
            my_pp = JsonConvert.DeserializeObject<List<Myappointments>>(data);
            return View(my_pp);
        }

    }
}