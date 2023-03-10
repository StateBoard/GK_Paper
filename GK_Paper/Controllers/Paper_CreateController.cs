using GK_Paper.Helper;
using GK_Paper.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GK_Paper.Controllers
{
    public class Paper_CreateController : Controller

    {
        db_GK_Quetion_BankEntities1 db = new db_GK_Quetion_BankEntities1();

        Common common = new Common();
        // GET: Paper_Create

        public void Get_Cnt()
        {
            Login_Model login_model = common.Get_Login_Details();
            ViewData["1-97"] = db.Tbl_Final_Que.Where(x => x.Index_No == login_model.Inedx_no && x.Type == "1-97" && x.Paper_ID == null).Count();
            ViewData["98-99"] = db.Tbl_Final_Que.Where(x => x.Index_No == login_model.Inedx_no && x.Type == "98-99" && x.Paper_ID == null).Count();
            ViewData["100"] = db.Tbl_Final_Que.Where(x => x.Index_No == login_model.Inedx_no && x.Type == "100" && x.Paper_ID == null).Count();
            ViewData["Total"] = db.Tbl_Final_Que.Where(x => x.Index_No == login_model.Inedx_no && x.Paper_ID == null).Count();

            
        }

        [HttpGet]
        public ActionResult Create(string username)
        {
            Login_Model login_Model = new Login_Model();

            login_Model = common.Get_Login_Details();
            var model = db.Tbl_Chapter_Name.ToList();
            Session["Index_No"] = username;
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
            Get_Cnt();

            return View();
        }
        public List<string> GetChapterList()
        {
            var tbl_Chapter_Names = db.Tbl_Chapter_Name.Select(x => x.Chapter_Name).ToList();
            return tbl_Chapter_Names;
        }

        public ActionResult Get_Dat(String Chapter_Name, string Type, string Verify)
        {
            var model = db.tbl_Question_Paper.Where(x => x.Chapter_Name == Chapter_Name).ToList();

            if (Chapter_Name != Type && Verify != null)

            {
                var res = db.tbl_Question_Paper.Where(x => x.Chapter_Name == Chapter_Name && x.Question_Type == Type && x.Verify != null).ToList();
                return Json(new { Result = true, Response = res }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var res = db.tbl_Question_Paper.Where(x => x.Chapter_Name == Chapter_Name && x.Question_Type == Type && x.Verify != null).ToList();

                return Json(new { Result = true, Response = res }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult Data_re(Final_Data model)
        {
            try
            {
                Login_Model login_Model = common.Get_Login_Details();
                Tbl_Final_Que _Question_Paper = new Tbl_Final_Que();
                var res = db.Tbl_gklogin.Where(x => x.Inedx_no == login_Model.Inedx_no).FirstOrDefault();

                var Q98_99 = db.Tbl_Final_Que.Where(x => x.Index_No == login_Model.Inedx_no && x.Type == "98-99" && x.Paper_ID == null).Count();
                var Q1_97 = db.Tbl_Final_Que.Where(x => x.Index_No == login_Model.Inedx_no && x.Type == "1-97" && x.Paper_ID == null).Count();
                var Q100 = db.Tbl_Final_Que.Where(x => x.Index_No == login_Model.Inedx_no && x.Type == "100" && x.Paper_ID == null).Count();
                
                //var ss = Q98_99 + Q1_97 + Q100;

                var index = db.Tbl_Final_Que.Where(x => x.Index_No == login_Model.Inedx_no && x.Paper_ID == null).ToList();

                if (model.Type == "98-99" && Q98_99 == 2)
                {
                    return Json(new { Result = false, Response = "Only 2 questions are allowed!" }, JsonRequestBehavior.AllowGet);
                }
                if (model.Type == "1-97" && Q1_97 == 7)
                {
                    return Json(new { Result = false, Response = "Only 97 questions are allowed!" }, JsonRequestBehavior.AllowGet);
                }
                if (model.Type == "100" && Q100 == 1)
                {
                    return Json(new { Result = false, Response = "Only 1 question is allowed!" }, JsonRequestBehavior.AllowGet);
                }

                if (model.Chk != null)
                {
                    foreach (var item in model.Chk)
                    {
                        if ((model.Type == "98-99" && Q98_99 != 2) || (model.Type == "1-97" && Q1_97 != 7) || (model.Type == "100" && Q100 != 1))
                        {
                            var res1 = db.tbl_Question_Paper.Where(x => x.id.ToString() == item).FirstOrDefault();
                            _Question_Paper.Question_Id = res1.id.ToString();
                            _Question_Paper.Type = res1.Question_Type;
                            _Question_Paper.Question = res1.Question_Name;
                            _Question_Paper.Option_A = res1.OptionA;
                            _Question_Paper.Option_B = res1.OptionB;
                            _Question_Paper.Option_C = res1.OptionC;
                            _Question_Paper.Option_D = res1.OptionD;
                            _Question_Paper.Option_1 = res1.Option1;
                            _Question_Paper.Option_2 = res1.Option2;
                            _Question_Paper.Option_3 = res1.Option3;
                            _Question_Paper.Option_4 = res1.Option4;
                            _Question_Paper.Correct_Option = res1.Correct_Option;
                            _Question_Paper.Index_No = res.Inedx_no;
                            _Question_Paper.IP_Address = res.IP_Adress;
                            _Question_Paper.Date_Time.ToString();
                            _Question_Paper.Date_Time = DateTime.Now;

                            db.Tbl_Final_Que.Add(_Question_Paper);
                            db.SaveChanges();
                            

                            Q98_99 = db.Tbl_Final_Que.Where(x => x.Index_No == login_Model.Inedx_no && x.Type == "98-99").Count();
                            Q1_97 = db.Tbl_Final_Que.Where(x => x.Index_No == login_Model.Inedx_no && x.Type == "1-97").Count();
                            Q100 = db.Tbl_Final_Que.Where(x => x.Index_No == login_Model.Inedx_no && x.Type == "100").Count();
                        }
                        else
                        {
                            return Json(new { Result = false, Response = "Question limit exceded!" }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    return Json(new { Result = true, Response = "Question Added Successfully!" }, JsonRequestBehavior.AllowGet);
                }
                if (index.Count == 10)
                {
                    foreach (var item2 in index)
                    {
                        if (item2.Paper_ID == null)
                        {
                            item2.Paper_ID = model.Paper_ID;
                            db.Entry(item2).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    return Json(new { Result = true, Response = "Paper Created Successfully!" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Result = true, Response = "Please Add Question !???" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Something Went Wrong!" + ex }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult data_view(tbl_Question_Paper model)
        {
            return View();
        }

        [HttpPost]
        public ActionResult data_view()
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
                string Query = "select * from tbl_Question_Paper where Verify is not null ORDER BY ID DESC OFFSET " + pageSize + " * (" + page + ") ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
                List<tbl_Question_Paper> tbl = db.Database.SqlQuery<tbl_Question_Paper>(Query).ToList();

                return Json(new { draw = draw, recordsFiltered = Count, recordsTotal = Count, data = tbl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exe)
            {
                return Json(new { Result = false, Response = "Record not Successfully save" }, JsonRequestBehavior.AllowGet);
            }


        }

    }
}