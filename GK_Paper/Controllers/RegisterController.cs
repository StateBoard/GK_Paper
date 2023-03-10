using GK_Paper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GK_Paper.Controllers
{
    public class RegisterController : Controller
    {
        db_GK_Quetion_BankEntities1 db = new db_GK_Quetion_BankEntities1();
        // GET: Registration
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Registration(Tbl_gklogin model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Name == null) { return Json(new { Result = false, Response = "Please Enter Name." }, JsonRequestBehavior.AllowGet); }
                    if (model.Email_ID == null) { return Json(new { Result = false, Response = "Please Enter Email_ID ." }, JsonRequestBehavior.AllowGet); }
                    if (model.Mobile_no == null) { return Json(new { Result = false, Response = "Please Enter Mobile_no." }, JsonRequestBehavior.AllowGet); }
                    if (model.Password == null) { return Json(new { Result = false, Response = "Please Enter Password." }, JsonRequestBehavior.AllowGet); }
                    if (model.Inedx_no == null) { return Json(new { Result = false, Response = "Please Enter Inedx_no." }, JsonRequestBehavior.AllowGet); }
                    if (model.date == null) { return Json(new { Result = false, Response = "Please Enter date." }, JsonRequestBehavior.AllowGet); }
                    if (model.IP_Adress == null) { return Json(new { Result = false, Response = "Please Enter IP_Adress." }, JsonRequestBehavior.AllowGet); }

                    db.Tbl_gklogin.Add(model);
                    db.SaveChanges();
                    ModelState.Clear();
                    return Json(new { Result = true, Response = "Record Saved Successfully" }, JsonRequestBehavior.AllowGet);

                }
                return Json(new { Result = false, Response = "Failed" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Failed" + ex }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login_Model login_Model)
        {
            try
            {
                var res = db.Tbl_gklogin.Where(x => x.Inedx_no == login_Model.Inedx_no).FirstOrDefault();

                if (login_Model.Inedx_no == res.Inedx_no && login_Model.Password == res.Password)
                {
                    login_Model.Inedx_no = res.Inedx_no;
                    string json = JsonConvert.SerializeObject(login_Model);
                    FormsAuthentication.SetAuthCookie(json, false);


                    //TempData["Msg"] = "login sucsessfully";
                    return RedirectToAction("Create", "Paper_Create", new { username = login_Model.Inedx_no });


                }

                else
                {
                    ModelState.AddModelError("", "Invalid Details.");
                    return View();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ActionResult DashBoard()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Register");
        }
        public ActionResult forget_password()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult getPassword(string Inedx_no)
        {
            try
            {
                var Password = db.Tbl_gklogin.Where(x => x.Inedx_no == Inedx_no).FirstOrDefault();
                if (Password == null)
                {
                    string Error = "Invalid Inedx No Number.";
                    return Json(Error, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(Password.Password, JsonRequestBehavior.AllowGet);
                    return Json(new { Result = true, Message = "Forget Password Successfully !" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpGet]
        public ActionResult Change_Pass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Change_Pass(String Inedx_no, String Password)
        {
            Tbl_gklogin pass = db.Tbl_gklogin.Where(x => x.Inedx_no == Inedx_no).FirstOrDefault();
           try
            {
                string Index = Inedx_no.ToLower();
                if (pass.Inedx_no == Index)
                {
                    pass.Password = Password;
                }
                db.Tbl_gklogin.Attach(pass);

                db.Entry(pass).Property(x => x.Password).IsModified = true;
                db.SaveChanges();

            }
            catch (Exception e)
            {
                return Json(new { Result = "false", Message = "Something Went Wrong!" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Result = "true", Message = "Password changed successfully" }, JsonRequestBehavior.AllowGet);
        }


    }
}