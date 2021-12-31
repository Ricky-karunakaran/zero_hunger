using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;


namespace ZeroHunger.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        public string emailBody { get; set; } = "";
        public string userEmail { get; set; } = "";
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public User forgotUser { get; set; }
        public ForgotPasswordModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public string resetPasswordLink { get; set; } = "";
        public void OnGet()
        {
           
            
        }
        //public static void SendEmail(string emailbody, string userEmail)
        //{
            
        //    // Specify the from and to email address
        //    MailMessage mailMessage = new MailMessage("vtechzerohunger@gmail.com", userEmail);
        //    // Specify the email body
        //    mailMessage.Body = emailbody;
        //    // Specify the email Subject
        //    mailMessage.Subject = "Reset Password Zero Hunger";

        //    // Specify the SMTP server name and post number
        //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        //    // Specify your gmail address and password
        //    smtpClient.Credentials = new System.Net.NetworkCredential()
        //    {
        //        UserName = "vtechzerohunger@gmail.com",
        //        Password = "ad_0hunger"
        //    };
        //    // Gmail works on SSL, so set this property to true
        //    smtpClient.EnableSsl = true;
        //    // Finall send the email message using Send() method
        //    smtpClient.Send(mailMessage);
        //}

        public void anotherSendEmail(string emailbody, string userEmail)
        {

            var client = new SocketLabsClient(42290, "e5N6HwBb2k8A3Mrc9R7K"); //Your SocketLabs ServerId and Injection API key

            var message = new BasicMessage();

            message.Subject = "Zero Hunger Reset Password";
            message.HtmlBody = emailbody;
            message.PlainTextBody = emailbody;

            message.From.Email = "vtechzerohunger@gmail.com";

            //A basic message supports up to 50 recipients and supports several different ways to add recipients
            message.To.Add(userEmail); //Add a To address by passing the email address

            var response = client.Send(message);
        }
        public IActionResult OnPost(string userEmail)
        {
            
                // Find the user by email
                forgotUser = (User)(_db.User.Where(b => b.UserEmail.Equals(userEmail)).FirstOrDefault());
                // If the user is found AND Email is confirmed
                if (forgotUser != null)
                {

                    //var token = await Microsoft.AspNetCore.Identity.UserManager<User>.GeneratePasswordResetTokenAsync(forgotUser);
                    // Build the password reset link
                    //string passwordResetLink = Url.Action("ResetPassword", "Account",
                    //new { email = model.userEmail, token = rnd }, Request.Scheme);
                    resetPasswordLink = "https://localhost:44306/ResetPassword?UserID=" + forgotUser.UserID;

                    anotherSendEmail(("Please reset your password \n" + resetPasswordLink), userEmail);
                    ViewData["Message"] = string.Format("We have sent an email to you.\\Please check your email.");



                    return RedirectToPage("/login");

                }
                
                ViewData["Message"] = string.Format("This email has not been registered yet.\\nPlease register instead.");
                return Page();
            

        }
    }
}
