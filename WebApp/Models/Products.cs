using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ProductInfo
    {
        public int slno
        {
            get;
            set;
        }
        public string Product_Name
        {
            get;
            set;
        }
        public decimal Price
        {
            get;
            set;
        }
        public string Url
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
    }
}