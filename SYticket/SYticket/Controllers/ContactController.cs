using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using SYticket.Models;

namespace SYticket.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        Syticket syt = new Syticket();

        [Authorize(Roles = "a")]
        public ActionResult Index(int id)
        {

            List<companyContact> cc = syt.companyContacts.Where(u => u.company_ID == id).ToList();
            companyInfo ci = syt.companyInfoes.FirstOrDefault(x => x.company_ID == id);
            ViewBag.ci = ci;
            ViewBag.cc = cc;
            return View();
        }
        [Authorize(Roles = "a")]
        public ActionResult DeleteContact(int id)
        {

            companyContact cc = syt.companyContacts.FirstOrDefault(u => u.contact_ID == id);
            var company_ID = cc.company_ID;
            syt.companyContacts.Remove(cc);
            syt.SaveChanges();
            return RedirectToAction("Index" , new { id = company_ID });
        }

        [Authorize(Roles = "a")]
        public ActionResult AddContact(int id)
        {
            companyInfo ci = syt.companyInfoes.FirstOrDefault(x => x.company_ID ==id);
            ViewBag.ci = ci;
            return View();
        }

        [Authorize(Roles = "a")]
        [HttpPost]
        public ActionResult AddCompanyContact(int id , string CompanyInfoEmail, string CompanyInfoPhoneNumber, string CompanyInfoWebsite)
        {
            companyContact cc = new companyContact();
            cc.company_ID = id;
            cc.company_PhoneNumber = CompanyInfoPhoneNumber;
            cc.company_Email = CompanyInfoEmail;
            cc.company_Website = CompanyInfoWebsite;
            syt.companyContacts.Add(cc);
            syt.SaveChanges();
            return RedirectToAction("Index" , new {id = id});
        }

        [Authorize(Roles = "a")]
        public ActionResult EditContact(int id)
        {
            companyContact cc = syt.companyContacts.FirstOrDefault(x => x.contact_ID == id);
            companyInfo ci = syt.companyInfoes.FirstOrDefault(x => x.company_ID == cc.company_ID);
            ViewBag.ci = ci; 
            ViewBag.cc = cc;
            return View();
        }

        [Authorize(Roles = "a")]
        [HttpPost]
        public ActionResult EditCompanyContact(int id , string CompanyInfoEmail , string CompanyInfoPhoneNumber , string CompanyInfoWebsite)
        {
            companyContact cc = syt.companyContacts.First(x => x.contact_ID == id);
            cc.company_Email = CompanyInfoEmail;
            cc.company_PhoneNumber = CompanyInfoPhoneNumber;
            cc.company_Website = CompanyInfoWebsite;
            cc.contact_ID = cc.contact_ID;
            syt.companyContacts.AddOrUpdate(cc);
            syt.SaveChanges();
            return RedirectToAction("Index" , new {id = cc.company_ID});
        }
    }
}