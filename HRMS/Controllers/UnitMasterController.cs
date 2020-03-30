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
    public class UnitMasterController : Controller
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



            // GET: UnitMaster
            public ActionResult Index(string Data, string Search, int? page)
            {
                if (Data == "1")
                {
                    return View(db.UnitMasters.Where(x => x.UnitName.StartsWith(Search) || Search == null).ToList().ToPagedList(page ?? 1, 3));
                }
                else if (Data == "2")
                {
                    return View(db.UnitMasters.Where(x => x.Country.StartsWith(Search) || Search == null).ToList().ToPagedList(page ?? 1, 3));
                }
                else
                {
                    return View(db.UnitMasters.Where(x => x.City.StartsWith(Search) || Search == null).ToList().ToPagedList(page ?? 1, 3));
                }

            }

            // GET: UnitMaster/Details/5
            public ActionResult Details(long? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UnitMaster unitMaster = db.UnitMasters.Find(id);
                if (unitMaster == null)
                {
                    return HttpNotFound();
                }
                return View(unitMaster);
            }

            // GET: UnitMaster/Create
            public ActionResult Create()
            {
                ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
                ViewBag.State = new SelectList("");
                ViewBag.City = new SelectList("");
                return View();
            }

            // POST: UnitMaster/Create
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "UnitCode,UnitName,Address,Country,City,Pincode,Priority,PaySlip,IsActive,State")] UnitMaster unitMaster)
            {
                if (ModelState.IsValid)
                {
                    bool isValid = db.UnitMasters.Any(x => x.UnitName == unitMaster.UnitName || x.Priority == unitMaster.Priority);
                    if (!isValid)
                    {
                        unitMaster.IsActive = true;
                        db.UnitMasters.Add(unitMaster);
                        db.SaveChanges();
                        ViewBag.success = "Unit is Successfully created!";
                        ModelState.Clear();
                        ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
                        ViewBag.State = new SelectList("");
                        ViewBag.City = new SelectList("");
                        return View();
                    }
                    else
                    {
                        ViewBag.error = "Sorry! Unitname is already exist!";
                        long cid = db.Countries.Where(x => x.CountryName == unitMaster.Country).Select(x => x.CountryID).SingleOrDefault();
                        long sid = db.States.Where(x => x.StateName == unitMaster.State).Select(x => x.StateID).SingleOrDefault();
                        ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
                        ViewBag.State = new SelectList(db.States.Where(x => x.CountryID == cid), "StateName", "StateName");
                        ViewBag.City = new SelectList(db.Cities.Where(x => x.StateID == sid), "CityName", "CityName");

                        return View(unitMaster);
                    }
                }

                ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
                ViewBag.State = new SelectList("");
                ViewBag.City = new SelectList("");
                return View(unitMaster);
            }

            // GET: UnitMaster/Edit/5
            public ActionResult Edit(long? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UnitMaster unitMaster = db.UnitMasters.Find(id);
                if (unitMaster == null)
                {
                    return HttpNotFound();
                }

                long cid = db.Countries.Where(x => x.CountryName == unitMaster.Country).Select(x => x.CountryID).SingleOrDefault();
                long sid = db.States.Where(x => x.StateName == unitMaster.State).Select(x => x.StateID).SingleOrDefault();
                ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
                ViewBag.State = new SelectList(db.States.Where(x => x.CountryID == cid), "StateName", "StateName");
                ViewBag.City = new SelectList(db.Cities.Where(x => x.StateID == sid), "CityName", "CityName");
                return View(unitMaster);
            }

            // POST: UnitMaster/Edit/5
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "UnitCode,UnitName,Address,Country,City,Pincode,Priority,PaySlip,IsActive,State")] UnitMaster unitMaster)
            {
                if (ModelState.IsValid)
                {
                    bool isValid = db.UnitMasters.Any(x => (x.UnitCode != unitMaster.UnitCode) && (x.UnitName == unitMaster.UnitName || x.Priority == unitMaster.Priority));
                    if (!isValid)
                    {
                        db.Entry(unitMaster).State = EntityState.Modified;
                        db.SaveChanges();
                        ViewBag.success = "Your Record Successfully Updated!";

                        long cid = db.Countries.Where(x => x.CountryName == unitMaster.Country).Select(x => x.CountryID).SingleOrDefault();
                        long sid = db.States.Where(x => x.StateName == unitMaster.State).Select(x => x.StateID).SingleOrDefault();
                        ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
                        ViewBag.State = new SelectList(db.States.Where(x => x.CountryID == cid), "StateName", "StateName");
                        ViewBag.City = new SelectList(db.Cities.Where(x => x.StateID == sid), "CityName", "CityName");
                        return View();
                    }
                    else
                    {
                        if (db.UnitMasters.Any(x => (x.UnitCode != unitMaster.UnitCode) && (x.UnitName == unitMaster.UnitName)))
                        {
                            ViewBag.error = "Unitname is Already exist!";

                            long cid = db.Countries.Where(x => x.CountryName == unitMaster.Country).Select(x => x.CountryID).SingleOrDefault();
                            long sid = db.States.Where(x => x.StateName == unitMaster.State).Select(x => x.StateID).SingleOrDefault();
                            ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
                            ViewBag.State = new SelectList(db.States.Where(x => x.CountryID == cid), "StateName", "StateName");
                            ViewBag.City = new SelectList(db.Cities.Where(x => x.StateID == sid), "CityName", "CityName");
                            return View();
                        }
                        else
                        {
                            ViewBag.error = "Priority is Already exist!";

                            long cid = db.Countries.Where(x => x.CountryName == unitMaster.Country).Select(x => x.CountryID).SingleOrDefault();
                            long sid = db.States.Where(x => x.StateName == unitMaster.State).Select(x => x.StateID).SingleOrDefault();
                            ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
                            ViewBag.State = new SelectList(db.States.Where(x => x.CountryID == cid), "StateName", "StateName");
                            ViewBag.City = new SelectList(db.Cities.Where(x => x.StateID == sid), "CityName", "CityName");
                            return View();
                        }
                    }
                }

                long cid1 = db.Countries.Where(x => x.CountryName == unitMaster.Country).Select(x => x.CountryID).SingleOrDefault();
                long sid1 = db.States.Where(x => x.StateName == unitMaster.State).Select(x => x.StateID).SingleOrDefault();
                ViewBag.Country = new SelectList(db.Countries, "CountryName", "CountryName");
                ViewBag.State = new SelectList(db.States.Where(x => x.CountryID == cid1), "StateName", "StateName");
                ViewBag.City = new SelectList(db.Cities.Where(x => x.StateID == sid1), "CityName", "CityName");
                return View(unitMaster);
            }

            // GET: UnitMaster/Delete/5
            public ActionResult Delete(long? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UnitMaster unitMaster = db.UnitMasters.Find(id);
                if (unitMaster == null)
                {
                    return HttpNotFound();
                }
                return View(unitMaster);
            }

            // POST: UnitMaster/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(long id)
            {
                UnitMaster unitMaster = db.UnitMasters.Find(id);
                db.UnitMasters.Remove(unitMaster);
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
