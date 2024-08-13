using DtdcCashCounter.EntityFr;
using DtdcCashCounter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DtdcCashCounter.Controllers
{
    public class AdminController : Controller
    {
        private db_a91f79_zionenterprisesEntities db = new db_a91f79_zionenterprisesEntities();
        // GET: Admin
        public ActionResult AdminLogin(string returnUrl)
        {


            ViewBag.ReturnUrl = returnUrl;

            return View();

        }

        [HttpPost]
        public ActionResult AdminLogin(AdminLogin login, string ReturnUrl)
        {

            var obj = db.Admins.Where(a => a.Username.Equals(login.UserName) && a.A_Password.Equals(login.Password)).FirstOrDefault();

            if (obj != null)
            {
                Session["Admin"] = obj.A_Id.ToString();
                Session["UserName"] = obj.Username.ToString();

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
                    return RedirectToAction("FranchiseeList");
                }

                
            }
            return View();
        }


        [SessionAdmin]
        public ActionResult CreateUser()
        {

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "CashCounter", Value = "CashCounter" });

            items.Add(new SelectListItem { Text = "Billing", Value = "Billing" });

            ViewBag.Usertype = items;



            ViewBag.PF_Code = new SelectList(db.Franchisees, "PF_Code", "PF_Code");
            return View();

        }
        [SessionAdmin]
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "CashCounter", Value = "CashCounter" });

            items.Add(new SelectListItem { Text = "Billing", Value = "Billing" });


            if (ModelState.IsValid)
            {


                db.Users.Add(user);
                db.SaveChanges();




                //////////Alert Afte Success///
                ViewBag.Success = " Added Successfully...!!!";
                ////////////////////////////////////////
                ViewBag.PF_Code = new SelectList(db.Franchisees, "PF_Code", "PF_Code", user.PF_Code);
                ViewBag.Usertype = items;
                ModelState.Clear();

                return View(new User());
            }

            ViewBag.PF_Code = new SelectList(db.Franchisees, "PF_Code", "PF_Code", user.PF_Code);
            ViewBag.Usertype = items;


            return View(user);

        }

        [SessionAdmin]
        public ActionResult EditUser(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.PF_Code = new SelectList(db.Franchisees, "PF_Code", "F_Address", user.PF_Code);
            return View(user);
        }

        // POST: demo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "User_Id,Name,Email,Contact_no,PF_Code,Password_U,Usertype,Datetime_User")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserList");
            }
            ViewBag.PF_Code = new SelectList(db.Franchisees, "PF_Code", "F_Address", user.PF_Code);
            return View(user);
        }


        [SessionAdmin]
        public ActionResult AddFranchisee()
        {
            return View();
        }


        [SessionAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFranchisee(Franchisee franchisee)
        {
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }


            if (ModelState.IsValid)
            {
                db.Franchisees.Add(franchisee);
                db.SaveChanges();



                //Adding Eantries To the Sector Table
                var sectornamelist = db.sectorNames.ToList();

                var pfcode = (from u in db.Franchisees
                              where u.PF_Code == franchisee.PF_Code
                              select u).FirstOrDefault();
                if (pfcode != null)
                {
                    foreach (var i in sectornamelist)
                    {
                        Sector sn = new Sector();

                        sn.Pf_code = pfcode.PF_Code;
                        sn.Sector_Name = i.sname;

                        if(sn.Sector_Name == "Within City")
                        {
                            sn.Pincode_values = "411001 - 411100";
                        }
                        else if (sn.Sector_Name == "Within Zone")
                        {
                            sn.Pincode_values = "360000-400000,450000-490000";
                        }
                        else if (sn.Sector_Name == "Within State")
                        {
                            sn.Pincode_values = "400000-450000";
                        }
                        else if (sn.Sector_Name == "Metro")
                        {
                            sn.Pincode_values = "110000-110505,500001-500873,560000-560099,600001-600099,700001-700099";
                        }
                        else if (sn.Sector_Name == "Estern And Non Estern")
                        {
                            sn.Pincode_values = "780000-800000,170000-180000";
                        }
                        else if (sn.Sector_Name == "Jammu And Kashmir")
                        {
                            sn.Pincode_values = "180000-200000";
                        }
                        else if (sn.Sector_Name == "Rest Of India")
                        {
                            sn.Pincode_values = "000000";
                        }
                        else
                        {
                            sn.Pincode_values = null;
                        }



                        db.Sectors.Add(sn);

                        db.SaveChanges();

                    }
                }
                //////////////////////////////////////////////

                ///Adding Eantries To New Company For Cash Counter ///               




                var Companyid = "Cash_" + franchisee.PF_Code;


                var secotrs = db.Sectors.Where(m => m.Pf_code == franchisee.PF_Code).ToList();

                Company cm = new Company();
                cm.Company_Id = Companyid;
                cm.Pf_code = franchisee.PF_Code;
                cm.Phone = 9657570808;
                cm.Email = "khengarebalu@gmail.com";
                db.Companies.Add(cm);



                try
                {
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges

                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }


                foreach (var i in secotrs)
                {
                    Ratem dox = new Ratem();
                    Nondox ndox = new Nondox();
                    express_cargo cs = new express_cargo();

                    dox.Company_id = Companyid;
                    dox.Sector_Id = i.Sector_Id;
                    dox.NoOfSlab = 2;
                    //dox.CashCounter = true;

                    ndox.Company_id = Companyid;
                    ndox.Sector_Id = i.Sector_Id;
                    ndox.NoOfSlabN = 2;
                    ndox.NoOfSlabS = 2;
                    // ndox.CashCounterNon = true;


                    cs.Company_id = Companyid;
                    cs.Sector_Id = i.Sector_Id;
                    
                    // cs.CashCounterExpr = true;

                    db.Ratems.Add(dox);
                    db.Nondoxes.Add(ndox);
                    db.express_cargo.Add(cs);


                }

                for (int i = 0; i < 5; i++)
                {
                    dtdcPlu dtplu = new dtdcPlu();
                    Dtdc_Ptp stptp = new Dtdc_Ptp();

                    if (i == 0)
                    {
                        dtplu.destination = "City Plus";
                        stptp.dest = "City";
                    }
                    else if (i == 1)
                    {
                        dtplu.destination = "Zonal Plus/Blue";
                        stptp.dest = "Zonal";

                    }
                    else if (i == 2)
                    {
                        dtplu.destination = "Metro Plus/Blue";
                        stptp.dest = "Metro";
                    }
                    else if (i == 3)
                    {
                        dtplu.destination = "National Plus/Blue";
                        stptp.dest = "National";
                    }
                    else if (i == 4)
                    {
                        dtplu.destination = "Regional Plus";
                        stptp.dest = "Regional";
                    }

                    dtplu.Company_id = Companyid;
                    // dtplu.CashCounterPlus = true;
                    stptp.Company_id = Companyid;


                    db.dtdcPlus.Add(dtplu);
                    db.Dtdc_Ptp.Add(stptp);

                }

                db.SaveChanges();

                /////////////////////////////////////////////////////
                //////////Alert Afte Success///
                TempData["Success1"] = " Added Successfully...!!!";
                ////////////////////////////////////////
                ModelState.Clear();

                return RedirectToAction("Add_SectorPin", new { PfCode = franchisee.PF_Code });
            }

            return View(franchisee);

        }

        [SessionAdmin]
        public ActionResult Add_SectorPin(string PfCode)
        {
            string Pf = PfCode; /*Session["PfID"].ToString();*/



            List<Sector> st = (from u in db.Sectors
                               where u.Pf_code == Pf                              
                               select u).ToList();
            ViewBag.pfcode = PfCode;//stored in hidden format on the view


            return View(st);
        }



        [SessionAdmin]
        [HttpPost]
        public ActionResult Add_SectorPin(FormCollection fc, string pfcode)
        {
            string Pf = pfcode;

            ViewBag.pfcode = pfcode;//stored in hidden format on the view if All fields not filled

            var sectoridarray = fc.GetValues("item.Sector_Id");
            var pincodearayy = fc.GetValues("item.Pincode_values");


            for (int i = 0; i < sectoridarray.Count(); i++)
            {

                Sector str = db.Sectors.Find(Convert.ToInt16(sectoridarray[i]));

                if (pincodearayy[i] == "")
                {
                    pincodearayy[i] = null;
                }


                str.Pincode_values = pincodearayy[i];
                db.Entry(str).State = EntityState.Modified;

            }

            int result = pincodearayy.Count(s => s == null);

            if (result > 0)
            {
                ModelState.AddModelError("PinError", "All Fields Are Compulsary");

                List<Sector> stt = (from u in db.Sectors
                                    where u.Pf_code == Pf
                                    && u.Pincode_values == null
                                    select u).ToList();
                return View(stt);
            }
            else
            {
                db.SaveChanges();
                TempData["Success"] = "Sectors Added Successfully!";
            }


            List<Sector> st = (from u in db.Sectors
                               where u.Pf_code == Pf

                               select u).ToList();

            return View(st);
        }

        [SessionAdmin]
        public ActionResult Add_SectorPinEdit(string PfCode)
        {
            string Pf = PfCode; /*Session["PfID"].ToString();*/



            List<Sector> st = (from u in db.Sectors
                               where u.Pf_code == Pf                               
                               select u).ToList();
            ViewBag.pfcode = PfCode;//stored in hidden format on the view

            return View("Add_SectorPin", st);
        }

        public ActionResult Edit(string PfCode)
        {
            if (PfCode == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Franchisee franchisee = db.Franchisees.Find(PfCode);
            if (franchisee == null)
            {
                return HttpNotFound();
            }
            return View(franchisee);
        }

        // POST: demo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "F_Id,PF_Code,F_Address,OwnerName,BranchName,GstNo,Franchisee_Name,ContactNo,Branch_Area,Datetime_Fr")] Franchisee franchisee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(franchisee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserList");
            }
            return View(franchisee);
        }


        public ActionResult ImportCsv()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportCsv(HttpPostedFileBase postedFile)
        {



            string filePath = string.Empty;
            if (postedFile != null)
            {

                string path = Server.MapPath("~/Content/");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                var readcsv = System.IO.File.ReadAllText(filePath);

                string[] csvfilerecord = readcsv.Split('\n');

                foreach (var row in csvfilerecord)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        var cells = row.Split(',');


                        Destination card = new Destination();

                        card.Pincode = cells[1];
                        card.Name = cells[2];
                        card.State_ = cells[3];

                        db.Destinations.Add(card);

                        db.SaveChanges();


                    }

                }
                

            }


            return View();
        }

        public ActionResult ImportService()
        {

            //ViewBag.PF_Code = new SelectList(db.Franchisees, "PF_Code", "PF_Code");

            return View();
        }

        [HttpPost]
        public ActionResult ImportService(HttpPostedFile postedFile )
        {

            string filePath = string.Empty;
            if (postedFile != null)
            {

                string path = Server.MapPath("~/Content/");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                var readcsv = System.IO.File.ReadAllText(filePath);

                string[] csvfilerecord = readcsv.Split('\n');

                foreach (var row in csvfilerecord)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        var cells = row.Split(',');


                        Service_list card = new Service_list();

                        card.Pincode = cells[1];
                        card.Service_ = cells[2];
                       // card.Pf_Code = PF_Code;

                        db.Service_list.Add(card);

                        db.SaveChanges();


                    }

                }



            }
          //  ViewBag.PF_Code = new SelectList(db.Franchisees, "PF_Code", "PF_Code");

            return View();

            
        }



        public ActionResult ImportServicelist()
        {
           // ViewBag.PF_Code = new SelectList(db.Franchisees, "PF_Code", "PF_Code");
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> ImportServicelist(HttpPostedFileBase httpPostedFileBase)
        {




            if (httpPostedFileBase != null)
            {
                HttpPostedFileBase file = httpPostedFileBase;

                await GizmosAsync(httpPostedFileBase);


            }



           // ViewBag.PF_Code = new SelectList(db.Franchisees, "PF_Code", "PF_Code");

            return View();
        }




        public async Task<ActionResult> GizmosAsync(HttpPostedFileBase postedFile)

        {
            string filePath = string.Empty;
            if (postedFile != null)
            {

                string path = Server.MapPath("~/Content/");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                var readcsv = System.IO.File.ReadAllText(filePath);

                string[] csvfilerecord = readcsv.Split('\n');

                foreach (var row in csvfilerecord)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        var cells = row.Split(',');


                        Service_list card = new Service_list();

                        string pin = cells[0].ToString();
                        string serv = cells[1].ToString();

                        card = db.Service_list.Where(m => m.Pincode == pin && m.Service_ == serv).FirstOrDefault();

                        

                        if (card == null)
                        {
                            Service_list card1 = new Service_list();

                            card1.Pincode = cells[0].ToString();
                            card1.Service_ = cells[1].ToString();
                           // card1.Pf_Code = pfcode;


                            db.Service_list.Add(card1);
                        }
                        else
                        {
                            db.Entry(card).State = EntityState.Modified;
                        }

                       

                        db.SaveChanges();


                    }

                }


            }


            return View("Gizmos");
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Adminlogin", "Admin");

        }


        public ActionResult FranchiseeList()
        {

            return View(db.Franchisees.ToList());
        }


        public ActionResult UserList()
        {

            return View(db.Users.ToList());
        }

        public ActionResult Destinationlist()
        {


            return View(db.Destinations.ToList());
        }

        [SessionAdmin]
        public ActionResult Consignmentlist(string id)
        {

            return View(db.Receipt_details.ToList());
        }




        #region Edit Consignments


        public ActionResult EditCons(long? id)
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
        public ActionResult EditCons([Bind(Include = "Receipt_Id,Consignment_No,Destination,sender_phone,Sender_Email,Sender,SenderCompany,SenderAddress,SenderCity,SenderState,SenderPincode,Reciepents_phone,Reciepents_Email,Reciepents,ReciepentCompany,ReciepentsAddress,ReciepentsCity,ReciepentsState,ReciepentsPincode,Shipmenttype,Shipment_Length,Shipment_Quantity,Shipment_Breadth,Shipment_Heigth,DivideBy,TotalNo,Actual_Weight,volumetric_Weight,DescriptionContent1,DescriptionContent2,DescriptionContent3,Amount1,Amount2,Amount3,Total_Amount,Insurance,Insuance_Percentage,Insuance_Amount,Charges_Amount,Charges_Service,Risk_Surcharge,Service_Tax,Charges_Total,Cash,Credit,Credit_Amount,secure_Pack,Passport,OfficeSunday,Shipment_Mode,Addition_charge,Addition_Lable,Discount,Pf_Code,User_Id,Datetime_Cons,Paid_Amount")] Receipt_details receipt_details)
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

        #endregion


        public ActionResult DeleteCons(string id)
        {
            Receipt_details receipt_details = db.Receipt_details.Where(m=>m.Consignment_No ==id).FirstOrDefault();
            db.Receipt_details.Remove(receipt_details);
            db.SaveChanges();
            return RedirectToAction("Consignmentlist");
        }




    }
}