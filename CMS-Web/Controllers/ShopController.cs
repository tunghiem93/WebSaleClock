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
    public class ShopController : HQController
    {
        private CMSProductFactory _fac;
        private CMSCategoriesFactory _facCate;
        private CMSBrandsFactory _facBrand;
        private int PageSize = 12;
        public ShopController()
        {
            _fac = new CMSProductFactory();
            _facCate = new CMSCategoriesFactory();
            _facBrand = new CMSBrandsFactory();
            ViewBag.Range = GetListRangeSelectItem();
        }
        // GET: Shop
        public ActionResult Index()
        {
            try
            {
                ProductViewModels model = new ProductViewModels();
                //Category
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
                    var TotalProduct = model.ListProduct.Count;
                    if (TotalProduct % PageSize == 0)
                        model.TotalPage = TotalProduct / PageSize;
                    else
                        model.TotalPage = Convert.ToInt32(TotalProduct / PageSize) + 1;
                    model.ListProduct = model.ListProduct.Skip(0).Take(PageSize).ToList();
                    model.ListProductTopSales = model.ListProduct.Skip(0).Take(5).ToList();
                }
                return View(model);
            }
            catch (Exception ex)
            {
                // NSLog.Logger.Error("Index: ", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }

        public ActionResult Detail(string id)
        {
            ProductViewModels model = new ProductViewModels();
            try
            {
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Index", "Home");

                //Category
                model.ListCate = _facCate.GetList().OrderByDescending(x => x.CreatedDate).Skip(0).Take(10).ToList();
                //Brand
                model.ListBrand = _facBrand.GetList().OrderByDescending(x => x.CreatedDate).Skip(0).Take(5).ToList();
                /* get list product */
                var data = _fac.GetList();
                model.ProductModel = data.Where(o => o.Id.Equals(id)).FirstOrDefault();
                if(model.ProductModel != null)
                {
                    model.ProductModel.ListImages = _fac.GetListImageOfProduct(id);
                    if(model.ProductModel.ListImages != null && model.ProductModel.ListImages.Any())
                    {
                        model.ProductModel.ListImages.ForEach(o =>
                        {
                            if (!string.IsNullOrEmpty(o.ImageURL))
                                o.ImageURL = Commons._PublicImages + "Products/" + o.ImageURL;
                            else
                                o.ImageURL = "";
                        });
                        model.ProductModel.ImageURL = model.ProductModel.ListImages.Select(o => o.ImageURL).FirstOrDefault();
                    }


                    /* get product sample */
                    model.ListSameProduct = data.Where(o => o.CategoryId.Equals(model.ProductModel.CategoryId)).OrderBy(o => o.CreatedDate).ToList();
                    if(model.ListSameProduct != null && model.ListSameProduct.Any())
                    {
                        var dataImg = _fac.GetListImage();
                        model.ListSameProduct.ForEach(o =>
                        {
                            o.ImageURL = dataImg.Where(z => z.ProductId.Equals(o.Id)).Select(z => z.ImageURL).FirstOrDefault();
                            if (!string.IsNullOrEmpty(o.ImageURL))
                                o.ImageURL = Commons._PublicImages + "Products/" + o.ImageURL;
                        });
                    }

                    model.ListProduct = data.OrderByDescending(x => x.CreatedDate).Skip(0).Take(10).ToList();
                    if (model.ListProduct != null && model.ListProduct.Any())
                    {
                        var dataImg = _fac.GetListImage();
                        model.ListProduct.ForEach(o =>
                        {
                            o.ImageURL = dataImg.Where(z => z.ProductId.Equals(o.Id)).Select(z => z.ImageURL).FirstOrDefault();
                            if (!string.IsNullOrEmpty(o.ImageURL))
                                o.ImageURL = Commons._PublicImages + "Products/" + o.ImageURL;
                        });
                        model.ListProductTopSales = model.ListProduct.Skip(0).Take(5).ToList();
                    }
                }


            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("Shop_Detail : ", ex);
            }
            return View(model);
        }

        public ActionResult LoadDataPagging(int pageIndex)
        {
            ProductViewModels model = new ProductViewModels();
            try
            {
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