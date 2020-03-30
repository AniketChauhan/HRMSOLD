using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;

namespace HRMS.Controllers
{
    public class GenderController : Controller
    {

        private HRMSEntities db = new HRMSEntities();

        // GET: HRMS_EMP_GENDER_MS
        public ActionResult Index()
        {
            return View(db.HRMS_EMP_GENDER_MS.ToList());
        }

        // GET: HRMS_EMP_GENDER_MS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_GENDER_MS hRMS_EMP_GENDER_MS = db.HRMS_EMP_GENDER_MS.Find(id);
            if (hRMS_EMP_GENDER_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_EMP_GENDER_MS);
        }

        // GET: HRMS_EMP_GENDER_MS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HRMS_EMP_GENDER_MS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Gender_ID,Gender_Value")] HRMS_EMP_GENDER_MS hRMS_EMP_GENDER_MS)
        {
            if (ModelState.IsValid)
            {
                db.HRMS_EMP_GENDER_MS.Add(hRMS_EMP_GENDER_MS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hRMS_EMP_GENDER_MS);
        }

        // GET: HRMS_EMP_GENDER_MS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_GENDER_MS hRMS_EMP_GENDER_MS = db.HRMS_EMP_GENDER_MS.Find(id);
            if (hRMS_EMP_GENDER_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_EMP_GENDER_MS);
        }

        // POST: HRMS_EMP_GENDER_MS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Gender_ID,Gender_Value")] HRMS_EMP_GENDER_MS hRMS_EMP_GENDER_MS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hRMS_EMP_GENDER_MS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hRMS_EMP_GENDER_MS);
        }

        // GET: HRMS_EMP_GENDER_MS/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_GENDER_MS hRMS_EMP_GENDER_MS = db.HRMS_EMP_GENDER_MS.Find(id);
            if (hRMS_EMP_GENDER_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_EMP_GENDER_MS);
        }

        // POST: HRMS_EMP_GENDER_MS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HRMS_EMP_GENDER_MS hRMS_EMP_GENDER_MS = db.HRMS_EMP_GENDER_MS.Find(id);
            db.HRMS_EMP_GENDER_MS.Remove(hRMS_EMP_GENDER_MS);
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
