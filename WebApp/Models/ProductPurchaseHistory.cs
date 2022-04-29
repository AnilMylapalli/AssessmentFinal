using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ProductPurchaseHistory
    {
        public int PID { set; get; }
        public int SID { set; get; }
        public string Product { set; get; }
        public decimal Price { set; get; }
        public string url { set; get; }
        public string Description { set; get; }
        public int TotalAmount { set; get; }
        public string UserName { set; get; }
        public DateTime Purchase_Date { set; get; }
        public int Quantity { set; get; }
    }
}