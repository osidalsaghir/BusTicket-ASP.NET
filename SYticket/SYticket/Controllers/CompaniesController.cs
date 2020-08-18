using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYticket.Models;



namespace SYticket.Controllers
{
    public class CompaniesController : Controller
    {
        // GET: Companies
        Syticket syt = new Syticket();

        [Authorize(Roles = "a")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "a")]
        [HttpPost]
        public ActionResult CreateCompany(string ComapanyName ,string CompanyEmail 
                                        , string CompanyPassword , string CompanyInfoEmail 
                                        , string CompanyInfoPhoneNumber , string CompanyInfoWebsite 
                                        , string Street , string city   , string zipCode
                                        , string neighborhood , HttpRequestBase Photo , FormCollection collection)
        {
            string userEmail = HttpContext.User.Identity.Name;
            int aID = Convert.ToInt32(syt.Users.FirstOrDefault(x => x.user_Email == userEmail).adminLogin_ID);
            string path = "/uploads/def/logo.png";
            
            if (Request.Files["Photo"] != null)
            {
                path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                Request.Files["Photo"].SaveAs(Server.MapPath(path));
               

                
            }


            companyInfo ci = new companyInfo();
            ci.company_Name = ComapanyName;
            ci.company_Logo = path;
            ci.company_StartDate = DateTime.UtcNow;
            ci.admin_ID = aID;
            syt.companyInfoes.Add(ci);
            syt.SaveChanges();
            int companyID = syt.companyInfoes.ToList().Last().company_ID;

            User u = new User();
            u.user_Email = CompanyEmail;
            u.user_Password = CompanyPassword;
            u.user_role = "c";
            u.companyLogin_ID = companyID;
            syt.Users.Add(u);
            syt.SaveChanges();



            companyContact cc = new companyContact();

            cc.company_Email = CompanyInfoEmail;
            cc.company_PhoneNumber = CompanyInfoPhoneNumber;
            cc.company_Website = CompanyInfoWebsite;
            cc.company_ID = companyID; 
            syt.companyContacts.Add(cc);
            syt.SaveChanges();
            

            companyAddress cd = new companyAddress();

            cd.company_Street = Street;
            cd.company_neighborhood = neighborhood;
            cd.company_zip_code = zipCode;
            cd.company_city = city;
            cd.company_ID = companyID; 
            syt.companyAddresses.Add(cd);
            syt.SaveChanges();
            
           
            
            return RedirectToAction("ListCompanies");
        }

        [Authorize(Roles = "a")]
        public ActionResult ListCompanies()
        {


            List<companyInfo> ci = syt.companyInfoes.ToList();
            List<User> u = syt.Users.ToList();
            List < companyAddress > ca = syt.companyAddresses.ToList();
            List<companyContact> cc = syt.companyContacts.ToList();
            List<Travel> t = syt.Travels.ToList();
            ViewBag.ci = ci;
            ViewBag.u = u;
            ViewBag.cc = cc;
            ViewBag.ca = ca;
            ViewBag.t = t; 
            return View();
        }

        public ActionResult EditCompany(int id)
        {
                companyInfo ci = syt.companyInfoes.FirstOrDefault(x => x.company_ID == id);
                ViewBag.ci = ci;
            
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "a")]
        public ActionResult EditCompany( int id ,string ComapanyName, HttpRequestBase Photo , FormCollection collection)
        {
            
            companyInfo ci = syt.companyInfoes.FirstOrDefault(x => x.company_ID == id);
            string path = "/uploads/def/logo.png";

            if (Request.Files["Photo"].FileName != "")
            {
                path = "/uploads/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                Request.Files["Photo"].SaveAs(Server.MapPath(path));
                ci.company_Logo = path;
            }
            else
            {
                ci.company_Logo = ci.company_Logo;
            }
            ci.company_Name = ComapanyName;
            syt.SaveChanges();

            return RedirectToAction("EditCompany" , new {id=ci.company_ID});
        }
        [Authorize(Roles = "a")]
        public ActionResult DeleteCompany(int id)
        {
            List<companyAddress> ca = syt.companyAddresses.Where(x => x.company_ID == id).ToList();
            List<companyContact> cc = syt.companyContacts.Where(x => x.company_ID == id).ToList();
            List<Travel> t = syt.Travels.Where(x => x.company_ID == id).ToList();
            List<User> u = syt.Users.Where(x => x.companyLogin_ID == id).ToList();
            syt.Users.RemoveRange(u);

            syt.companyAddresses.RemoveRange(ca);

            syt.companyContacts.RemoveRange(cc);

            foreach(Travel tn in t)
            {
                List<reservation> res = syt.reservations.Where(x => x.Travel_ID == tn.Travel_ID).ToList();
                syt.reservations.RemoveRange(res);
                
            }
            syt.SaveChanges();

            syt.Travels.RemoveRange(t);

            syt.SaveChanges();
            companyInfo c = syt.companyInfoes.FirstOrDefault(x => x.company_ID == id);
            syt.companyInfoes.Remove(c);
            syt.SaveChanges();
            return RedirectToAction("ListCompanies");
        }


    }
}