
using GK_Paper.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GK_Paper.Controllers
{
    public class AddController : Controller
    {
        db_GK_Quetion_BankEntities1 db = new db_GK_Quetion_BankEntities1();
        // GET: Add
        [HttpGet]
        public ActionResult Index()
        {
            var model = db.Tbl_Chapter_Name.ToList();
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem { Text = "--Select --", Value = "0" }
            };
            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(new SelectListItem { Text = item.Chapter_Name, Value = item.Chapter_Name });
                }
            }
            ViewBag.ChapterList = new SelectList(list, "Value", "Text");
            return View();
        }
        public List<string> GetChapterList()
        {
            var tbl_Chapter_Names = db.Tbl_Chapter_Name.Select(x => x.Chapter_Name).ToList();
            return tbl_Chapter_Names;
        }

        [HttpPost]
        public ActionResult Index(tbl_Question_Paper model)
        {
            try
            {
                if (model.id == 0)
                {
                    if (model.Question_Name == null || model.Question_Name.Trim() == "0")
                    {
                        return Json(new { Result = false, Response = "Please Enter Question Name" }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Question_Type == null || model.Question_Type.Trim() == "0")
                    {
                        return Json(new { Result = false, Response = "Question Name" }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.OptionA == null || model.OptionA.Trim() == "0")
                    {
                        return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.OptionB == null || model.OptionB.Trim() == "0")
                    {
                        return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.OptionC == null || model.OptionC.Trim() == "0")
                    {
                        return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.OptionD == null || model.OptionD.Trim() == "0")
                    {
                        return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Correct_Option == null || model.Correct_Option.Trim() == "0")
                    {
                        return Json(new { Result = false }, JsonRequestBehavior.AllowGet);

                    }

                    int cnt = db.tbl_Question_Paper.Where(s => s.Question_Name == model.Question_Name).Count();

                    if (cnt != 0)
                    {
                        return Json(new { Result = false, Response = "Question Already Exists!" }, JsonRequestBehavior.AllowGet);

                    }

                    db.tbl_Question_Paper.Add(model);
                    db.SaveChanges();
                    return Json(new { Result = true, Response = "Question Add Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.tbl_Question_Paper.Add(model);
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { Result = true, Response = "Question Edited Successfully" }, JsonRequestBehavior.AllowGet);
                }


            }


            catch (Exception e)
            {
                return Json(new { Result = true, Response = "Question not Successfully save" }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Get_Data()
        {

            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int page = 1;
                if (start == "0" && start == null)
                {
                    page = 0;
                }
                else
                {
                    page = Convert.ToInt32(start) / Convert.ToInt32(length);
                }

                var Count = db.tbl_Question_Paper.Count();
                string Query = "select * from tbl_Question_Paper ORDER BY ID DESC OFFSET " + pageSize + " * (" + page + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
                List<tbl_Question_Paper> tbl = db.Database.SqlQuery<tbl_Question_Paper>(Query).ToList();

                return Json(new { draw = draw, recordsFiltered = Count, recordsTotal = Count, data = tbl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exe)
            {
                return Json(new { Result = false, Response = "Record not Successfully save" }, JsonRequestBehavior.AllowGet);
            }


        }
        [HttpGet]
        public ActionResult Question_Add()
        {
            var model = db.Tbl_Chapter_Name.ToList();
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem { Text = "--Select--", Value = "0" }
            };
            if (model != null)
            {
                foreach (var item in model)
                {
                    list.Add(new SelectListItem { Text = item.Chapter_Name, Value = item.Chapter_Name });
                }
            }
            ViewBag.ChapterList = new SelectList(list, "Value", "Text");
            return View();
        }
        public List<string> GetChapter_Add()
        {
            var tbl_Chapter_Names = db.Tbl_Chapter_Name.Select(x => x.Chapter_Name).ToList();
            return tbl_Chapter_Names;
        }
        [HttpPost]
        public ActionResult Question_Add(tbl_Question_Paper model)
        {
            try
            {
                if (model.id == 0)
                {
                    if (model.Question_Name == null || model.Question_Name.Trim() == "0")
                    {
                        return Json(new { Result = false, Response = "Please Enter Question Name" }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Question_Type == null || model.Question_Type.Trim() == "0")
                    {
                        return Json(new { Result = false, Response = "Question Name" }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.OptionA == null || model.OptionA.Trim() == "0")
                    {
                        //return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.OptionB == null || model.OptionB.Trim() == "0")
                    {
                        //return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.OptionC == null || model.OptionC.Trim() == "0")
                    {
                        //return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.OptionD == null || model.OptionD.Trim() == "0")
                    {
                        //return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Option1 == null || model.Option1.Trim() == "0")
                    {
                        //return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Option2 == null || model.Option2.Trim() == "0")
                    {
                        //return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Option3 == null || model.Option3.Trim() == "0")
                    {
                        //return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Option4 == null || model.Option4.Trim() == "0")
                    {
                        //return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
                    }
                    if (model.Correct_Option == null || model.Correct_Option.Trim() == "0")
                    {
                        //return Json(new { Result = false }, JsonRequestBehavior.AllowGet);

                    }

                    int cnt = db.tbl_Question_Paper.Where(s => s.Question_Name == model.Question_Name).Count();

                    if (cnt != 0)
                    {
                        return Json(new { Result = false, Response = "Question Already Exists!" }, JsonRequestBehavior.AllowGet);
                    }
                    db.tbl_Question_Paper.Add(model);
                    db.SaveChanges();

                }
                return Json(new { Result = true, Response = "Question Add Successfully" }, JsonRequestBehavior.AllowGet);

            }

            catch (Exception e)
            {
                return Json(new { Result = true, Response = "Question not Successfully save" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Get_Que()

        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int page = 1;
                if (start == "0" && start == null)
                {
                    page = 0;
                }
                else
                {
                    page = Convert.ToInt32(start) / Convert.ToInt32(length);
                }

                var Count = db.tbl_Question_Paper.Count();
                string Query = "select * from tbl_Question_Paper ORDER BY ID DESC OFFSET " + pageSize + " * (" + page + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
                List<tbl_Question_Paper> tbl = db.Database.SqlQuery<tbl_Question_Paper>(Query).ToList();

                return Json(new { draw = draw, recordsFiltered = Count, recordsTotal = Count, data = tbl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exe)
            {
                return Json(new { Result = false, Response = "Record not Successfully save" }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult Delete_Record(int id)
        {
            var detele = db.tbl_Question_Paper.Where(x => x.id == id).FirstOrDefault();
            db.tbl_Question_Paper.Remove(detele);
            db.SaveChanges();
            return Json(new { Result = true, Response = "Record Deleted Successfull" }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]

        public JsonResult Edit_Record(int id)
        {
            var edit = db.tbl_Question_Paper.Where(x => x.id == id).FirstOrDefault();
            return Json(new { Result = true, Response = edit }, JsonRequestBehavior.AllowGet);

        }

       

    }
}

