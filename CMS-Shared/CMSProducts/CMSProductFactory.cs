﻿using CMS_DTO.CMSImage;
using CMS_DTO.CMSProduct;
using CMS_Entity;
using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSProducts
{
    public class CMSProductFactory
    {
        public bool CreateOrUpdate(CMS_ProductsModels model,ref string msg)
        {
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(model.Id))
                        {
                            var _Id = Guid.NewGuid().ToString();
                            var e = new CMS_Products
                            {
                                Id = _Id,
                                CategoryId = model.CategoryId,
                                CreatedBy = model.CreatedBy,
                                CreatedDate = DateTime.Now,
                                TypeSize = model.TypeSize,
                                TypeState = model.TypeState,
                                Short_Description = model.Short_Description,
                                Description = model.Description,
                                ProductCode = model.ProductCode,
                                ProductName = model.ProductName,
                                Vendor = model.Vendor,
                                Information = model.Information,
                                ProductPrice = model.ProductPrice,
                                ProductExtraPrice = model.ProductExtraPrice,
                                UpdatedBy = model.UpdatedBy,
                                UpdatedDate = DateTime.Now,
                                IsActive  = model.IsActive
                            };
                            cxt.CMS_Products.Add(e);
                            
                            if(model.ListImages != null && model.ListImages.Any())
                            {
                                var _e = new List<CMS_Images>();
                                model.ListImages.ForEach(x =>
                                {
                                    _e.Add(new CMS_Images
                                    {
                                        Id = Guid.NewGuid().ToString(),
                                        CreatedBy = model.CreatedBy,
                                        CreatedDate = DateTime.Now,
                                        ImageURL = x.ImageURL,
                                        ProductId = _Id,
                                        UpdatedBy = model.UpdatedBy,
                                        UpdatedDate = DateTime.Now
                                    });
                                });
                                cxt.CMS_Images.AddRange(_e);
                            }
                        }
                        else
                        {
                            var e = cxt.CMS_Products.Find(model.Id);
                            if(e != null)
                            {
                                e.ProductName = model.ProductName;
                                e.ProductCode = model.ProductCode;
                                e.ProductPrice = model.ProductPrice;
                                e.ProductExtraPrice = model.ProductExtraPrice;
                                e.Short_Description = model.Short_Description;
                                e.Description = model.Description;
                                e.Vendor = model.Vendor;
                                e.Information = model.Information;
                                e.TypeSize = model.TypeSize;
                                e.TypeState = model.TypeState;
                                e.CategoryId = model.CategoryId;
                                e.UpdatedBy = model.UpdatedBy;
                                e.UpdatedDate = DateTime.Now;
                                e.IsActive = model.IsActive;
                            }

                            if (model.ListImages != null && model.ListImages.Any())
                            {
                                var _edel = cxt.CMS_Images.Where(x => x.ProductId.Equals(e.Id));
                                cxt.CMS_Images.RemoveRange(_edel);

                                var _e = new List<CMS_Images>();
                                model.ListImages.ForEach(x =>
                                {
                                    _e.Add(new CMS_Images
                                    {
                                        Id = Guid.NewGuid().ToString(),
                                        CreatedBy = model.CreatedBy,
                                        CreatedDate = DateTime.Now,
                                        ImageURL = x.ImageURL,
                                        ProductId = e.Id,
                                        UpdatedBy = model.UpdatedBy,
                                        UpdatedDate = DateTime.Now
                                    });
                                });
                                cxt.CMS_Images.AddRange(_e);
                            }
                        }
                        cxt.SaveChanges();
                        trans.Commit();
                    }
                    catch (Exception ex) {
                        Result = false;
                        msg = "Vui lòng kiểm tra đường truyền";
                        trans.Rollback();
                    }
                    finally
                    {
                        cxt.Dispose();
                    }
                }
            }
            return Result;
        }

        public bool Delete(string Id, ref string msg)
        {
            var Result = true;
            using (var cxt = new CMS_Context())
            {
                using (var trans = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        // Delete Image of Product
                        var _eImage = cxt.CMS_Images.Where(x => x.ProductId.Equals(Id));
                        cxt.CMS_Images.RemoveRange(_eImage);

                        var e = cxt.CMS_Products.Find(Id);
                        if (e != null)
                        {
                            cxt.CMS_Products.Remove(e);
                            cxt.SaveChanges();
                            trans.Commit();
                        }
                        else
                        {
                            msg = "Vui lòng kiểm tra đường truyền";
                        }
                    }catch(Exception)
                    {
                        Result = false;
                        msg = "Vui lòng kiểm tra đường truyền";
                        trans.Rollback();
                    }
                    finally
                    {
                        cxt.Dispose();
                    }
                }
            }
            return Result;
        }

        public CMS_ProductsModels GetDetail(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_Products.Join(cxt.CMS_Categories,
                                                    p =>p.CategoryId,
                                                    c => c.Id,
                                                    (p , c) => new { p, CategoryName = c.CategoryName})
                                             .Where(x=>x.p.Id.Equals(Id)).FirstOrDefault();
                    if(e != null)
                    {
                        var o = new CMS_ProductsModels
                        {
                            Id = e.p.Id,
                            CategoryId = e.p.CategoryId,
                            CreatedBy = e.p.CreatedBy,
                            CreatedDate = e.p.CreatedDate,
                            Short_Description = e.p.Short_Description,
                            Description = e.p.Description,
                            IsActive = e.p.IsActive,
                            ProductCode = e.p.ProductCode,
                            ProductName = e.p.ProductName,
                            ProductPrice = e.p.ProductPrice,
                            ProductExtraPrice = e.p.ProductExtraPrice,
                            Vendor = e.p.Vendor,
                            Information = e.p.Information,
                            TypeState = e.p.TypeState,
                            TypeSize = e.p.TypeSize,
                            UpdatedBy = e.p.UpdatedBy,
                            UpdatedDate = e.p.UpdatedDate,
                            CategoryName = e.CategoryName
                        };
                        return o;
                    }
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public List<CMS_ProductsModels> GetList()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Products.Join(cxt.CMS_Categories,
                                                    p => p.CategoryId,
                                                    c => c.Id,
                                                    (p, c) => new { p, CategoryName = c.CategoryName })
                                               .Select(x=> new CMS_ProductsModels
                                               {
                                                   Id = x.p.Id,
                                                   CategoryId = x.p.CategoryId,
                                                   CreatedBy = x.p.CreatedBy,
                                                   CreatedDate = x.p.CreatedDate,
                                                   Short_Description = x.p.Short_Description,
                                                   Description = x.p.Description,
                                                   IsActive = x.p.IsActive,
                                                   ProductCode = x.p.ProductCode,
                                                   ProductName = x.p.ProductName,
                                                   ProductPrice = x.p.ProductPrice,
                                                   ProductExtraPrice = x.p.ProductExtraPrice,
                                                   Vendor = x.p.Vendor,
                                                   Information = x.p.Information,
                                                   TypeSize = x.p.TypeSize,
                                                   TypeState = x.p.TypeState,
                                                   UpdatedBy = x.p.UpdatedBy,
                                                   UpdatedDate = x.p.UpdatedDate,
                                                   CategoryName = x.CategoryName
                                               }).ToList();
                    return data;
                }
            }
            catch (Exception) { }
            return null;
        }

        public List<CMS_ImagesModels> GetListImage()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Images
                                             .Select(x => new CMS_ImagesModels
                                             {
                                                 ImageName = x.ImageURL,
                                                 Id = x.Id,
                                                 ImageURL = x.ImageURL,
                                                 ProductId = x.ProductId
                                             }).ToList();
                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public List<CMS_ImagesModels> GetListImageOfProduct(string ProductId)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Images.Where(x => x.ProductId.Equals(ProductId))
                                             .Select(x => new CMS_ImagesModels
                                             {
                                                 ImageName = x.ImageURL,
                                                 Id = x.Id,
                                                 ImageURL = x.ImageURL,
                                                 ProductId = x.ProductId
                                             }).ToList();
                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public bool DeleteImage(string Id, ref string msg)
        {
            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_Images.Find(Id);
                    if(e != null)
                    {
                        msg = e.ImageURL;
                        cxt.CMS_Images.Remove(e);
                        cxt.SaveChanges();
                    }
                    else
                    {
                        result = false;
                        msg = "Vui lòng kiểm tra đường truyền";
                    }
                }
            }
            catch(Exception ex)
            {
                result = false;
                msg = "Vui lòng kiểm tra đường truyền";
            }
            return result;
        }
    }
}
