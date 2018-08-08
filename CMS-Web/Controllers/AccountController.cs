using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSSession;
using CMS_Shared.CMSCustomers;
using CMS_Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class AccountController : HQController
    {
        private CMSCustomersFactory _facCus;
        public AccountController()
        {
            _facCus = new CMSCustomersFactory();
        }

        // GET: Account
        public ActionResult Index()
        {
            CMS_CustomerModels model = new CMS_CustomerModels();
            try
            {
                if (Session["UserClient"] != null)
                {
                    var CusInfo = Session["UserClient"] as UserSession;
                    model.ID = CusInfo.UserId;
                    model = _facCus.GetDetail(model.ID);
                    model.Password = CommonHelper.Decrypt(model.Password);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(400, ex.Message);
            }            
        }
    }
}