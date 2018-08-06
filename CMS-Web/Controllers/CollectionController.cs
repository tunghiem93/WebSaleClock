using CMS_DTO.CMSProduct;
using CMS_Shared;
using CMS_Shared.CMSCategories;
using CMS_Shared.CMSProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class CollectionController : Controller
    {
        private CMSCategoriesFactory _facCate;
        public CollectionController()
        {
            _facCate = new CMSCategoriesFactory();
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
    }
}