using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Entity.Entity
{
    public class CMS_Brands : CMS_EntityBase
    {
        public string Id { get; set; }
        public string BrandName { get; set; }
        public string BrandCode { get; set; }
        public string Short_Description { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public virtual List<CMS_Products> Products { get; set; }
    }
}
