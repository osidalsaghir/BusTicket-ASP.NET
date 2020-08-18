using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SYticket.Models;

namespace SYticket.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        Syticket syt = new Syticket();
        public ActionResult SignUp()
        {
            return View();
        }
        
        public ActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userEmail = HttpContext.User.Identity.Name;
                string userRole = Roles.GetRolesForUser(userEmail).FirstOrDefault();
                if (userRole == "u")
                {
                    return RedirectToAction("Index", "Reservation");
                }
                else if (userRole == "c")
                {
                    return RedirectToAction("Index", "Travel");
                }
                else if (userRole == "a")
                {
                    return RedirectToAction("ListCompanies", "Companies");
                }
            }
            return View();
            
        }

        [HttpPost]
        public ActionResult UserSignIn(string email, string password)
        {
            try
            {
                User u = syt.Users.FirstOrDefault(x => x.user_Email == email);
                if (u != null)
                {
                    if (u.user_Password == password)
                    {
                        FormsAuthentication.SetAuthCookie(u.user_Email, false);
                        string userRole = Roles.GetRolesForUser(u.user_Email).FirstOrDefault();
                        if (userRole == "u")
                        {
                            return RedirectToAction("Index", "Reservation");
                        }
                        else if (userRole == "c")
                        {
                            return RedirectToAction("Index", "Travel");
                        }
                        else if (userRole == "a")
                        {
                            return RedirectToAction("ListCompanies", "Companies");
                        }

                    }
                    else
                    {
                        return RedirectToAction("SignIn");
                    }
                }
            }
            catch
            {
                return RedirectToAction("SignIn");
            }

            return RedirectToAction("SignIn");



        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
        }
        public ActionResult ErrorPage()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddAccount(userPanel up , userContact ucp , User sp , string password_repeat)
        {
            if (password_repeat.Equals(sp.user_Password))
            {
                userContact uc = new userContact();
                uc.userContact_PhoneNumber = "00000000000000";
                uc.userContact_email = ucp.userContact_email;
                syt.userContacts.Add(uc);
                syt.SaveChanges();
                int c_ID = syt.userContacts.ToList().Last().contact_ID;

                userPanel u = new userPanel();
                u.userPanel_Name = up.userPanel_Name;
                u.userPanel_Surname = up.userPanel_Surname;
                u.userPanel_national_ID = up.userPanel_national_ID;
                u.birthdate = up.birthdate;
                u.contact_ID = c_ID;
                syt.userPanels.Add(u);
                syt.SaveChanges();
                int userpID = syt.userPanels.ToList().Last().userPanel_ID;


                User uL = new User();
                uL.user_Email = ucp.userContact_email;
                uL.user_Password = sp.user_Password;
                uL.user_role = "u";
                uL.userLogin_ID = userpID;
                syt.Users.Add(uL);
                syt.SaveChanges();

                return RedirectToAction("SignIn");
            }
            else
            {
                return RedirectToAction("ErrorPage");
            }
           
        }

    }
}