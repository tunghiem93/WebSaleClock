using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSSession
{
    public class OrderCookie
    {
        public string ItemId { get; set; }
        public string UnitId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
