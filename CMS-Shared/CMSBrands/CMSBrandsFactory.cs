using CMS_DTO.CMSBrand;
using CMS_Entity;
using CMS_Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.CMSBrands
{
    public class CMSBrandsFactory
    {
        public bool CreateOrUpdate(CMSBrandsModels model, ref string Id, ref string msg)
        {
            var result = true;
            using (var cxt = new CMS_Context())
            {
                using (var beginTran = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        var _IsExits = cxt.CMS_Categories.Any(x => (x.CategoryCode.Equals(model.BrandCode) || x.CategoryName.Equals(model.BrandName)) && (string.IsNullOrEmpty(model.Id) ? 1 == 1 : !x.Id.Equals(model.Id)));
                        if (_IsExits)
                        {
                            result = false;
                            msg = "Mã thương hiệu hoặc tên thương hiệu đã tồn tại";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(model.Id))
                            {
                                var _Id = Guid.NewGuid().ToString();
                                var e = new CMS_Brands()
                                {
                                    BrandCode = model.BrandCode,
                                    BrandName = model.BrandName,
                                    CreatedBy = model.CreatedBy,
                                    CreatedDate = DateTime.Now,
                                    Description = model.Description,
                                    IsActive = model.IsActive,
                                    UpdatedBy = model.UpdatedBy,
                                    UpdatedDate = DateTime.Now,
                                    ImageURL = model.ImageURL,
                                    Id = _Id
                                };
                                Id = _Id;
                                cxt.CMS_Brands.Add(e);
                            }
                            else
                            {
                                var e = cxt.CMS_Brands.Find(model.Id);
                                if (e != null)
                                {
                                    e.BrandCode = model.BrandCode;
                                    e.BrandName = model.BrandName;
                                    e.Description = model.Description;
                                    e.IsActive = model.IsActive;
                                    e.UpdatedBy = model.UpdatedBy;
                                    e.UpdatedDate = DateTime.Now;
                                    e.ImageURL = model.ImageURL;
                                }
                            }
                            cxt.SaveChanges();
                            beginTran.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = "Lỗi đường truyền mạng";
                        beginTran.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }

        public bool Delete(string Id, ref string msg)
        {
            var result = true;
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var e = cxt.CMS_Brands.Find(Id);
                    cxt.CMS_Brands.Remove(e);
                    cxt.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                msg = "Không thể xóa thương hiệu này";
                result = false;
            }
            return result;
        }

        public CMSBrandsModels GetDetail(string Id)
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Brands.Select(x => new CMSBrandsModels
                    {
                        BrandCode = x.BrandCode,
                        BrandName = x.BrandName,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        Description = x.Description,
                        Id = x.Id,
                        IsActive = x.IsActive,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                        ImageURL = x.ImageURL,
                    }).Where(x => x.Id.Equals(Id)).FirstOrDefault();

                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public List<CMSBrandsModels> GetList()
        {
            try
            {
                using (var cxt = new CMS_Context())
                {
                    var data = cxt.CMS_Brands.Select(x => new CMSBrandsModels
                    {
                        BrandCode = x.BrandCode,
                        BrandName = x.BrandName,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        Description = x.Description,
                        Id = x.Id,
                        IsActive = x.IsActive,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                        ImageURL = x.ImageURL
                    }).ToList();

                    /* count number of product */
                    var lstNumOfProduct = cxt.CMS_Products.GroupBy(o => o.BrandId).Select(o => new
                    {
                        ID = o.Key,
                        Count = o.Count(),
                    }).ToList();
                    data.ForEach(o =>
                    {
                        o.NumberOfProduct = lstNumOfProduct.Where(c => c.ID == o.Id).Select(c => c.Count).FirstOrDefault();
                    });

                    /* response data */
                    return data;
                }
            }
            catch (Exception ex) { }
            return null;
        }
    }
}
