using HRMS.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HRMS.Controllers
{
    public class CategoryGradeController : Controller
    {
        private HRMSEntities db = new HRMSEntities();
        public ActionResult Index()
        {
            return View(db.HRMS_CATEGORY_GRADE.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Category_ID,Category_Name,Grade_Name,Grade_Detail")] HRMS_CATEGORY_GRADE hRMS_CATEGORY_GRADE)
        {
            if (ModelState.IsValid)
            {
                var category_name = db.HRMS_CATEGORY_GRADE.FirstOrDefault(rec => rec.Category_Name == hRMS_CATEGORY_GRADE.Category_Name);
                if (category_name != null)
                {
                    var grade_name = db.HRMS_CATEGORY_GRADE.FirstOrDefault(rec => rec.Grade_Name == hRMS_CATEGORY_GRADE.Grade_Name);
                    if (grade_name != null)
                    {
                        ViewBag.Grade_Status = "Grade name is Already exist in the same category !";
                        return View();
                    }
                    db.HRMS_CATEGORY_GRADE.Add(hRMS_CATEGORY_GRADE);
                    db.SaveChanges();
                    ViewBag.Grade_Status = "Grade is added Successfuly.";
                    return View(hRMS_CATEGORY_GRADE);
                }
                db.HRMS_CATEGORY_GRADE.Add(hRMS_CATEGORY_GRADE);
                db.SaveChanges();
                ViewBag.Grade_Status = "Grade is added Successfuly.";
                return View(hRMS_CATEGORY_GRADE);
            }

            return View(hRMS_CATEGORY_GRADE);
        }
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_CATEGORY_GRADE hRMS_CATEGORY_GRADE = db.HRMS_CATEGORY_GRADE.Find(id);
            if (hRMS_CATEGORY_GRADE == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_CATEGORY_GRADE);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Category_ID,Category_Name,Grade_Name,Grade_Detail")] HRMS_CATEGORY_GRADE hRMS_CATEGORY_GRADE)
        {
            if (ModelState.IsValid)
            {
                var category_name = db.HRMS_CATEGORY_GRADE.FirstOrDefault(rec => rec.Category_Name == hRMS_CATEGORY_GRADE.Category_Name);
                if (category_name != null)
                {
                    var grade_name = db.HRMS_CATEGORY_GRADE.FirstOrDefault(rec => rec.Grade_Name == hRMS_CATEGORY_GRADE.Grade_Name);
                    if (grade_name != null)
                    {
                        ViewBag.Grade_Status = "Grade name is Already exist in the same category !";
                        return View();
                    }
                    var searchRaw = db.HRMS_CATEGORY_GRADE.FirstOrDefault(rec=> rec.Category_ID == hRMS_CATEGORY_GRADE.Category_ID);
                    searchRaw.Category_Name = hRMS_CATEGORY_GRADE.Category_Name;
                    searchRaw.Grade_Name = hRMS_CATEGORY_GRADE.Grade_Name;
                    searchRaw.Grade_Detail = hRMS_CATEGORY_GRADE.Grade_Detail;
                    db.SaveChanges();
                    ViewBag.Grade_Status = "Grade is added Successfuly.";
                    return RedirectToAction("Index");
                }
                var searchRaw1 = db.HRMS_CATEGORY_GRADE.FirstOrDefault(rec => rec.Category_ID == hRMS_CATEGORY_GRADE.Category_ID);
                searchRaw1.Category_Name = hRMS_CATEGORY_GRADE.Category_Name;
                searchRaw1.Grade_Name = hRMS_CATEGORY_GRADE.Grade_Name;
                searchRaw1.Grade_Detail = hRMS_CATEGORY_GRADE.Grade_Detail;
                db.SaveChanges();
                ViewBag.Grade_Status = "Grade is added Successfuly.";
                return RedirectToAction("Index");
            }
            return View(hRMS_CATEGORY_GRADE);
        }
        [HttpPost]
        public bool delete(long id)
        {
            HRMS_CATEGORY_GRADE hRMS_CATEGORY_GRADE = db.HRMS_CATEGORY_GRADE.Find(id);
            if (hRMS_CATEGORY_GRADE != null)
            {
                db.HRMS_CATEGORY_GRADE.Remove(hRMS_CATEGORY_GRADE);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
