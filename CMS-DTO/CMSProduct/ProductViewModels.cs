using CMS_DTO.CMSBrand;
using CMS_DTO.CMSCategories;
using CMS_DTO.CMSCompany;
using CMS_DTO.CMSNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSProduct
{
    public class ProductViewModels
    {
        public List<CMS_ProductsModels> ListProduct { get; set; }
        public List<CMS_ProductsModels> ListProductTopSales { get; set; }
        public CMS_CompanyModels Company { get; set; }
        public List<CMS_NewsModels> ListNews { get; set; }
        
        public CMSCategoriesModels CateModel { get; set; }
        public string CateID { get; set; }
        public List<CMSCategoriesModels> ListCate { get; set; }
        public string BrandID { get; set; }
        public List<CMSBrandsModels> ListBrand { get; set; }
        public int RangeType { get; set; }
        public int TotalProduct { get; set; }
        public bool IsAddMore { get; set; }
        public int TotalPage { get; set; }
        public string Key { get; set; }
        public bool IsOrther { get; set; }
        public ProductViewModels()
        {
            CateModel = new CMSCategoriesModels();
            ListCate = new List<CMSCategoriesModels>();
            ListBrand = new List<CMSBrandsModels>();
            ListProduct = new List<CMS_ProductsModels>();
            ListProductTopSales = new List<CMS_ProductsModels>();
            Company = new CMS_CompanyModels();
            ListNews = new List<CMS_NewsModels>();
        }
    }
}
