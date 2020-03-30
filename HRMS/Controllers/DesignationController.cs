using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;

namespace HRMS.Controllers
{
    public class DesignationController : Controller
    {

        private HRMSEntities db = new HRMSEntities();

        // GET: HRMS_EMP_DESIGNATION_MS
        public ActionResult Index()
        {
            List<HRMS_EMP_DESIGNATION_MS> hRMS_EMP_DESIGNATION_MS1 = db.HRMS_EMP_DESIGNATION_MS.Where(rec => rec.Designation_Parent != null && rec.Designation_Parent != "" && rec.Designation_Name != null && rec.Designation_Name != "" && rec.Designation_Name != rec.Designation_Parent).ToList();

            return View(hRMS_EMP_DESIGNATION_MS1);
        }

        // GET: HRMS_EMP_DESIGNATION_MS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_DESIGNATION_MS hRMS_EMP_DESIGNATION_MS = db.HRMS_EMP_DESIGNATION_MS.Find(id);
            if (hRMS_EMP_DESIGNATION_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_EMP_DESIGNATION_MS);
        }

        // GET: HRMS_EMP_DESIGNATION_MS/Create
        public ActionResult Create()
        {

            HRMS_EMP_DESIGNATION_MS h1 = db.HRMS_EMP_DESIGNATION_MS.Where(rec => rec.Designation_ID != 0).FirstOrDefault();
            List<HRMS_EMP_DESIGNATION_MS> h2 = db.HRMS_EMP_DESIGNATION_MS.ToList();
            dynamic MultiView = new ExpandoObject();
            MultiView.data = h1;
            MultiView.list1 = h2;

            return View("Create", MultiView);

        }

        // POST: HRMS_EMP_DESIGNATION_MS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Designation_ID,Designation_Parent,Designation_ShortName,Designation_Name")] HRMS_EMP_DESIGNATION_MS hRMS_EMP_DESIGNATION_MS)
        {

            HRMS_EMP_DESIGNATION_MS h1 = db.HRMS_EMP_DESIGNATION_MS.Where(rec => rec.Designation_ID != 0).FirstOrDefault();
            List<HRMS_EMP_DESIGNATION_MS> h2 = db.HRMS_EMP_DESIGNATION_MS.ToList();
            dynamic MultiView = new ExpandoObject();
            MultiView.data = h1;
            MultiView.list1 = h2;





            HRMS_EMP_DESIGNATION_MS hed = new HRMS_EMP_DESIGNATION_MS();
            string D_parent = Request["D_parent"];
            string D_name = Request["D_name"];

            if (D_parent == "" || D_parent == null)
            {
                if (db.HRMS_EMP_DESIGNATION_MS.Where(rec => rec.Designation_Parent == D_name).Any())
                {
                    ViewBag.message = "Designation Already exist  !!!!!!!";

                }
                else
                {

                    hed.Designation_Parent = Request["D_name"];
                    hed.Designation_ShortName = Request["D_shortname"];
                    hed.Designation_Name = Request["D_name"];
                    db.HRMS_EMP_DESIGNATION_MS.Add(hed);
                    db.SaveChanges();
                    ViewBag.message = "Designation Added as parent  !!!!!!!";
                }
            }
            else if (db.HRMS_EMP_DESIGNATION_MS.Where(rec => rec.Designation_Parent == D_parent && rec.Designation_Name == D_name).Any())
            {
                ViewBag.message = "Designation Relationship already existed  !!!!!!!";
            }
            else
            {
                hed.Designation_Parent = Request["D_parent"];
                hed.Designation_ShortName = Request["D_shortname"];
                hed.Designation_Name = Request["D_name"];
                db.HRMS_EMP_DESIGNATION_MS.Add(hed);
                db.SaveChanges();
                ViewBag.message = "Added  !!!!!!!";
            }





            //                return RedirectToAction("Index");


            return View("Create", MultiView);
        }

        // GET: HRMS_EMP_DESIGNATION_MS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_DESIGNATION_MS hRMS_EMP_DESIGNATION_MS = db.HRMS_EMP_DESIGNATION_MS.Find(id);
            if (hRMS_EMP_DESIGNATION_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_EMP_DESIGNATION_MS);
        }

        // POST: HRMS_EMP_DESIGNATION_MS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Designation_ID,Designation_Parent,Designation_ShortName,Designation_Name")] HRMS_EMP_DESIGNATION_MS hRMS_EMP_DESIGNATION_MS)
        {

            if (ModelState.IsValid)
            {
                db.Entry(hRMS_EMP_DESIGNATION_MS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hRMS_EMP_DESIGNATION_MS);
        }

        // GET: HRMS_EMP_DESIGNATION_MS/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_DESIGNATION_MS hRMS_EMP_DESIGNATION_MS = db.HRMS_EMP_DESIGNATION_MS.Find(id);
            if (hRMS_EMP_DESIGNATION_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_EMP_DESIGNATION_MS);
        }

        // POST: HRMS_EMP_DESIGNATION_MS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HRMS_EMP_DESIGNATION_MS hRMS_EMP_DESIGNATION_MS = db.HRMS_EMP_DESIGNATION_MS.Find(id);
            db.HRMS_EMP_DESIGNATION_MS.Remove(hRMS_EMP_DESIGNATION_MS);
            db.SaveChanges();
            return RedirectToAction("Index");
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
