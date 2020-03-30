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
    public class AttachmentController : Controller
    {
        private HRMSEntities db = new HRMSEntities();
        public ActionResult Index()
        {
            return View(db.HRMS_ATTACHMENT_TYPE.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Attachment_Type_ID,Attachment_Type_Name")] HRMS_ATTACHMENT_TYPE hRMS_ATTACHMENT_TYPE)
        {
            if (ModelState.IsValid)
            {
                var attachment_name = db.HRMS_ATTACHMENT_TYPE.FirstOrDefault(rec => rec.Attachment_Type_Name == hRMS_ATTACHMENT_TYPE.Attachment_Type_Name);
                if(attachment_name == null) { 
                db.HRMS_ATTACHMENT_TYPE.Add(hRMS_ATTACHMENT_TYPE);
                db.SaveChanges();
                    ViewBag.Attachment_status = "Attachment Type is added successfully!";
                    return View();
                }
                else
                {
                    ViewBag.Attachment_status = "Attachment Type is already exist!";
                    return View();
                }
            }
            return View(hRMS_ATTACHMENT_TYPE);
        }
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_ATTACHMENT_TYPE hRMS_ATTACHMENT_TYPE = db.HRMS_ATTACHMENT_TYPE.Find(id);
            if (hRMS_ATTACHMENT_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_ATTACHMENT_TYPE);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Attachment_Type_ID,Attachment_Type_Name")] HRMS_ATTACHMENT_TYPE hRMS_ATTACHMENT_TYPE)
        {
            if (ModelState.IsValid)
            {
                var attachment_name = db.HRMS_ATTACHMENT_TYPE.FirstOrDefault(rec => rec.Attachment_Type_Name == hRMS_ATTACHMENT_TYPE.Attachment_Type_Name);
                if (attachment_name == null)
                {
                    db.Entry(hRMS_ATTACHMENT_TYPE).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Attachment_status = "Attachment Type is already exist!";
                    return View();
                }
            }
            return View(hRMS_ATTACHMENT_TYPE);
        }
        [HttpPost]
        public ActionResult Delete(long id)
        {
            HRMS_ATTACHMENT_TYPE hRMS_ATTACHMENT_TYPE = db.HRMS_ATTACHMENT_TYPE.Find(id);
            db.HRMS_ATTACHMENT_TYPE.Remove(hRMS_ATTACHMENT_TYPE);
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
