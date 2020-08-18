using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYticket.Models;

namespace SYticket.Controllers
{
    public class AddressController : Controller
    {
        // GET: Address
        Syticket syt = new Syticket();
        [Authorize(Roles = "a")]
        public ActionResult Index(int id)
        {
            
            List<companyAddress> ca = syt.companyAddresses.Where(u => u.company_ID == id).ToList();
            companyInfo ci = syt.companyInfoes.FirstOrDefault(u => u.company_ID == id);
            ViewBag.ca = ca;
            ViewBag.ci = ci; 

            return View();
            
           
        }
        [Authorize(Roles = "a")]
        public ActionResult AddAddress(int id)
        {
            companyInfo ci = syt.companyInfoes.FirstOrDefault(u => u.company_ID == id);
            ViewBag.ci = ci;

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "a")]
        public ActionResult AddCompanyAddress(int id , string street , string neighborhood ,string City , string Zip_Code)
        {
            companyAddress ca = new companyAddress();
            ca.company_ID = id;
            ca.company_city = City;
            ca.company_neighborhood = neighborhood;
            ca.company_Street = street;
            ca.company_zip_code = Zip_Code; 
            syt.companyAddresses.Add(ca);
            syt.SaveChanges();

            return RedirectToAction("Index", new { id = id });
        }
        [Authorize(Roles = "a")]
        public ActionResult DeleteAddress(int id)
        {
            companyAddress ca = syt.companyAddresses.FirstOrDefault(x => x.address_ID == id);
            syt.companyAddresses.Remove(ca);
            syt.SaveChanges();

            return RedirectToAction("Index", new { id = ca.company_ID });
        }
        [Authorize(Roles = "a")]
        public ActionResult EditAddress(int id)
        {
            companyAddress ca = syt.companyAddresses.FirstOrDefault(x => x.address_ID == id);
            companyInfo ci = syt.companyInfoes.FirstOrDefault(u => u.company_ID == ca.company_ID);
            ViewBag.ci = ci;
            ViewBag.ca = ca;
            return View();
        }
        [Authorize(Roles = "a")]
        public ActionResult EditCompanyAddress(int id, string street, string neighborhood, string City, string Zip_Code)
        {
            companyAddress ca = syt.companyAddresses.FirstOrDefault(x => x.address_ID == id);
            ca.address_ID = ca.address_ID;
            ca.company_ID = ca.company_ID;
            ca.company_Street = street;
            ca.company_neighborhood = neighborhood;
            ca.company_city = City;
            ca.company_zip_code = Zip_Code;
            syt.companyAddresses.AddOrUpdate(ca);
            syt.SaveChanges();

            return RedirectToAction("Index", new { id = ca.company_ID });
        }
    }
}