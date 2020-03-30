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
    public class SALUTATIONController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: SALUTATION
        public ActionResult Index()
        {
            return View(db.HRMS_SALUTATION.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Salutation_ID,Salutation_Name")] HRMS_SALUTATION hRMS_SALUTATION)
        {
            if (ModelState.IsValid)
            {
                var salutationName = db.HRMS_SALUTATION.FirstOrDefault(rec => rec.Salutation_Name == hRMS_SALUTATION.Salutation_Name);
                if (salutationName == null)
                {
                    db.HRMS_SALUTATION.Add(hRMS_SALUTATION);
                    db.SaveChanges();
                    ViewBag.Salutation_Status = "It is Created successfully!";
                    return View();
                }
                else
                {
                    ViewBag.Salutation_Status = "It is already exist!";
                    return View();
                }
            }

            return View(hRMS_SALUTATION);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_SALUTATION hRMS_SALUTATION = db.HRMS_SALUTATION.Find(id);
            if (hRMS_SALUTATION == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_SALUTATION);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Salutation_ID,Salutation_Name")] HRMS_SALUTATION hRMS_SALUTATION)
        {
            if (ModelState.IsValid)
            {
                var salutationName = db.HRMS_SALUTATION.FirstOrDefault(rec => rec.Salutation_Name == hRMS_SALUTATION.Salutation_Name);
                if (salutationName == null)
                {
                    db.Entry(hRMS_SALUTATION).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Salutation_Status = "It is already exist!";
                    return View();
                }
            }
            return View(hRMS_SALUTATION);
        }
        [HttpPost]
        public bool delete(long id)
        {
            HRMS_SALUTATION hRMS_SALUTATION = db.HRMS_SALUTATION.Find(id);
            if (hRMS_SALUTATION != null)
            {
                db.HRMS_SALUTATION.Remove(hRMS_SALUTATION);
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
