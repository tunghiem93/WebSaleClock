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
        List<string> listPropertyReject = null;
        public void PropertyReject()
        {
            foreach (var item in listPropertyReject)
            {
                if (ModelState.ContainsKey(item))
                    ModelState[item].Errors.Clear();
            }
        }

        public AccountController()
        {
            _facCus = new CMSCustomersFactory();
            listPropertyReject = new List<string>();
            listPropertyReject.Add("Address");
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

        [HttpPost]
        public ActionResult Index(CMS_CustomerModels model)
        {
            try
            {
                PropertyReject();
                if (!string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(model.ConfirmPassword) && !model.Password.Equals(model.ConfirmPassword))
                    ModelState.AddModelError("ConfirmPassword", "Xác nhận mật khẩu không chính xác !");

                if (!ModelState.IsValid)
                    return View(model);
                model.Password = CommonHelper.Encrypt(model.Password);
                string msg = "";
                string cusId = "";
                model.Address = "None";
                var result = _facCus.InsertOrUpdate(model, ref cusId, ref msg);
                if (result)
                {                   
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Email", msg);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("SignUp", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }
    }
}