using CMS_DTO.CMSNews;
using CMS_Shared;
using CMS_Shared.CMSCategories;
using CMS_Shared.CMSNews;
using CMS_Shared.CMSProducts;
using CMS_Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class NewsController : Controller
    {
        private CMSNewsFactory _fac;
        private CMSCategoriesFactory _facCate;
        private CMSProductFactory _facPro;
        public NewsController()
        {
            _fac = new CMSNewsFactory();
            _facCate = new CMSCategoriesFactory();
            _facPro = new CMSProductFactory();
        }
        // GET: Clients/News
        public ActionResult Index()
        {
            var model = new CMS_NewsViewModel();
            var data = _fac.GetList().OrderByDescending(x=>x.CreatedDate).ToList();
            if(data != null)
            {
                data.ForEach(x =>
                {
                    x.Alias = CommonHelper.RemoveUnicode(x.Title.Trim().Replace(" ", "-")).ToLower();
                    x.ImageURL = Commons.HostImage + "News/" + x.ImageURL;
                });

                model.ListNews = data;
                model.ListNewsNew = data.OrderBy(x => x.CreatedDate).ToList();
                model.ListNewsOld = data.OrderByDescending(x => x.CreatedDate).Skip(0).Take(5).ToList();
            }
            return View(model);
        }
        
        public ActionResult Detail(string q)
        {
            var model = new CMS_NewsViewModel();
            try
            {
                if(string.IsNullOrEmpty(q))
                {
                    return RedirectToAction("Index", "NotFound");
                }
                else
                {
                    q = q.Trim().Replace("-", " ");
                    var data = _fac.GetList().Where(o => CommonHelper.RemoveUnicode(o.Title.Trim().ToLower()).Equals(q)).FirstOrDefault();
                    if (data != null)
                    {
                        if(!string.IsNullOrEmpty(data.ImageURL))
                        {
                            data.ImageURL = Commons.HostImage + "News/" + data.ImageURL;
                        }
                    }

                    model.CMS_News = data;
                    model.ListNewsOld = _fac.GetList().OrderByDescending(x => x.CreatedDate).Skip(0).Take(5).ToList();   
                    if(model.ListNewsOld != null && model.ListNewsOld.Any())
                    {
                        model.ListNewsOld.ForEach(x =>
                        {
                            x.Alias = CommonHelper.RemoveUnicode(x.Title.Trim().Replace(" ", "-")).ToLower();
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index", "NotFound");
            }
            return View(model);
        }
    }
}