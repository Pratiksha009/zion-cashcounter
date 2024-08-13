using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DtdcCashCounter.EntityFr;

namespace DtdcCashCounter.Controllers
{
    public class demoController : Controller
    {
        private db_a91f79_zionenterprisesEntities db = new db_a91f79_zionenterprisesEntities();

        // GET: demo
        public async Task<ActionResult> Index()
        {
            


            await GizmosAsync();


          

            return View();
        }



        public async Task<ActionResult> GizmosAsync()
        {
            var slist = db.Service_list.ToList();

            foreach (var i in slist)
            {
                i.Pincode = i.Pincode.Replace('"', ' ').Trim();
                i.Service_ = i.Service_.Replace('"', ' ').Trim();



                db.Entry(i).State = EntityState.Modified;
                db.SaveChanges();


            }

            return View("Gizmos");
        }



        // GET: demo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sector sector = db.Sectors.Find(id);
            if (sector == null)
            {
                return HttpNotFound();
            }
            return View(sector);
        }

        // GET: demo/Create
        public ActionResult Create()
        {
            ViewBag.Pf_code = new SelectList(db.Franchisees, "PF_Code", "F_Address");
            return View();
        }

        // POST: demo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sector_Id,Sector_Name,Pf_code,Pincode_values,Priority,CashD,CashN,BillD,BillN")] Sector sector)
        {
            if (ModelState.IsValid)
            {
                db.Sectors.Add(sector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Pf_code = new SelectList(db.Franchisees, "PF_Code", "F_Address", sector.Pf_code);
            return View(sector);
        }

        // GET: demo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sector sector = db.Sectors.Find(id);
            if (sector == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pf_code = new SelectList(db.Franchisees, "PF_Code", "F_Address", sector.Pf_code);
            return View(sector);
        }

        // POST: demo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sector_Id,Sector_Name,Pf_code,Pincode_values,Priority,CashD,CashN,BillD,BillN")] Sector sector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Pf_code = new SelectList(db.Franchisees, "PF_Code", "F_Address", sector.Pf_code);
            return View(sector);
        }

        // GET: demo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sector sector = db.Sectors.Find(id);
            if (sector == null)
            {
                return HttpNotFound();
            }
            return View(sector);
        }

        // POST: demo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sector sector = db.Sectors.Find(id);
            db.Sectors.Remove(sector);
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
