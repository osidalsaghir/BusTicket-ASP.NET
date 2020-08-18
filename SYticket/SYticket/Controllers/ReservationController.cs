using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYticket.Models;

namespace SYticket.Controllers
{
    public class ReservationController : Controller
    {
        // GET: Reservation
        Syticket syt = new Syticket();

        [Authorize(Roles = "u")]
        [AcceptVerbs("GET", "POST")]
        public ActionResult Index(string from , string to , string date)
        {
            if(from == null || to ==null || date == null)
            {
                List<Travel> t = syt.Travels.ToList();
                ViewBag.t = t;
                return View();
            }
            else
            {
                var ft = new SYticketEntities();
                var t = ft.FindTravel(from, to, Convert.ToDateTime(date)).ToList();
                ViewBag.t = t;
                return View();
            }
        }
        [Authorize(Roles = "u")]
        public ActionResult Select(int id)
        {
            Travel t = syt.Travels.FirstOrDefault(x => x.Travel_ID == id);
            List<reservation> r = syt.reservations.Where(x => x.Travel_ID == t.Travel_ID).ToList();
            List<string> seatReserved = new List<string>();
            foreach(reservation res in r)
            {
                seatReserved.Add(res.seatNumber);
            }
            ViewBag.seatReserved = seatReserved;
            ViewBag.t = t; 
            return View();
        }
        [Authorize(Roles = "u")]
        [HttpPost]
        public ActionResult Reserve(int id ,string SeatNumber)
        {

            string userEmail = HttpContext.User.Identity.Name;
            int uID = Convert.ToInt32(syt.Users.FirstOrDefault(x => x.user_Email == userEmail).userLogin_ID);
            reservation r = new reservation();
            r.data_of_reservation = DateTime.Now;
            r.seatNumber = SeatNumber;
            r.Travel_ID = id;
            r.userPanel_ID = uID;
            syt.reservations.Add(r);
            syt.SaveChanges();
            return RedirectToAction("GetTickets");
        }
        [Authorize(Roles = "u")]
        public ActionResult GetTickets()
        {
            string userEmail = HttpContext.User.Identity.Name;
            int uID = Convert.ToInt32(syt.Users.FirstOrDefault(x => x.user_Email == userEmail).userLogin_ID);
            List<reservation> r = syt.reservations.Where(x => x.userPanel_ID == uID).ToList();
            ViewBag.r = r;
            return View();
        }
        [Authorize(Roles = "u")]
        public ActionResult DeleteTicket(int id)
        {
            string userEmail = HttpContext.User.Identity.Name;
            int uID = Convert.ToInt32(syt.Users.FirstOrDefault(x => x.user_Email == userEmail).userLogin_ID);
            reservation r = syt.reservations.FirstOrDefault(x => x.reserv_ID == id);
            if(r.userPanel_ID == uID)
            {
                syt.reservations.Remove(r);
                syt.SaveChanges();
            }
           
            return RedirectToAction("GetTickets");
        }

    }
}