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
    public class DivisionController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: HRMS_EMP_BUSINESSDIVISION_MS
        public ActionResult Index()
        {
            return View(db.HRMS_EMP_BUSINESSDIVISION_MS.ToList());
        }

        // GET: HRMS_EMP_BUSINESSDIVISION_MS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_BUSINESSDIVISION_MS hRMS_EMP_BUSINESSDIVISION_MS = db.HRMS_EMP_BUSINESSDIVISION_MS.Find(id);
            if (hRMS_EMP_BUSINESSDIVISION_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_EMP_BUSINESSDIVISION_MS);
        }

        // GET: HRMS_EMP_BUSINESSDIVISION_MS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HRMS_EMP_BUSINESSDIVISION_MS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HRMS_EMP_BUSINESSDIVISION_MS hRMS_EMP_BUSINESSDIVISION_MS)
        {
            ViewBag.message = "";
            if (ModelState.IsValid)
            {
                HRMS_EMP_BUSINESSDIVISION_MS hebd = new HRMS_EMP_BUSINESSDIVISION_MS();
                string BD_name1 = Request["BD_name"];
                string BD_sapcode1 = (Request["BD_sapcode"]);
                if (BD_name1 != null && BD_name1 != "" && BD_sapcode1 != "" && BD_sapcode1 != null)
                {
                    string BD_name = Request["BD_name"];
                    long BD_sapcode = Convert.ToInt64(Request["BD_sapcode"]);
                    if (db.HRMS_EMP_BUSINESSDIVISION_MS.Where(rec => rec.BusinessDivision_Name == BD_name && rec.BusinessDivision_SapCode == BD_sapcode).Any())
                    {
                        ViewBag.message = "SAPCODE for this BUSINESS DIVISION Already Exist !!!!!";
                        return View(hRMS_EMP_BUSINESSDIVISION_MS);
                    }
                    else
                    {
                        if (db.HRMS_EMP_BUSINESSDIVISION_MS.Where(rec => rec.BusinessDivision_Name == BD_name).Any())
                        {
                            ViewBag.message = "BUSINESS DIVISION Already Exist !!!!!";

                            return View(hRMS_EMP_BUSINESSDIVISION_MS);
                        }
                        else if (db.HRMS_EMP_BUSINESSDIVISION_MS.Where(rec => rec.BusinessDivision_SapCode == BD_sapcode).Any())
                        {
                            ViewBag.message = "SAP CODE Already allocated !!!!!";

                            return View(hRMS_EMP_BUSINESSDIVISION_MS);

                        }
                        else
                        {
                            hebd.BusinessDivision_Name = BD_name;
                            hebd.BusinessDivision_SapCode = BD_sapcode;
                            db.HRMS_EMP_BUSINESSDIVISION_MS.Add(hebd);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    ViewBag.message = "please fill up all value !!!!!";
                }
            }

            return View();
        }

        // GET: HRMS_EMP_BUSINESSDIVISION_MS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_BUSINESSDIVISION_MS hRMS_EMP_BUSINESSDIVISION_MS = db.HRMS_EMP_BUSINESSDIVISION_MS.Find(id);
            if (hRMS_EMP_BUSINESSDIVISION_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_EMP_BUSINESSDIVISION_MS);
        }

        // POST: HRMS_EMP_BUSINESSDIVISION_MS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessDivision_ID,BusinessDivision_Name,BusinessDivision_SapCode")] HRMS_EMP_BUSINESSDIVISION_MS hRMS_EMP_BUSINESSDIVISION_MS)
        {
            if (ModelState.IsValid)
            {
                ViewBag.message = "";
                HRMS_EMP_BUSINESSDIVISION_MS hebd = new HRMS_EMP_BUSINESSDIVISION_MS();
                string BD_name1 = Request["BD_name"];
                string BD_sapcode1 = (Request["BD_sapcode"]);
                if (BD_name1 != null && BD_name1 != "" && BD_sapcode1 != "" && BD_sapcode1 != null)
                {
                    string BD_name = Request["BD_name"];
                    long BD_sapcode = Convert.ToInt64(Request["BD_sapcode"]);
                    if (db.HRMS_EMP_BUSINESSDIVISION_MS.Where(rec => rec.BusinessDivision_Name == BD_name && rec.BusinessDivision_SapCode == BD_sapcode).Any())
                    {
                        ViewBag.message = "SAPCODE for this BUSINESS DIVISION Already Exist !!!!!";
                        return View(hRMS_EMP_BUSINESSDIVISION_MS);
                    }
                    else
                    {
                        if (db.HRMS_EMP_BUSINESSDIVISION_MS.Where(rec => rec.BusinessDivision_Name == BD_name).Any())
                        {
                            ViewBag.message = "BUSINESS DIVISION Already Exist !!!!!";

                            return View(hRMS_EMP_BUSINESSDIVISION_MS);
                        }
                        else if (db.HRMS_EMP_BUSINESSDIVISION_MS.Where(rec => rec.BusinessDivision_SapCode == BD_sapcode).Any())
                        {
                            ViewBag.message = "SAP CODE Already allocated !!!!!";

                            return View(hRMS_EMP_BUSINESSDIVISION_MS);

                        }
                        else
                        {
                            hebd.BusinessDivision_Name = BD_name;
                            hebd.BusinessDivision_SapCode = BD_sapcode;
                            db.Entry(hebd).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    ViewBag.message = "please fill up all value !!!!!";
                }


            }
            return View(hRMS_EMP_BUSINESSDIVISION_MS);
        }

        // GET: HRMS_EMP_BUSINESSDIVISION_MS/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_EMP_BUSINESSDIVISION_MS hRMS_EMP_BUSINESSDIVISION_MS = db.HRMS_EMP_BUSINESSDIVISION_MS.Find(id);
            if (hRMS_EMP_BUSINESSDIVISION_MS == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_EMP_BUSINESSDIVISION_MS);
        }

        // POST: HRMS_EMP_BUSINESSDIVISION_MS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HRMS_EMP_BUSINESSDIVISION_MS hRMS_EMP_BUSINESSDIVISION_MS = db.HRMS_EMP_BUSINESSDIVISION_MS.Find(id);
            db.HRMS_EMP_BUSINESSDIVISION_MS.Remove(hRMS_EMP_BUSINESSDIVISION_MS);
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
