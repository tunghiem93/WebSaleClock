using CMS_DTO.CMSBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMS_DTO.CMSBrand
{
    public class CMSBrandsModels : CMS_BaseModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên thương hiệu")]
        [MaxLength(60, ErrorMessage = "Tên thể loại tối đa 250 kí tự")]
        public string BrandName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mã thương hiệu")]
        [MaxLength(50, ErrorMessage = "Mã thương hiệu tối đa 50 kí tự")]
        public string BrandCode { get; set; }
        public bool IsActive { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string sStatus { get; set; }
        public int NumberOfProduct { get; set; }

        public CMSBrandsModels()
        {
            IsActive = true;
        }
    }
}
