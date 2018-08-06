using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared
{
    public static class Commons
    {
        public const string Image100_50 = "http://placehold.it/100x50";
        public const string Image200_100 = "http://placehold.it/200x100";
        public const string Image870_225 = "http://placehold.it/870x225";
        public const string Image270_270 = "http://placehold.it/270x270";
        public const string Image770_395 = "http://placehold.it/770x395";
        public const string Image1920_730 = "http://placehold.it/1920x730";

        public static int WidthProduct = Convert.ToInt16(ConfigurationManager.AppSettings["WidthImageProduct"]);
        public static int HeightProduct = Convert.ToInt16(ConfigurationManager.AppSettings["HeightImageProduct"]);
        public static int WidthCate = Convert.ToInt16(ConfigurationManager.AppSettings["WidthImageCate"]);
        public static int HeightCate = Convert.ToInt16(ConfigurationManager.AppSettings["HeightImageCate"]);
        public static int WidthBrand = Convert.ToInt16(ConfigurationManager.AppSettings["WidthImageBrand"]);
        public static int HeightBrand = Convert.ToInt16(ConfigurationManager.AppSettings["HeightImageBrand"]);
        public static int WidthImageNews = Convert.ToInt16(ConfigurationManager.AppSettings["WidthImageNews"]);
        public static int HeightImageNews = Convert.ToInt16(ConfigurationManager.AppSettings["HeightImageNews"]);
        public static int WidthImageSilder = Convert.ToInt16(ConfigurationManager.AppSettings["WidthImageSilder"]);
        public static int HeightImageSilder = Convert.ToInt16(ConfigurationManager.AppSettings["HeightImageSilder"]);

        public static string Phone1 = ConfigurationManager.AppSettings["Phone1"];
        public static string Phone2 = ConfigurationManager.AppSettings["Phone2"];
        public static string Email1 = ConfigurationManager.AppSettings["Email1"];
        public static string Email2 = ConfigurationManager.AppSettings["Email2"];
        public static string AddressCompany = ConfigurationManager.AppSettings["AddressCompnay"];
        public static string CompanyTitle = ConfigurationManager.AppSettings["CompanyTitle"];
        public static string HostImage = ConfigurationManager.AppSettings["HostImage"];
        public static string _PublicImages = string.IsNullOrEmpty(ConfigurationManager.AppSettings["PublicImages"]) ? "" : ConfigurationManager.AppSettings["PublicImages"];

        public const string PasswordChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        #region Enum
        public enum ESizeType
        {
            XS = 0,
            S = 1,
            M = 2,
            L = 3,
            XL = 4,
        }

        public enum EStateType
        {
            Avalable = 0,
            None = 1,
        }

        public enum ERangeType
        {
            Leatest = 0,
            Hightest = 1,
        }
        #endregion
    }
}
