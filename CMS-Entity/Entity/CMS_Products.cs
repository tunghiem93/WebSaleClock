using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Entity
{
    public class CMS_Products : CMS_EntityBase
    {
        public string Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Short_Description { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public string Vendor { get; set; }
        public int TypeSize { get; set; }
        public int TypeState { get; set; }
        public decimal ProductPrice { get; set; }
        public string CategoryId { get; set; }
        public string BrandId { get; set; }

        public virtual CMS_Categories Category { get; set; }
        public virtual CMS_Brands Brand { get; set; }
        public virtual List<CMS_Images> Images { get; set; }
    }
}
