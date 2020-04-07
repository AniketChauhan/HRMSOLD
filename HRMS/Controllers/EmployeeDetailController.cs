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
    public class EmployeeDetailController : Controller
    {
        private HRMSEntities db = new HRMSEntities();

        // GET: EmployeeDetail
        public ActionResult Index()
        {
            var hRMS_Emp_Details = db.HRMS_Emp_Details.Include(h => h.HRMS_COST_CENTER).Include(h => h.HRMS_DEPT).Include(h => h.HRMS_EMP_DESIGNATION_MS).Include(h => h.HRMS_SALUTATION).Include(h => h.UnitMaster).Include(h => h.WorkLocationMaster);
            return View(hRMS_Emp_Details.ToList());
        }

        // GET: EmployeeDetail/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Emp_Details hRMS_Emp_Details = db.HRMS_Emp_Details.Find(id);
            if (hRMS_Emp_Details == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_Emp_Details);
        }

        // GET: EmployeeDetail/Create
        public ActionResult Create()
        {
            ViewBag.Cost_Center = new SelectList(db.HRMS_COST_CENTER, "ID", "Cost_Cntr_Name");
            ViewBag.Department = new SelectList(db.HRMS_DEPT, "Dept_Id", "Dept_Name");
            ViewBag.Designation = new SelectList(db.HRMS_EMP_DESIGNATION_MS, "Designation_ID", "Designation_Parent");
            ViewBag.salutation = new SelectList(db.HRMS_SALUTATION, "Salutation_ID", "Salutation_Name");
            ViewBag.Unit = new SelectList(db.UnitMasters, "UnitCode", "UnitName");
            ViewBag.Work_Location = new SelectList(db.WorkLocationMasters, "WorkID", "WorkLocationName");
            return View();
        }

        // POST: EmployeeDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Emp_ID,Emp_Cd,Old_Emp_Cd,Join_Date,Card_Id,salutation,First_Name,Middle_Name,Last_Name,Display_Name,Designation,Work_Location,Unit,Department,Cost_Center,UAN_No_")] HRMS_Emp_Details hRMS_Emp_Details)
        {
            if (ModelState.IsValid)
            {
                db.HRMS_Emp_Details.Add(hRMS_Emp_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cost_Center = new SelectList(db.HRMS_COST_CENTER, "ID", "Cost_Cntr_Name", hRMS_Emp_Details.Cost_Center);
            ViewBag.Department = new SelectList(db.HRMS_DEPT, "Dept_Id", "Dept_Name", hRMS_Emp_Details.Department);
            ViewBag.Designation = new SelectList(db.HRMS_EMP_DESIGNATION_MS, "Designation_ID", "Designation_Parent", hRMS_Emp_Details.Designation);
            ViewBag.salutation = new SelectList(db.HRMS_SALUTATION, "Salutation_ID", "Salutation_Name", hRMS_Emp_Details.salutation);
            ViewBag.Unit = new SelectList(db.UnitMasters, "UnitCode", "UnitName", hRMS_Emp_Details.Unit);
            ViewBag.Work_Location = new SelectList(db.WorkLocationMasters, "WorkID", "WorkLocationName", hRMS_Emp_Details.Work_Location);
            return View(hRMS_Emp_Details);
        }

        // GET: EmployeeDetail/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Emp_Details hRMS_Emp_Details = db.HRMS_Emp_Details.Find(id);
            if (hRMS_Emp_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cost_Center = new SelectList(db.HRMS_COST_CENTER, "ID", "Cost_Cntr_Name", hRMS_Emp_Details.Cost_Center);
            ViewBag.Department = new SelectList(db.HRMS_DEPT, "Dept_Id", "Dept_Name", hRMS_Emp_Details.Department);
            ViewBag.Designation = new SelectList(db.HRMS_EMP_DESIGNATION_MS, "Designation_ID", "Designation_Parent", hRMS_Emp_Details.Designation);
            ViewBag.salutation = new SelectList(db.HRMS_SALUTATION, "Salutation_ID", "Salutation_Name", hRMS_Emp_Details.salutation);
            ViewBag.Unit = new SelectList(db.UnitMasters, "UnitCode", "UnitName", hRMS_Emp_Details.Unit);
            ViewBag.Work_Location = new SelectList(db.WorkLocationMasters, "WorkID", "WorkLocationName", hRMS_Emp_Details.Work_Location);
            return View(hRMS_Emp_Details);
        }

        // POST: EmployeeDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Emp_ID,Emp_Cd,Old_Emp_Cd,Join_Date,Card_Id,salutation,First_Name,Middle_Name,Last_Name,Display_Name,Designation,Work_Location,Unit,Department,Cost_Center,UAN_No_")] HRMS_Emp_Details hRMS_Emp_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hRMS_Emp_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cost_Center = new SelectList(db.HRMS_COST_CENTER, "ID", "Cost_Cntr_Name", hRMS_Emp_Details.Cost_Center);
            ViewBag.Department = new SelectList(db.HRMS_DEPT, "Dept_Id", "Dept_Name", hRMS_Emp_Details.Department);
            ViewBag.Designation = new SelectList(db.HRMS_EMP_DESIGNATION_MS, "Designation_ID", "Designation_Parent", hRMS_Emp_Details.Designation);
            ViewBag.salutation = new SelectList(db.HRMS_SALUTATION, "Salutation_ID", "Salutation_Name", hRMS_Emp_Details.salutation);
            ViewBag.Unit = new SelectList(db.UnitMasters, "UnitCode", "UnitName", hRMS_Emp_Details.Unit);
            ViewBag.Work_Location = new SelectList(db.WorkLocationMasters, "WorkID", "WorkLocationName", hRMS_Emp_Details.Work_Location);
            return View(hRMS_Emp_Details);
        }

        // GET: EmployeeDetail/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HRMS_Emp_Details hRMS_Emp_Details = db.HRMS_Emp_Details.Find(id);
            if (hRMS_Emp_Details == null)
            {
                return HttpNotFound();
            }
            return View(hRMS_Emp_Details);
        }

        // POST: EmployeeDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HRMS_Emp_Details hRMS_Emp_Details = db.HRMS_Emp_Details.Find(id);
            db.HRMS_Emp_Details.Remove(hRMS_Emp_Details);
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
