using CMS_DTO.CMSCategories;
using CMS_DTO.CMSProduct;
using CMS_Shared;
using CMS_Shared.CMSBrands;
using CMS_Shared.CMSCategories;
using CMS_Shared.CMSProducts;
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
        private int PageSize = 2;
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

        public ActionResult Detail(string id)
        {
            try
            {
                ProductViewModels model = new ProductViewModels();
                model.ListCate = _facCate.GetList().OrderByDescending(x => x.CreatedDate).Skip(0).Take(10).ToList();
                //Category
                model.ListBrand = _facBrand.GetList().OrderByDescending(x => x.CreatedDate).Skip(0).Take(5).ToList();
                //Product
                model.ListProduct = _fac.GetList().OrderByDescending(x => x.CreatedDate).ToList();
                var dataImage = _fac.GetListImage();
                if (model.ListProduct != null && model.ListProduct.Any())
                {
                    model.ListProduct.ForEach(x =>
                    {
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
                    var TotalProduct = model.ListProduct.Count(o => o.CategoryId.Equals(id));
                    if (TotalProduct % PageSize == 0)
                        model.TotalPage = TotalProduct / PageSize;
                    else
                        model.TotalPage = Convert.ToInt32(TotalProduct / PageSize) + 1;
                    model.ListProduct = model.ListProduct.Where(o=>o.CategoryId.Equals(id)).Skip(0).Take(PageSize).ToList();
                    model.ListProductTopSales = model.ListProduct.Skip(0).Take(5).ToList();
                }
                if (!string.IsNullOrEmpty(id))
                {
                    var dataDetail = _facCate.GetDetail(id);
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