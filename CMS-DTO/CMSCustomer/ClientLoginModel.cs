using CMS_DTO.CMSContact;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSCustomer
{
    public class ClientLoginModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }
        public bool IsRemember { get; set; }
        public string DisplayName { get; set; }
        public bool IsAdmin { get; set; }
        public CMS_ContactModels ContactDTO { get; set; }

        public ClientLoginModel()
        {
            IsRemember = true;
            ContactDTO = new CMS_ContactModels();
        }
    }
}
