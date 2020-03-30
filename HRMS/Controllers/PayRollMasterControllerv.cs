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
    public class PayRollMasterController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: PayRollMaster
        public ActionResult Index(int? page)
        {
            return View(db.PayRollMasters.ToList().ToPagedList(page ?? 1, 3));
        }

        // GET: PayRollMaster/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayRollMaster payRollMaster = db.PayRollMasters.Find(id);
            if (payRollMaster == null)
            {
                return HttpNotFound();
            }
            return View(payRollMaster);
        }

        // GET: PayRollMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PayRollMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PayRollCode,PayRollName")] PayRollMaster payRollMaster)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.PayRollMasters.Any(x => x.PayRollName == payRollMaster.PayRollName);
                if (!isValid)
                {

                    db.PayRollMasters.Add(payRollMaster);
                    db.SaveChanges();
                    ViewBag.success = "PayRoll is Successfully created!";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.error = "Sorry! PayRoll is already exist!";
                    return View(payRollMaster);
                }
            }

            return View(payRollMaster);
        }

        // GET: PayRollMaster/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayRollMaster payRollMaster = db.PayRollMasters.Find(id);
            if (payRollMaster == null)
            {
                return HttpNotFound();
            }
            return View(payRollMaster);
        }

        // POST: PayRollMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PayRollCode,PayRollName")] PayRollMaster payRollMaster)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.PayRollMasters.Any(x => (x.PayRollCode != payRollMaster.PayRollCode) && (x.PayRollName == payRollMaster.PayRollName));
                if (!isValid)
                {
                    db.Entry(payRollMaster).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.success = "Your Record Successfully Updated!";
                    return View();
                }
                else
                {
                    ViewBag.error = "PayRoll is Already exist!";
                    return View();

                }
            }
            return View(payRollMaster);
        }

        // GET: PayRollMaster/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PayRollMaster payRollMaster = db.PayRollMasters.Find(id);
            if (payRollMaster == null)
            {
                return HttpNotFound();
            }
            return View(payRollMaster);
        }

        // POST: PayRollMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PayRollMaster payRollMaster = db.PayRollMasters.Find(id);
            db.PayRollMasters.Remove(payRollMaster);
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
