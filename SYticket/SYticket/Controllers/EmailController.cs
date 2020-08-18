using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYticket.Models;

namespace SYticket.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        Syticket syt = new Syticket();
        [Authorize(Roles = "a")]
        public ActionResult Index(int id )
        {
            List<User> u = syt.Users.Where(x => x.companyLogin_ID == id).ToList();
            companyInfo ci = syt.companyInfoes.FirstOrDefault(x => x.company_ID == id);
            ViewBag.ci = ci; 
            ViewBag.u = u;
            
            return View();
        }
        [Authorize(Roles = "a")]
        public ActionResult AddEmail(int id)
        {

            companyInfo ci = syt.companyInfoes.FirstOrDefault(x => x.company_ID == id);
            ViewBag.ci = ci;
            return View();
        }
        [Authorize(Roles = "a")]
        [HttpPost]
         public ActionResult AddCompanyEmail(int id , string email , string password)
        {

            User u = new User();
            u.user_Email = email;
            u.user_Password = password;
            u.user_role = "c";
            u.companyLogin_ID = id;
            syt.Users.Add(u);
            syt.SaveChanges();
            return RedirectToAction("Index" , new { id = id});
        }

        [Authorize(Roles = "a")]
        public ActionResult DeleteCompanyEmail(int id)
        {
           
            User u = syt.Users.FirstOrDefault(x => x.user_Login_id == id);
            var company_ID = u.companyLogin_ID;
            syt.Users.Remove(u);
            syt.SaveChanges();
            return RedirectToAction("Index", new { id = company_ID });
        }
        [Authorize(Roles = "a")]
        public ActionResult EditEmail(int id)
        {

            User u = syt.Users.FirstOrDefault(x => x.user_Login_id == id);
            companyInfo ci = syt.companyInfoes.FirstOrDefault(x => x.company_ID == u.companyLogin_ID);
            ViewBag.u = u; 
            ViewBag.ci = ci;
            return View();
        }

        
         [HttpPost]
        [Authorize(Roles = "a")]
        public ActionResult EditCompanyEmail(int id, string email, string password)
        {

            User u = syt.Users.FirstOrDefault(x => x.user_Login_id == id); ;
            u.user_Login_id = id;
            u.user_Email = email;
            u.user_Password = password;
            u.user_role = "c";
            u.companyLogin_ID = u.companyLogin_ID;
            syt.Users.AddOrUpdate(u);
            syt.SaveChanges();
            return RedirectToAction("Index", new { id = u.companyLogin_ID });
        }
    }
}