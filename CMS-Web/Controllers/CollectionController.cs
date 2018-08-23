using CMS_DTO.CMSCategories;
using CMS_DTO.CMSProduct;
using CMS_Shared;
using CMS_Shared.CMSBrands;
using CMS_Shared.CMSCategories;
using CMS_Shared.CMSProducts;
using CMS_Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class CollectionController : HQController
    {
        private CMSProductFactory _fac;
        private CMSCategoriesFactory _facCate;
        private CMSBrandsFactory _facBrand;
        private int PageSize = 12;
        public CollectionController()
        {
            _fac = new CMSProductFactory();
            _facCate = new CMSCategoriesFactory();
            _facBrand = new CMSBrandsFactory();
            ViewBag.Range = GetListRangeSelectItem();
        }

        // GET: Collection
        public ActionResult Index()
        {
            try
            {
                ProductViewModels model = new ProductViewModels();
                model.ListCate = _facCate.GetList().OrderByDescending(x => x.CreatedDate).ToList();
                if (model.ListCate != null && model.ListCate.Any())
                {
                    model.ListCate.ForEach(x =>
                    {
                        x.Alias = CommonHelper.RemoveUnicode(x.CategoryName.Trim().Replace(" ", "-")).ToLower();
                        x.ImageURL = Commons.HostImage + "Categories/" + x.ImageURL;
                            
                    });
                }
                return View(model);
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Index: ", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }

        public ActionResult Detail(string q)
        {
            try
            {
                ProductViewModels model = new ProductViewModels();
                model.ListCate = _facCate.GetList().OrderByDescending(x => x.CreatedDate).Skip(0).Take(10).ToList();
                if (model.ListCate != null && model.ListCate.Any())
                {
                    model.ListCate.ForEach(o =>
                    {
                        o.Alias = CommonHelper.RemoveUnicode(o.CategoryName.Trim().Replace(" ", "-")).ToLower();
                    });
                }
                //Category
                model.ListBrand = _facBrand.GetList().OrderByDescending(x => x.CreatedDate).Skip(0).Take(5).ToList();
                //Product
                model.ListProduct = _fac.GetList().OrderByDescending(x => x.CreatedDate).ToList();
                var dataImage = _fac.GetListImage();
                if (model.ListProduct != null && model.ListProduct.Any())
                {
                    model.ListProduct.ForEach(x =>
                    {
                        x.Alias = CommonHelper.RemoveUnicode(x.ProductName.Trim().Replace(" ", "-")).ToLower();
                        var _Image = dataImage.FirstOrDefault(z => z.ProductId.Equals(x.Id));
                        if (_Image != null)
                        {
                            x.ImageURL = _Image.ImageURL;
                            if (!string.IsNullOrEmpty(x.ImageURL))
                            {
                                x.ImageURL = Commons._PublicImages + "Products/" + x.ImageURL;
                            }
                            else
                            {
                                x.ImageURL = "";
                            }
                        }
                    });
                    q = q.Trim().Replace("-", " ");
                    model.ListProductTopSales = model.ListProduct.Skip(0).Take(5).ToList();
                    var TotalProduct = model.ListProduct.Count(o => CommonHelper.RemoveUnicode(o.CategoryName.Trim()).ToLower().Equals(q.ToLower()));
                    if (TotalProduct % PageSize == 0)
                        model.TotalPage = TotalProduct / PageSize;
                    else
                        model.TotalPage = Convert.ToInt32(TotalProduct / PageSize) + 1;
                    model.ListProduct = model.ListProduct.Where(o=> CommonHelper.RemoveUnicode(o.CategoryName.Trim()).ToLower().Equals(q.ToLower())).Skip(0).Take(PageSize).ToList();
                    
                }
                if (!string.IsNullOrEmpty(q))
                {
                    var dataDetail = _facCate.GetList().Where(o => CommonHelper.RemoveUnicode(o.CategoryName.Trim()).ToLower().Equals(q.ToLower())).FirstOrDefault();
                    if (dataDetail != null)
                    {
                        if (dataDetail.ImageURL != null)
                        {
                            dataDetail.ImageURL = Commons.HostImage + "Categories/" + dataDetail.ImageURL;
                        }
                        model.CateModel = dataDetail;
                        return View(model);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                //NSLog.Logger.Error("GetDetail: ", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }

        public ActionResult LoadDataPagging(int pageIndex,string cateId)
        {
            ProductViewModels model = new ProductViewModels();
            try
            {
                //Product
                model.ListProduct = _fac.GetList().Where(o => o.CategoryId.Equals(cateId)).OrderByDescending(x => x.CreatedDate).ToList();
                var dataImage = _fac.GetListImage();
                if (model.ListProduct != null && model.ListProduct.Any())
                {
                    model.ListProduct.ForEach(x =>
                    {
                        x.Alias = CommonHelper.RemoveUnicode(x.ProductName.Trim().Replace(" ", "-")).ToLower();
                        var _Image = dataImage.FirstOrDefault(z => z.ProductId.Equals(x.Id));
                        if (_Image != null)
                        {
                            x.ImageURL = _Image.ImageURL;
                            if (!string.IsNullOrEmpty(x.ImageURL))
                            {
                                x.ImageURL = Commons._PublicImages + "Products/" + x.ImageURL;
                            }
                            else
                            {
                                x.ImageURL = "";
                            }
                        }
                    });
                    model.ListProduct = model.ListProduct.Skip((pageIndex - 1) * PageSize).Take(PageSize).ToList();
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("LoadDataPagging :", ex);
            }
            return PartialView("_ListItem", model);
        }
    }
}