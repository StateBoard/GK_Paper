using GK_Paper.Controllers;
using GK_Paper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GK_Paper.Controllers
{
    public class QuestionsListController : Controller
    {

        db_GK_Quetion_BankEntities1 db = new db_GK_Quetion_BankEntities1();

        public string Question_Name { get; private set; }
        public string Question_Type { get; private set; }

        // GET: QuestionsList

        public ActionResult Question_list()
        {

            return View();
        }

        public JsonResult Question_Data(tbl_Question_Paper model)
        {
            var records = db.tbl_Question_Paper.ToList();
            return Json(new { Result = true, Response = records }, JsonRequestBehavior.AllowGet);

            var res = db.tbl_Question_Paper.Where(x => x.Question_Name == model.Question_Name && x.Verify == "checked").AsEnumerable().Select((n, index) => new
            {
                SRNO = index + 1,

                Status = n.Verify.Replace("checked", "Verify")

            }).OrderBy(a => a.SRNO).ToList();

        }

        [HttpPost]
        public JsonResult Submit_Verified_Que(int Data)
        {
            try
            {
                var records = db.tbl_Question_Paper.Where(x => x.id == Data).FirstOrDefault();
                records.Verify = "Verify";
                db.tbl_Question_Paper.Attach(records);
                db.Entry(records).Property(x => x.Verify).IsModified = true;
                db.SaveChanges();
                return Json(new { Result = true, Response = "submit Sucsessfully" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Failed" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Question_list1()
        {
            try
            {


                var model = db.tbl_Question_Paper.ToList();

                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "---select chapter----", Value = "---select chapter----" });
                if (model != null)
                {
                    foreach (var group in model.GroupBy(x => x.Chapter_Name))
                    {
                        list.Add(new SelectListItem { Text = group.Key, Value = group.Key });
                    }

                }
                ViewBag.chapterList = new SelectList(list, "Value", "Text");


            }
            catch (Exception ex)
            {

            }


            return View();
        }

        public JsonResult getque(string id)
        {
            try
            {
                var List = db.tbl_Question_Paper.Where(s => s.Chapter_Name == id).GroupBy(x => x.Question_Type).Select(grp => grp.FirstOrDefault()).ToList();
                List<SelectListItem> licent = new List<SelectListItem>();
                licent.Add(new SelectListItem { Text = "-Select Question Type-", Value = "0" });
                if (List != null)
                {
                    foreach (var x in List)
                    {
                        licent.Add(new SelectListItem { Text = x.Question_Type, Value = x.Question_Type.ToString() });
                    }
                }
                return Json(new SelectList(licent, "Value", "Text", JsonRequestBehavior.AllowGet));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult getChapterList(string Chapter_Name, string Question_Type)
        {
            try
            {
                var model = db.tbl_Question_Paper.Where(x => x.Chapter_Name == Chapter_Name && x.Verify == null).ToList();
                List<SelectListItem> Quelist = new List<SelectListItem>();
                Quelist.Add(new SelectListItem { Text = "---select QuetionType----", Value = "---select QuetionType----" });
                if (model != null)
                {
                    foreach (var item in model)
                    {
                        Quelist.Add(new SelectListItem { Text = item.Question_Type.ToString(), Value = item.Question_Name.ToString() });
                    }
                }

                ViewBag.queList = new SelectList(Quelist, "Text", "Value");



                if (Question_Type == "" || Question_Type == "0")
                {
                    var res = db.tbl_Question_Paper.Where(x => x.Chapter_Name == Chapter_Name && x.Verify == null).ToList();

                    return Json(new { Result = true, Response = res }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var res = db.tbl_Question_Paper.Where(x => x.Chapter_Name == Chapter_Name && x.Question_Type == Question_Type).ToList();
                    return Json(new { Result = true, Response = res }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      


    }



}




