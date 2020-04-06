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
    public class BranchMasterController : Controller
    {
        private HRMSEntities db = new HRMSEntities();


        public JsonResult GetStateList(string CountryX)
        {
            db.Configuration.ProxyCreationEnabled = false;
            long CountryID = db.Countries.Where(x => x.CountryName == CountryX).Select(x => x.CountryID).SingleOrDefault();
            List<State> StateList = db.States.Where(x => x.CountryID == CountryID).ToList();
            return Json(StateList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCityList(string StateX)
        {
            db.Configuration.ProxyCreationEnabled = false;
            long StateID = db.States.Where(x => x.StateName == StateX).Select(x => x.StateID).SingleOrDefault();
            List<City> CityList = db.Cities.Where(x => x.StateID == StateID).ToList();
            return Json(CityList, JsonRequestBehavior.AllowGet);
        }


        //Bank Index
        public ActionResult BankIndex()
        {
            return View(db.BankMaster.ToList());
        }



        // GET: BranchMaster
        public ActionResult Index()
        {
            var branchMaster = db.BranchMaster.Include(b => b.BankMaster);
            return View(branchMaster.ToList());
        }

        // GET: BranchMaster/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchMaster branchMaster = db.BranchMaster.Find(id);
            if (branchMaster == null)
            {
                return HttpNotFound();
            }
            return View(branchMaster);
        }

        [HttpPost]
        public ActionResult AddBank(string BankName)
        {

            BankMaster obj = new BankMaster();
            bool isValid = db.BankMaster.Any(x => x.BankName == BankName);
            if (!isValid)
            {
                obj.BankName = BankName;
                db.BankMaster.Add(obj);
                db.SaveChanges();
                TempData["success"] = "Bank Name is Successfully created!";
                ModelState.Clear();
                return RedirectToAction("Create");
            }
            else
            {
                TempData["error"] = "Sorry!Bank Name is already exist!";
                return RedirectToAction("Create");
            }
        }


        // GET: BranchMaster/Create
        public ActionResult Create()
        {
            ViewBag.success = TempData["success"];
            ViewBag.error = TempData["error"];

            ViewBag.BankCode = new SelectList(db.BankMaster, "BankCode", "BankName");
            ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
            ViewBag.State = new SelectList("");
            ViewBag.City = new SelectList("");
            return View();
        }

        // POST: BranchMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BranchCode,BankCode,BranchName,MICR_Code,IFSC_Code,Contact,Fax,EmailID,Country,State,City,Address,PinCode")] BranchMaster branchMaster)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.BranchMaster.Any(x => (x.BankCode == branchMaster.BankCode) && (x.BranchName == branchMaster.BranchName));
                if (!isValid)
                {

                    db.BranchMaster.Add(branchMaster);
                    db.SaveChanges();
                    ViewBag.success1 = "Branch is Successfully created!";
                    ModelState.Clear();
                    ViewBag.BankCode = new SelectList(db.BankMaster, "BankCode", "BankName");
                    ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
                    ViewBag.State = new SelectList("");
                    ViewBag.City = new SelectList("");
                    return View();
                }
                else
                {
                    ViewBag.error1 = "Sorry! Branch Name is already exist!";
                    long cid = db.Countries.Where(x => x.CountryName == branchMaster.Country).Select(x => x.CountryID).SingleOrDefault();
                    long sid = db.States.Where(x => x.StateName == branchMaster.State).Select(x => x.StateID).SingleOrDefault();
                    ViewBag.BankCode = new SelectList(db.BankMaster, "BankCode", "BankName");
                    ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
                    ViewBag.State = new SelectList(db.States.Where(x => x.CountryID == cid), "StateName", "StateName");
                    ViewBag.City = new SelectList(db.Cities.Where(x => x.StateID == sid), "CityName", "CityName");

                    return View(branchMaster);
                }
            }
            ViewBag.BankCode = new SelectList(db.BankMaster, "BankCode", "BankName", branchMaster.BankCode);
            ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
            ViewBag.State = new SelectList("");
            ViewBag.City = new SelectList("");
            return View(branchMaster);



        }


        //Edit Bank
        [HttpGet]
        public ActionResult EditBank(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankMaster bankmaster = db.BankMaster.Find(id);
            if (bankmaster == null)
            {
                return HttpNotFound();
            }

            return View(bankmaster);

        }

        [HttpPost]
        public ActionResult EditBank(BankMaster bankmaster)
        {
            bool isValid = db.BankMaster.Any(x => (x.BankCode != bankmaster.BankCode) && (x.BankName == bankmaster.BankName));
            if (!isValid)
            {
                db.Entry(bankmaster).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.success = "Your Record Successfully Updated!";
                return View();
            }
            else
            {
                ViewBag.error = "Bank Name is Already exist!";
                return View();

            }
        }

        // GET: BranchMaster/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchMaster branchMaster = db.BranchMaster.Find(id);
            if (branchMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankCode = new SelectList(db.BankMaster, "BankCode", "BankName", branchMaster.BankCode);
            return View(branchMaster);
        }

        // POST: BranchMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BranchCode,BankCode,BranchName,MICR_Code,IFSC_Code,Contact,Fax,EmailID,Country,State,City,Address,PinCode")] BranchMaster branchMaster)
        {
            if (ModelState.IsValid)
            {
                bool isValid = db.BranchMaster.Any(x => (x.BranchCode != branchMaster.BranchCode) && (x.BankCode == branchMaster.BankCode && x.BranchName == branchMaster.BranchName));
                if (!isValid)
                {
                    db.Entry(branchMaster).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.success = "Your Record Successfully Updated!";

                    long cid = db.Countries.Where(x => x.CountryName == branchMaster.Country).Select(x => x.CountryID).SingleOrDefault();
                    long sid = db.States.Where(x => x.StateName == branchMaster.State).Select(x => x.StateID).SingleOrDefault();
                    ViewBag.BankCode = new SelectList(db.BankMaster, "BankCode", "BankName", branchMaster.BankCode);
                    ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
                    ViewBag.State = new SelectList(db.States.Where(x => x.CountryID == cid), "StateName", "StateName");
                    ViewBag.City = new SelectList(db.Cities.Where(x => x.StateID == sid), "CityName", "CityName");
                    return View();
                }
                else
                {
                    ViewBag.error = "Branch Name is Already exist!";
                    long cid = db.Countries.Where(x => x.CountryName == branchMaster.Country).Select(x => x.CountryID).SingleOrDefault();
                    long sid = db.States.Where(x => x.StateName == branchMaster.State).Select(x => x.StateID).SingleOrDefault();
                    ViewBag.BankCode = new SelectList(db.BankMaster, "BankCode", "BankName", branchMaster.BankCode);
                    ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
                    ViewBag.State = new SelectList(db.States.Where(x => x.CountryID == cid), "StateName", "StateName");
                    ViewBag.City = new SelectList(db.Cities.Where(x => x.StateID == sid), "CityName", "CityName");
                    return View();

                }
            }

            long cid1 = db.Countries.Where(x => x.CountryName == branchMaster.Country).Select(x => x.CountryID).SingleOrDefault();
            long sid1 = db.States.Where(x => x.StateName == branchMaster.State).Select(x => x.StateID).SingleOrDefault();
            ViewBag.BankCode = new SelectList(db.BankMaster, "BankCode", "BankName", branchMaster.BankCode);
            ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
            ViewBag.State = new SelectList(db.States.Where(x => x.CountryID == cid1), "StateName", "StateName");
            ViewBag.City = new SelectList(db.Cities.Where(x => x.StateID == sid1), "CityName", "CityName");
            return View(branchMaster);
        }

        // GET: BranchMaster/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchMaster branchMaster = db.BranchMaster.Find(id);
            if (branchMaster == null)
            {
                return HttpNotFound();
            }
            return View(branchMaster);
        }

        // POST: BranchMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BranchMaster branchMaster = db.BranchMaster.Find(id);
            db.BranchMaster.Remove(branchMaster);
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
