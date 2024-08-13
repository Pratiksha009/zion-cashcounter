using DtdcCashCounter.EntityFr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DtdcCashCounter.Controllers
{
    public class WalletController : Controller
    {
        db_a91f79_zionenterprisesEntities db = new db_a91f79_zionenterprisesEntities();
        // GET: Wallet
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WalletHistory(string phone)
        {
            List<wallet_History> wallet_History = db.wallet_History.Where(m => m.mobile_no == phone).ToList();
            return View(wallet_History);
        }


    }
}