using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DtdcCashCounter.EntityFr;

namespace DtdcCashCounter.Controllers
{
    public class Receipt_detailsController : Controller
    {
        private db_a91f79_zionenterprisesEntities db = new db_a91f79_zionenterprisesEntities();

        // GET: Receipt_details
        public ActionResult Index()
        {
            var receipt_details = db.Receipt_details.Include(r => r.Franchisee).Include(r => r.User);
            return View(receipt_details.ToList());
        }

        // GET: Receipt_details/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt_details receipt_details = db.Receipt_details.Find(id);
            if (receipt_details == null)
            {
                return HttpNotFound();
            }
            return View(receipt_details);
        }

        // GET: Receipt_details/Create
        public ActionResult Create()
        {
            ViewBag.Pf_Code = new SelectList(db.Franchisees, "PF_Code", "F_Address");
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name");
            return View();
        }

        // POST: Receipt_details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Receipt_Id,Consignment_No,Destination,sender_phone,Sender_Email,Sender,SenderCompany,SenderAddress,SenderCity,SenderState,SenderPincode,Reciepents_phone,Reciepents_Email,Reciepents,ReciepentCompany,ReciepentsAddress,ReciepentsCity,ReciepentsState,ReciepentsPincode,Shipmenttype,Shipment_Length,Shipment_Quantity,Shipment_Breadth,Shipment_Heigth,DivideBy,TotalNo,Actual_Weight,volumetric_Weight,DescriptionContent1,DescriptionContent2,DescriptionContent3,Amount1,Amount2,Amount3,Total_Amount,Insurance,Insuance_Percentage,Insuance_Amount,Charges_Amount,Charges_Service,Risk_Surcharge,Service_Tax,Charges_Total,Cash,Credit,Credit_Amount,secure_Pack,Passport,OfficeSunday,Shipment_Mode,Addition_charge,Addition_Lable,Discount,Pf_Code,User_Id,Datetime_Cons,Paid_Amount")] Receipt_details receipt_details)
        {
            if (ModelState.IsValid)
            {
                db.Receipt_details.Add(receipt_details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Pf_Code = new SelectList(db.Franchisees, "PF_Code", "F_Address", receipt_details.Pf_Code);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", receipt_details.User_Id);
            return View(receipt_details);
        }

        // GET: Receipt_details/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt_details receipt_details = db.Receipt_details.Find(id);
            if (receipt_details == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pf_Code = new SelectList(db.Franchisees, "PF_Code", "F_Address", receipt_details.Pf_Code);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", receipt_details.User_Id);
            return View(receipt_details);
        }

        // POST: Receipt_details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Receipt_Id,Consignment_No,Destination,sender_phone,Sender_Email,Sender,SenderCompany,SenderAddress,SenderCity,SenderState,SenderPincode,Reciepents_phone,Reciepents_Email,Reciepents,ReciepentCompany,ReciepentsAddress,ReciepentsCity,ReciepentsState,ReciepentsPincode,Shipmenttype,Shipment_Length,Shipment_Quantity,Shipment_Breadth,Shipment_Heigth,DivideBy,TotalNo,Actual_Weight,volumetric_Weight,DescriptionContent1,DescriptionContent2,DescriptionContent3,Amount1,Amount2,Amount3,Total_Amount,Insurance,Insuance_Percentage,Insuance_Amount,Charges_Amount,Charges_Service,Risk_Surcharge,Service_Tax,Charges_Total,Cash,Credit,Credit_Amount,secure_Pack,Passport,OfficeSunday,Shipment_Mode,Addition_charge,Addition_Lable,Discount,Pf_Code,User_Id,Datetime_Cons,Paid_Amount")] Receipt_details receipt_details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receipt_details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Pf_Code = new SelectList(db.Franchisees, "PF_Code", "F_Address", receipt_details.Pf_Code);
            ViewBag.User_Id = new SelectList(db.Users, "User_Id", "Name", receipt_details.User_Id);
            return View(receipt_details);
        }

        // GET: Receipt_details/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receipt_details receipt_details = db.Receipt_details.Find(id);
            if (receipt_details == null)
            {
                return HttpNotFound();
            }
            return View(receipt_details);
        }

        // POST: Receipt_details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Receipt_details receipt_details = db.Receipt_details.Find(id);
            db.Receipt_details.Remove(receipt_details);
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
