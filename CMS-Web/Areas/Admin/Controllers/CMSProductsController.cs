using CMS_DTO.CMSImage;
using CMS_DTO.CMSProduct;
using CMS_Shared;
using CMS_Shared.CMSProducts;
using CMS_Shared.Utilities;
using CMS_Web.Web.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    [NuAuth]
    public class CMSProductsController : HQController
    {
        private CMSProductFactory _factory;
        public CMSProductsController()
        {
            _factory = new CMSProductFactory();
            ViewBag.Category = GetListCategorySelectItem();
            ViewBag.Brand = GetListBrandSelectItem();
            ViewBag.State = getListState();
            ViewBag.Size = getListSize();
        }
        // GET: Admin/CMSProduct
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadGrid()
        {
            var model = _factory.GetList();
            model.ForEach(x =>
            {
                x.sStatus = x.IsActive ? "Kích hoạt" : "Chưa kích hoạt";
            });
            return PartialView("_ListData", model);
        }

        public ActionResult Create()
        {
            CMS_ProductsModels model = new CMS_ProductsModels();
            model.TypeState = (byte)Commons.EStateType.Avalable;
            return PartialView("_Create", model);
        }

        public CMS_ProductsModels GetDetail(string Id)
        {
            CMS_ProductsModels model = _factory.GetDetail(Id);
            try
            {
                if (model != null)
                {
                    if (!string.IsNullOrEmpty(model.ImageURL))
                        model.ImageURL = Commons.HostImage + model.ImageURL;
                    if (model.ListImg != null && model.ListImg.Count > 0)
                    {
                        model.ListImg.ForEach(o => {
                            if (!string.IsNullOrEmpty(o.ImageURL))
                            {
                                o.ImageURL = Commons.HostImage + "Products/" + o.ImageURL;
                            }
                            o.IsDelete = false;
                        });
                    }
                    return model;
                }
                else
                {
                    model = new CMS_ProductsModels();
                    return model;
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Product_Detail: ", ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult Create(CMS_ProductsModels model)
        {
            try
            {
                byte[] photoByte = null;
                if (model.PictureUpload != null && model.PictureUpload.ContentLength > 0)
                {
                    Byte[] imgByte = new Byte[model.PictureUpload.ContentLength];
                    model.PictureUpload.InputStream.Read(imgByte, 0, model.PictureUpload.ContentLength);
                    model.PictureByte = imgByte;
                    model.RawImageUrl = Guid.NewGuid() + Path.GetExtension(model.PictureUpload.FileName);
                    model.PictureUpload = null;
                    photoByte = imgByte;
                }
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Create", model);
                }
                if (!string.IsNullOrEmpty(model.RawImageUrl))
                {
                    model.ListImageUrl.Add(model.RawImageUrl);
                }
                //========
                Dictionary<int, byte[]> lstImgByte = new Dictionary<int, byte[]>();
                var ListImage = model.ListImg.Where(x => !x.IsDelete).ToList();
                foreach (var item in ListImage)
                {
                    if (item.PictureUpload != null && item.PictureUpload.ContentLength > 0)
                    {
                        Byte[] imgByte = new Byte[item.PictureUpload.ContentLength];
                        item.PictureUpload.InputStream.Read(imgByte, 0, item.PictureUpload.ContentLength);
                        item.PictureByte = imgByte;
                        item.ImageURL = Guid.NewGuid() + Path.GetExtension(item.PictureUpload.FileName);
                        item.PictureUpload = null;
                        lstImgByte.Add(item.OffSet, imgByte);
                        model.ListImageUrl.Add(item.ImageURL);
                    }
                }

                var msg = "";
                var result = _factory.CreateOrUpdate(model, ref msg);
                if (result)
                {
                    if (!string.IsNullOrEmpty(model.RawImageUrl) && model.PictureByte != null)
                    {
                        var path = Server.MapPath("~/Uploads/Products/" + model.RawImageUrl);
                        MemoryStream ms = new MemoryStream(photoByte, 0, photoByte.Length);
                        ms.Write(photoByte, 0, photoByte.Length);
                        System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                        ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.RawImageUrl, ref photoByte, 400, Commons.WidthProduct, Commons.HeightProduct);
                    }

                    foreach (var item in ListImage)
                    {
                        if (!string.IsNullOrEmpty(item.ImageURL) && item.PictureByte != null)
                        {
                            var path = Server.MapPath("~/Uploads/Products/" + item.ImageURL);
                            MemoryStream ms = new MemoryStream(lstImgByte[item.OffSet], 0, lstImgByte[item.OffSet].Length);
                            ms.Write(lstImgByte[item.OffSet], 0, lstImgByte[item.OffSet].Length);
                            System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                            ImageHelper.Me.SaveCroppedImage(imageTmp, path, item.ImageURL, ref photoByte, 400, Commons.WidthProduct, Commons.HeightProduct);
                        }
                    }
                    return RedirectToAction("Index");
                }
                    
                ModelState.AddModelError("ProductCode", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Create", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Create", model);
            }
        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            var model = GetDetail(Id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(CMS_ProductsModels model)
        {
            try
            {
                List<string> ListNotChangeImg = new List<string>();
                byte[] photoByte = null;

                if (!string.IsNullOrEmpty(model.ImageURL))
                {
                    model.ImageURL = model.ImageURL.Replace(Commons._PublicImages, "").Replace(Commons.Image200_100, "");
                    model.RawImageUrl = model.ImageURL;
                }
                if (model.PictureUpload != null && model.PictureUpload.ContentLength > 0)
                {
                    Byte[] imgByte = new Byte[model.PictureUpload.ContentLength];
                    model.PictureUpload.InputStream.Read(imgByte, 0, model.PictureUpload.ContentLength);
                    model.PictureByte = imgByte;
                    model.RawImageUrl = Guid.NewGuid() + Path.GetExtension(model.PictureUpload.FileName);
                    model.PictureUpload = null;
                    photoByte = imgByte;
                    if (!string.IsNullOrEmpty(model.ImageURL))
                    {
                        model.ImageURL = model.ImageURL.Replace(Commons._PublicImages, "").Replace(Commons.Image200_100, "");
                        ListNotChangeImg.Add(model.ImageURL);
                    }
                }
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }

                if (!string.IsNullOrEmpty(model.RawImageUrl))
                {
                    model.ListImageUrl.Add(model.RawImageUrl);
                }

                //========
                Dictionary<int, byte[]> lstImgByte = new Dictionary<int, byte[]>();
                var ListImage = model.ListImg.Where(x => !x.IsDelete).ToList();
                foreach (var item in ListImage)
                {
                    if (item.PictureUpload != null && item.PictureUpload.ContentLength > 0)
                    {
                        if (!string.IsNullOrEmpty(item.ImageURL))
                        {
                            item.ImageURL = item.ImageURL.Replace(Commons._PublicImages, "").Replace(Commons.Image200_100, "");
                            ListNotChangeImg.Add(item.ImageURL);
                        }

                        Byte[] imgByte = new Byte[item.PictureUpload.ContentLength];
                        item.PictureUpload.InputStream.Read(imgByte, 0, item.PictureUpload.ContentLength);
                        item.PictureByte = imgByte;
                        item.ImageURL = Guid.NewGuid() + Path.GetExtension(item.PictureUpload.FileName);
                        item.PictureUpload = null;
                        lstImgByte.Add(item.OffSet, imgByte);
                        model.ListImageUrl.Add(item.ImageURL);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.ImageURL))
                        {
                            item.ImageURL = item.ImageURL.Replace(Commons._PublicImages, "").Replace(Commons.Image200_100, "");
                            model.ListImageUrl.Add(item.ImageURL);
                        }
                    }
                }

                var msg = "";
                var result = _factory.CreateOrUpdate(model, ref msg);
                if (result)
                {
                    if (ListNotChangeImg != null && ListNotChangeImg.Any())
                    {
                        //Delete image on forder
                        foreach (var item in ListNotChangeImg)
                        {
                            if (!item.Equals(Commons.Image200_100))
                            {
                                var filePath = Server.MapPath("~/Uploads/Products/" + item);
                                if (System.IO.File.Exists(filePath))
                                {
                                    System.IO.File.Delete(filePath);
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(model.RawImageUrl) && model.PictureByte != null)
                    {
                        var path = Server.MapPath("~/Uploads/Products/" + model.RawImageUrl);
                        MemoryStream ms = new MemoryStream(photoByte, 0, photoByte.Length);
                        ms.Write(photoByte, 0, photoByte.Length);
                        System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                        ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.RawImageUrl, ref photoByte, 400, Commons.WidthProduct, Commons.HeightProduct);
                    }

                    foreach (var item in ListImage)
                    {
                        if (!string.IsNullOrEmpty(item.ImageURL) && item.PictureByte != null)
                        {
                            var path = Server.MapPath("~/Uploads/Products/" + item.ImageURL);
                            MemoryStream ms = new MemoryStream(lstImgByte[item.OffSet], 0, lstImgByte[item.OffSet].Length);
                            ms.Write(lstImgByte[item.OffSet], 0, lstImgByte[item.OffSet].Length);
                            System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                            ImageHelper.Me.SaveCroppedImage(imageTmp, path, item.ImageURL, ref photoByte, 400, Commons.WidthProduct, Commons.HeightProduct);
                        }
                    }
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("ProductCode", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Edit", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Edit", model);
            }
        }

        [HttpGet]
        public ActionResult View(string Id)
        {
            var model = GetDetail(Id);
            var _OffSet = 0;
            if (model.ListImageUrl != null && model.ListImageUrl.Any())
            {
                model.ListImageUrl.ForEach(x =>
                {
                    x = Commons.HostImage + "Products/" + x;
                });
            }
            return PartialView("_View", model);
        }

        [HttpGet]
        public ActionResult Delete(string Id)
        {
            var model = GetDetail(Id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CMS_ProductsModels model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Delete", model);
                }
                var msg = "";
                var _LstImageOfProduct = _factory.GetListImageOfProduct(model.Id);
                var result = _factory.Delete(model.Id, ref msg);
                if (result)
                {
                    if(_LstImageOfProduct != null && _LstImageOfProduct.Any())
                    {
                        foreach(var item in _LstImageOfProduct)
                        {
                            // delete image for folder
                            if (System.IO.File.Exists(Server.MapPath("~/Uploads/Products/" + item.ImageURL)))
                            {
                                ImageHelper.Me.TryDeleteImageUpdated(Server.MapPath("~/Uploads/Products/" + item.ImageURL));
                            }
                        }
                    }
                    return RedirectToAction("Index");
                }
                    
                ModelState.AddModelError("ProductCode", msg);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
        }
        
        public PartialViewResult AddImageItem(int OffSet)
        {
            ImageProduct model = new ImageProduct();
            model.OffSet = OffSet;
            return PartialView("_ImageItemProduct", model);
        }        
    }
}