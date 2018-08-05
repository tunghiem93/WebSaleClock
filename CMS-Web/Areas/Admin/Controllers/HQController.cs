using CMS_DTO.CMSBase;
using CMS_Shared;
using CMS_Shared.CMSBrands;
using CMS_Shared.CMSCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Areas.Admin.Controllers
{
    public class HQController : Controller
    {
        public List<SelectListItem> GetListCategorySelectItem()
        {
            var _factory = new CMSCategoriesFactory();
            var data = _factory.GetList().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.CategoryName,
            }).ToList();
            return data;
        }

        public List<SelectListItem> GetListBrandSelectItem()
        {
            var _factory = new CMSBrandsFactory();
            var data = _factory.GetList().Select(x => new SelectListItem
            {
                Value = x.Id,
                Text = x.BrandName,
            }).ToList();
            return data;
        }

        public List<CategoryByCategory> GetListCategory()
        {
            var _factory = new CMSCategoriesFactory();
            //var data = _factory.GetList().Where(x => !string.IsNullOrEmpty(x.ParentId)).Select(x => new SelectListItem
            //{
            //    Value = x.Id,
            //    Text = x.CategoryName,
            //}).ToList();
            //return data;
            var models = new List<CategoryByCategory>();
            var data = _factory.GetList();
            if (data != null)
            {
                var groupCate = data.Where(x => string.IsNullOrEmpty(x.ParentId)).ToList();
                if (groupCate != null)
                {
                    groupCate.ForEach(x =>
                    {
                        var model = new CategoryByCategory();
                        model.id = x.Id;
                        model.text = x.CategoryName.ToUpper();
                        model.children = data.Where(y => !string.IsNullOrEmpty(y.ParentId) && y.ParentId.Equals(x.Id))
                                                .Select(z => new CategoryChildren
                                                {
                                                    id = z.Id,
                                                    text = z.CategoryName
                                                }).ToList();
                        models.Add(model);
                    });
                }
            }

            return models;
        }

        public List<SelectListItem> getListSize()
        {
            var _lstSize = new List<SelectListItem>() {
                new SelectListItem() { Text = "XS", Value = Commons.ESizeType.XS.ToString("d") },
                new SelectListItem() { Text = "S", Value = Commons.ESizeType.S.ToString("d") },
                new SelectListItem() { Text = "M", Value = Commons.ESizeType.M.ToString("d") },
                new SelectListItem() { Text = "L", Value = Commons.ESizeType.L.ToString("d") },
                new SelectListItem() { Text = "XL", Value = Commons.ESizeType.XL.ToString("d") },
            };
            return _lstSize;
        }

        public List<SelectListItem> getListState()
        {
            var _lstSize = new List<SelectListItem>() {
                new SelectListItem() { Text = "Sẵn sàng", Value = Commons.EStateType.Avalable.ToString("d") },
                new SelectListItem() { Text = "Hết hàng", Value = Commons.EStateType.None.ToString("d") },
            };
            return _lstSize;
        }
    }
}