using CMS_DTO.CMSSession;
using CMS_Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class HQController : Controller
    {
        public HQController()
        {
            var _Path = HostingEnvironment.MapPath("~/Uploads/Silder/");
            var list = Directory.GetFiles(_Path).Select(x => Path.GetFileName(x)).ToList();
            var ListSlider = new List<SliderSession>();
            if (list != null && list.Count > 0)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    ListSlider.Add(new SliderSession
                    {
                        ImageUrl = "~/Uploads/Silder/" +  list[i]
                    });
                }
            }
            System.Web.HttpContext.Current.Session["SliderSession"] = ListSlider;
        }

        public List<SelectListItem> GetListRangeSelectItem()
        {
            var _lstRange = new List<SelectListItem>() {
                new SelectListItem() { Text = "Giá thấp nhất", Value = Commons.ERangeType.Leatest.ToString("d") },
                new SelectListItem() { Text = "Giá cao nhất", Value = Commons.ERangeType.Hightest.ToString("d") },
            };
            return _lstRange;
        }

        public List<OrderCookie> GetListOrderCookie()
        {
            if (Request.Cookies["cms-order"] != null)
            {
                var _Orders = Request.Cookies["cms-order"].Value;
                var strOrder = Server.UrlDecode(_Orders);
                var ListOrder = JsonConvert.DeserializeObject<List<OrderCookie>>(strOrder);
                return ListOrder;
            }
            return null;
        }
    }
}