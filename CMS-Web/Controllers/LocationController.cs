using CMS_DTO.CMSCompany;
using CMS_Shared.CMSCompanies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Web.Controllers
{
    public class LocationController : Controller
    {
        private CMSCompaniesFactory _facCom;
        public LocationController()
        {
            _facCom = new CMSCompaniesFactory();
        }

        // GET: Location
        public ActionResult Index()
        {
            CMS_CompanyModels model = new CMS_CompanyModels();
            try
            {
                model = _facCom.GetInfor();
                return View(model);
            }
            catch (Exception ex)
            {                
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }
    }
}