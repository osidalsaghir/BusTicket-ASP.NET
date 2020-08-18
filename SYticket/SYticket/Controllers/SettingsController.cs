using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYticket.Models;

namespace SYticket.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        Syticket syt = new Syticket();

        [Authorize(Roles = "u")]
        public ActionResult Index()
        {
            string userName = HttpContext.User.Identity.Name;
            int u = Convert.ToInt32(syt.Users.FirstOrDefault(x => x.user_Email==userName ).userLogin_ID);

            userPanel up = syt.userPanels.FirstOrDefault(x => x.userPanel_ID == u);
            User us = syt.Users.FirstOrDefault(x => x.userLogin_ID == u);
            userContact uc = syt.userContacts.FirstOrDefault(x => x.contact_ID == up.contact_ID);
            ViewBag.up = up;
            ViewBag.date = up.birthdate.Date.ToString("yyyy-MM-dd");
            ViewBag.us = us;
            ViewBag.uc = uc;
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "u")]
        public ActionResult Index( DateTime date , string first_name , string last_name)
        {
            string userName = HttpContext.User.Identity.Name;
            int u = Convert.ToInt32(syt.Users.FirstOrDefault(x => x.user_Email == userName).userLogin_ID);

            userPanel up = syt.userPanels.FirstOrDefault(x => x.userPanel_ID == u);
            up.userPanel_ID = u; 
            up.userPanel_Name = first_name;
            up.userPanel_Surname = last_name;
            up.birthdate = date.Date;
            syt.userPanels.AddOrUpdate(up);
            syt.SaveChanges();
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "u")]
        public ActionResult ChangeContact(string Con_PhoneNumber, string Con_Email)
        {
            string userName = HttpContext.User.Identity.Name;
            int u = Convert.ToInt32(syt.Users.FirstOrDefault(x => x.user_Email == userName).userLogin_ID);

            userPanel up = syt.userPanels.FirstOrDefault(x => x.userPanel_ID == u);
            userContact uc = syt.userContacts.FirstOrDefault(x => x.contact_ID == up.contact_ID);
            uc.userContact_email = Con_Email;
            uc.userContact_PhoneNumber = Con_PhoneNumber;
            syt.SaveChanges();
            return RedirectToAction("Index");
        }  
    }
}