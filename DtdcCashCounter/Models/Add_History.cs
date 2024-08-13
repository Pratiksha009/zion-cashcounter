using DtdcCashCounter.EntityFr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 


namespace DtdcCashCounter.Models
{
    public class Add_History
    {
        db_a91f79_zionenterprisesEntities db = new db_a91f79_zionenterprisesEntities();
        
        public void addHistory(string Status,string pfcode,long ? userid,float ? amount,string Consignment_No,string phone)
        {
            
            wallet_History wh = new wallet_History();
            wh.H_Status = Status;
            wh.PF_Code = pfcode;
            wh.User_Id = userid;
            wh.Amount = Math.Round((Double)amount, 2);
            wh.datetime = GetLocalTime.GetDateTime();
            wh.consignment_no = Consignment_No;
            wh.mobile_no = phone;
            
            wh.User_Id= db.Receipt_details.OrderByDescending(p => p.Receipt_Id).Select(m=>m.User_Id).FirstOrDefault();

            db.wallet_History.Add(wh);
            db.SaveChanges();
        }
    }
}