using CMS_DTO.CMSOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Shared.Utilities
{
    public class MailHelper
    {
        public static bool SendMail(string subject, string body, string emailTo)
        {
            var result = true;
            try
            {
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;
                string emailFrom = Commons.ClockMail;
                string password = Commons.ClockPass;
                string _emailToAdmin = Commons.Email2;
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(_emailToAdmin);
                    if (!string.IsNullOrEmpty(emailTo))
                        mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("SendMail : ", ex);
                result = false;
            }
            return result;
        }

        public static string CreateBodyMail(CMS_CheckOutModels model)
        {
            string body = string.Empty;
            try
            {
                body += "<div class='payment-order clearfix'>";
                body += "<h3>Mã đơn hàng của bạn: <b>#" + model.OrderNo + "</b></h3>";
                body += "<p><b>Ngày đặt:</b> <i>" + model.OrderDate.ToString("dd/MM/yyyy") + "</i></p>";
                body += "<p><b>Tên khách hàng:</b> <i>" + model.Customer.Name + "</i></p>";
                body += "<p><b>Số điện thoại:</b> <i>" + model.Customer.Phone + "</i></p>";
                body += "<p><b>Email:</b> <i>" + model.Customer.Email + "</i></p>";
                body += "<p><b>Địa chỉ:</b> <i>" + model.Customer.Address + "</i></p>";
                body += "<p><b>Phương thức thanh toán:</b> <i></i></p>";
                body += "<h1 class='page-heading' style= 'font-size: 16px;color: #958457;margin-bottom: 5px;'>Thông tin đơn hàng</h1>";
                body += "<table class='table' style='width: 100%;margin-bottom: 20px;max-width: 100%;border-collapse: collapse;border-spacing: 0;'>";
                body += "<thead style='background-color: #eaeaea;'>";
                body += "<tr>";
                body += "<th style='padding: 15px;vertical-align: bottom;border-bottom: 2px solid #e7ecf1;line-height: 1.42857;'>STT</th>";
                body += "<th style='padding: 15px;vertical-align: bottom;border-bottom: 2px solid #e7ecf1;line-height: 1.42857;'>Sản phâm</th>";
                body += "<th style='padding: 15px;vertical-align: bottom;border-bottom: 2px solid #e7ecf1;line-height: 1.42857;'>Đơn giá</th>";
                body += "<th style='padding: 15px;vertical-align: bottom;border-bottom: 2px solid #e7ecf1;line-height: 1.42857;'>Số lượng</th>";
                body += "<th style='padding: 15px;vertical-align: bottom;border-bottom: 2px solid #e7ecf1;line-height: 1.42857;'>Thành tiền</th>";
                body += "</tr>";
                body += "</thead>";
                body += " <tbody>";
                if (model.ListItem != null && model.ListItem.Any())
                {
                    var Index = 1;
                    foreach (var item in model.ListItem)
                    {
                        body += "<tr>";
                        body += " <td style='padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align:center;'>" + Index + "</td>";
                        body += "<td style='padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align:center;'>";
                        body += "<span>" + item.ProductName + "</span>";
                        body += " <p class='note'></p>";
                        body += "</td>";
                        body += "<td style='padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align:center;'>" + item.Price.ToString("#,0") + " đ</td>";
                        body += "<td style='padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align:center;'>" + item.Quantity + "</td>";
                        body += "<td style='padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align:center;'>" + item.TotalPrice.ToString("#,0") + " đ</td>";
                        body += "</tr>";
                        Index = Index + 1;
                    }
                }
                body += "</tbody>";
                body += "<tfoot>";
                body += "<tr>";
                body += "<td colspan='4' class='text-right label-payment' style='text-transform: uppercase;padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align: right;'><b>Tổng thanh toán chưa VAT:</b></td>";
                body += "<td class='total-payment' style='color: #ff0000;border-top: 1px solid #e7ecf1;'>" + model.SubTotalPrice.ToString("#,0") + " đ</td>";
                body += "</tr>";
                body += "<tr>";
                body += "<td colspan='4' class='text-right label-payment' style='text-transform: uppercase;padding: 8px;line-height: 1.42857;vertical-align: top;border-top: 1px solid #e7ecf1;text-align: right;'><b>Tổng thanh toán:</b></td>";
                body += "<td class='total-payment' style='color: #ff0000;border-top: 1px solid #e7ecf1;'>" + model.TotalPrice.ToString("#,0") + " đ</td>";
                body += "</tr>";
                body += "</tfoot>";
                body += "</table>";
                body += "<br clear=\"all\">";
                body += "<div><div><div><br>-- <br><div class=\"gmail_signature\" data-smartmail=\"gmail_signature\"><div dir=\"ltr\"><div class=\"gmail_signature\" data-smartmail=\"gmail_signature\"><div dir=\"ltr\"><div class=\"gmail_signature\" data-smartmail=\"gmail_signature\"><div dir=\"ltr\"><div class=\"gmail_signature\" data-smartmail=\"gmail_signature\"><div dir=\"ltr\"><div style=\"font-size: 1.2em;\"><span style=\"color: rgb(255, 153, 0);\"><b>Best regard, <br></b></span></div><div style=\"color:#000001;\"><span style=\"font-weight: 700;\"></span>  <b><span>"+Commons.CompanyTitle + "</span></b>  <span></span></div><span><span style=\"color:#55a931;\">Phone:&nbsp;</span><span><a style=\"color:#000001;\" href=\"tel:"+Commons.Phone1+"\">"+Commons.Phone1+"</a><br></span></span></div><div dir=\"ltr\"><span><span></span></span><span><span style=\"color:#55a931;\">Mobile:&nbsp;</span><span><a style=\"color:#000001;\" href=\"tel:"+Commons.Phone2+"\">"+Commons.Phone2+"</a></span></span> <br></div><div dir=\"ltr\"> <span><span style=\"color:#55a931;\">Website:&nbsp;</span><span><a href=\""+Commons.Website+"\" style=\"color:#000001;\" target=\"_blank\">"+Commons.Website+"</a><br></span></span></div><div dir=\"ltr\"><span><span></span></span>   <span><span style=\"color:#55a931;\">Email:&nbsp;</span><span><a href=\"mailto:"+Commons.Email1+"\" target=\"_blank\" style=\"color: #000001;\">"+Commons.Email1+"</a></span></span><br></div><div dir=\"ltr\">   <span><span style=\"color:#55a931;\">Address:&nbsp;</span><span style=\"color:#000001;\">"+Commons.AddressCompany+"</span></span>   <br>    <br></div></div></div></div></div></div></div></div></ div ></ div ></ div > ";
                body += "</div>";
               
            }
            catch (Exception ex) { }

            return body;
        }
    }
}
