using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using PagedList;
using PagedList.Mvc;
namespace HRMS.Controllers
{
    public class SegmentMasterController : Controller
    {
   private HRMSEntities db = new HRMSEntities();

        // GET: SegmentMaster
        public ActionResult Index(int? page)
        {
            return View(db.SegmentMasters.ToList().ToPagedList(page ?? 1, 3));
        }

        // GET: SegmentMaster/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SegmentMaster segmentMaster = db.SegmentMasters.Find(id);
            if (segmentMaster == null)
            {
                return HttpNotFound();
            }
            return View(segmentMaster);
        }

        // GET: SegmentMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SegmentMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SegmentID,SegmentName,SAPCode")] SegmentMaster segmentMaster)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.SegmentMasters.Any(x => x.SegmentName == segmentMaster.SegmentName);
                if (!isValid)
                {
                    db.SegmentMasters.Add(segmentMaster);
                    db.SaveChanges();
                    ViewBag.success = "Segment is Successfully created!";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.error = "Sorry! Segment is already exist!";
                    return View(segmentMaster);
                }
            }

            return View(segmentMaster);
        }

        // GET: SegmentMaster/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SegmentMaster segmentMaster = db.SegmentMasters.Find(id);
            if (segmentMaster == null)
            {
                return HttpNotFound();
            }
            return View(segmentMaster);
        }

        // POST: SegmentMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SegmentID,SegmentName,SAPCode")] SegmentMaster segmentMaster)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.SegmentMasters.Any(x => (x.SegmentID != segmentMaster.SegmentID) && (x.SegmentName == segmentMaster.SegmentName));
                if (!isValid)
                {
                    db.Entry(segmentMaster).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.success = "Your Record Successfully Updated!";
                    return View();
                }
                else
                {
                    ViewBag.error = "Segment is Already exist!";
                    return View();

                }
            }
            return View(segmentMaster);
        }

        // GET: SegmentMaster/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SegmentMaster segmentMaster = db.SegmentMasters.Find(id);
            if (segmentMaster == null)
            {
                return HttpNotFound();
            }
            return View(segmentMaster);
        }

        // POST: SegmentMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SegmentMaster segmentMaster = db.SegmentMasters.Find(id);
            db.SegmentMasters.Remove(segmentMaster);
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
