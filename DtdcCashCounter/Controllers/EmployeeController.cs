using DtdcCashCounter.EntityFr;
using DtdcCashCounter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DtdcCashCounter.Controllers
{
    public class EmployeeController : Controller
    {
        db_a91f79_zionenterprisesEntities db = new db_a91f79_zionenterprisesEntities();
        // GET: Employee
        public ActionResult EmpLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }


        [HttpPost]
        public ActionResult EmpLogin(EmpLogin emplogin,string ReturnUrl)
        {

            if (ModelState.IsValid)
            {
                var obj = db.Users.Where(a => a.Email.ToLower().Equals(emplogin.UserName.ToLower()) && a.Password_U.Equals(emplogin.Password) && a.Usertype.Contains("CashCounter")).FirstOrDefault();

                if (obj != null)
                {
                    Session["EmpId"] = obj.User_Id.ToString();
                    Session["pfCode"] = obj.PF_Code.ToString();
                    Session["EmpName"] = obj.Name.ToString();


                    string decodedUrl = "";
                    if (!string.IsNullOrEmpty(ReturnUrl))
                        decodedUrl = Server.UrlDecode(ReturnUrl);

                    //Login logic...

                    if (Url.IsLocalUrl(decodedUrl))
                    {
                        return Redirect(decodedUrl);
                    }
                    else
                    {
                        return RedirectToAction("Printreceipt", "Booking");
                    }


                }
                else
                {
                    ModelState.AddModelError("LoginAuth", "Username ro Password Is Incorrect");
                }
            }
            return View(emplogin);
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("EmpLogin", "Employee");
            
        }

        public ActionResult CancelCons()
        {

            string pfcode = Session["pfCode"].ToString();

            List<Receipt_details> rd = db.Receipt_details.Where(m => m.Pf_Code == pfcode && m.Status != "cancel").OrderByDescending(m => m.Receipt_Id).ToList();

            ViewBag.totalAmt = (from emp in rd
                                select emp.Charges_Total).Sum();

            return View(rd);
        }

        public ActionResult UpdateStatus(long id)
        {
            var recipt = db.Receipt_details.Where(m => m.Receipt_Id == id).FirstOrDefault();

            if(recipt != null)
            {
                recipt.Status = "cancel";
                db.Entry(recipt).State = EntityState.Modified;
                db.SaveChanges();

                TempData["Success"] = "Consignment Cancelled SuccessFully";
            }


            return RedirectToAction("CancelCons");
        }
    }
}