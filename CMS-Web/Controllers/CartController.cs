using CMS_DTO.CMSOrder;
using CMS_DTO.CMSSession;
using CMS_Shared.CMSProducts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class CartController : HQController
    {
        private CMSProductFactory _fac;
        public CartController()
        {
            _fac = new CMSProductFactory();
        }
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckOut()
        {
            CMS_CheckOutModels model = new CMS_CheckOutModels();
            try
            {
                var _Orders = GetListOrderCookie();
                NSLog.Logger.Info("List Order Cookie", JsonConvert.SerializeObject(_Orders));
                if (_Orders != null && _Orders.Any())
                {
                    var ItemIds = _Orders.Select(x => x.ItemId).ToList();
                    var data = _fac.GetList().Where(o => ItemIds.Contains(o.Id))
                                             .Select(o => new CMS_ItemModels
                                             {
                                                 Price = o.ProductPrice,
                                                 ProductID = o.Id,
                                                 ProductName = o.ProductName,
                                             }).ToList();
                    if (data != null && data.Any())
                    {
                        data.ForEach(o =>
                        {
                            var item = _Orders.FirstOrDefault(z => z.ItemId.Equals(o.ProductID));
                            o.Quantity = item.Quantity;
                            o.ImageUrl = item.ImageUrl;
                            o.TotalPrice = Convert.ToDouble(o.Price * item.Quantity);
                        });
                        model.ListItem = data;
                        model.TotalPrice = data.Sum(o => o.TotalPrice);
                        model.SubTotalPrice = data.Sum(o => o.TotalPrice);
                    }
                }

                /* get information customer from session */
                if (Session["UserClient"] != null)
                {
                    var CusInfo = Session["UserClient"] as UserSession;
                    model.Customer.FirstName = CusInfo.FirstName;
                    model.Customer.LastName = CusInfo.LastName;
                    model.Customer.Phone = CusInfo.Phone;
                    model.Customer.Email = CusInfo.Email;
                    model.Customer.Address = CusInfo.Address;
                    model.Customer.Id = CusInfo.UserId;
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("CheckOut", ex);
            }
            return View(model);
        }
    }
}