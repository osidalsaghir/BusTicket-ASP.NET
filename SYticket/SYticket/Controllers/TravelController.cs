using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using SYticket.Models;

namespace SYticket.Controllers
{
    public class TravelController : Controller
    {
        // GET: Travel
        Syticket syt = new Syticket();


        [Authorize(Roles = "c")]
        public ActionResult Index()
        {
            string userEmail = HttpContext.User.Identity.Name;
            int u = Convert.ToInt32(syt.Users.FirstOrDefault(x => x.user_Email == userEmail).companyLogin_ID);
            
            List<Travel> t = syt.Travels.Where(x=>x.company_ID == u).ToList();
            ViewBag.t = t;
            return View();
        }

        [Authorize(Roles = "a")]
        public ActionResult ListTravelCompany(int id)
        {

            List<Travel> t = syt.Travels.Where(x => x.company_ID == id).ToList();
            ViewBag.t = t;
            return View("Index");
        }

        [Authorize(Roles = "c")]
        public ActionResult AddTravel()
        {

            return View();
        }
        [Authorize(Roles = "c")]
        [HttpPost]
        public ActionResult AddCompanyTravel(Travel t , string timestart , string timeend )
        {
            string userEmail = HttpContext.User.Identity.Name;
            int u = Convert.ToInt32(syt.Users.FirstOrDefault(x => x.user_Email == userEmail).companyLogin_ID);
            Travel tr = new Travel();
            string startDateAndTime = t.endsAt.ToString("yyyy-MM-dd") + " " + timeend;
            string endDateAndTime = t.startsAt.ToString("yyyy-MM-dd") + " " + timestart;
            tr.company_ID = u;
            tr.from_city = t.from_city;
            tr.to_city = t.to_city;
            tr.endsAt = DateTime.ParseExact(startDateAndTime, "yyyy-MM-dd HH:mm", null);
            tr.startsAt = DateTime.ParseExact(endDateAndTime, "yyyy-MM-dd HH:mm", null);
            tr.maxPassengers = t.maxPassengers;
            tr.price = t.price;
            syt.Travels.Add(tr);
            syt.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "c , a")]
        public ActionResult DeleteTravel(int id)
        {


            string userEmail = HttpContext.User.Identity.Name;
            int cID = Convert.ToInt32(syt.Users.FirstOrDefault(x => x.user_Email == userEmail).companyLogin_ID);


            Travel tr = syt.Travels.FirstOrDefault(x => x.Travel_ID == id);
            if(tr.company_ID == cID)
            {
                List<reservation> r = syt.reservations.Where(x => x.Travel_ID == tr.Travel_ID).ToList();
                foreach (reservation res in r)
                {
                    syt.reservations.Remove(res);
                    syt.SaveChanges();
                }
                int cm_ID = tr.company_ID;
                syt.Travels.Remove(tr);
                syt.SaveChanges();

                if (Roles.GetRolesForUser(userEmail).FirstOrDefault() == "a")
                {
                    return RedirectToAction("ListTravelCompany", new { id = cm_ID });
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}