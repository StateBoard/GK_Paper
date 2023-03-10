using GK_Paper.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GK_Paper.Controllers
{
    public class MasterController : Controller
    {
            db_GK_Quetion_BankEntities1 db = new db_GK_Quetion_BankEntities1();
            private SqlConnection _Con;
            private object Tbl_Chapter_Name;
            private SqlCommand _Command;

            // GET: Master

            public ActionResult MasterPage()
            {
                return View();
            }
            [HttpPost]
            public JsonResult MasterPage(Tbl_Chapter_Name model)
            {

                try
                {

                    if (model.Id == 0)
                    {
                        model.Active = "1";
                        db.Tbl_Chapter_Name.Add(model);

                        db.SaveChanges();
                    }
                    else
                    {
                        var obj = db.Tbl_Chapter_Name.Where(x => x.Id == model.Id).FirstOrDefault();
                        obj.Chapter_Name = model.Chapter_Name;
                        db.Tbl_Chapter_Name.Attach(obj);
                        db.Entry(obj).Property(x => x.Chapter_Name).IsModified = true;
                        db.SaveChanges();
                    }


                    return Json(new { Result = true, Response = "Submitted" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Result = false, Response = "      " }, JsonRequestBehavior.AllowGet);
                }

            }
            [HttpGet]
            public JsonResult BindRecord(Tbl_Chapter_Name model)
            {

                try
                {
                    var res = db.Tbl_Chapter_Name.Where(X => X.Active == "1").ToList();



                    return Json(new { Result = true, Response = res }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Result = false, Response = "      " }, JsonRequestBehavior.AllowGet);
                }
            }
            [HttpGet]
            public JsonResult GetProdByID(int ID)
            {
                try
                {
                    var record = db.Tbl_Chapter_Name.Where(x => x.Id == ID).FirstOrDefault();
                    return Json(new { Result = true, Response = record }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Result = false, Response = "Submitted" }, JsonRequestBehavior.AllowGet);
                }
            }

            public JsonResult DeleteProdByID(int ID)
            {
                try
                {
                    var record = db.Tbl_Chapter_Name.Where(x => x.Id == ID).FirstOrDefault();

                    db.SaveChanges();
                    _Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
                    _Con.Open();
                    if (record != null)
                    {
                        _Command = new SqlCommand("Update Tbl_Chapter_Name set Active='0' where Id='" + record.Id + "'", _Con);
                        _Command.ExecuteNonQuery();
                    }
                    else
                    {
                        _Command = new SqlCommand("Update Tbl_Chapter_Name set Active='1' where Id='" + record.Id + "'", _Con);
                        _Command.ExecuteNonQuery();
                    }

                    _Con.Close();
                    return Json(new { Result = true, Response = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { Result = false, Response = "Cannot delete" }, JsonRequestBehavior.AllowGet);
                }
            }

        }
    }