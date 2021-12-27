using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Threading.Tasks;
using ZeroHunger.Data;
using ZeroHunger.Model;

namespace ZeroHunger.Pages.Register
{
    public class RegisterReceiverModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public RegisterReceiverModel(ApplicationDbContext db)
        {
            _db = db;

        }
        [BindProperty]
        public Receiver application { get; set; }
        public IEnumerable<SalaryGroup> salaryGroups { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public string street { get; set; }
        public string additional { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string message { get; set; } = "";
        public static void SendEmail(string emailbody, string userEmail)
        {
            // Specify the from and to email address
            MailMessage mailMessage = new MailMessage("vtechzerohunger@gmail.com", userEmail);
            // Specify the email body
            mailMessage.Body = emailbody;
            // Specify the email Subject
            mailMessage.Subject = "We had received your application!";

            // Specify the SMTP server name and post number
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            // Specify your gmail address and password
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "vtechzerohunger@gmail.com",
                Password = "ad_0hunger"
            };
            // Gmail works on SSL, so set this property to true
            smtpClient.EnableSsl = true;
            // Finall send the email message using Send() method
            smtpClient.Send(mailMessage);
        }
        public void OnGet()
        {
            salaryGroups = _db.SalaryGroup;
        }
        public async Task<IActionResult> OnPost(Receiver Application, string fName, string lName, string street, string additional, string zip, string city, string state)
        {
            foreach(Receiver receiver in _db.Receiver)
            {
                if(Application.receiverIC==receiver.receiverIC)
                {
                    message = "This IC no. has been registered.";
                    return Page();
                }
            }
            foreach (ReceiverFamily receiverfamily in _db.ReceiverFamily)
            {
                if (Application.receiverIC == receiverfamily.receiverIC)
                {
                    message = "This IC no has been registered.";
                    return Page();
                }
            }
            foreach (Receiver receiver in _db.Receiver)
            {
                if (Application.receiverEmail == receiver.receiverEmail)
                {
                    message = "This email has been registered.";
                    return Page();
                }
            }
            foreach (User user in _db.User)
            {
                if (Application.receiverEmail == user.UserEmail)
                {
                    message = "This email has been registered.";
                    return Page();
                }
            }

            Application.receiverName = fName+" " + lName;
            if(!street.EndsWith(','))
            {
                street += ",";
            }
            if (!additional.EndsWith(','))
            {
                additional += ",";
            }
            Application.receiverAdrs1 = street+" " + additional;
            Application.receiverAdrs2 = zip + " " + city + ", " + state;
            Application.receiverSalaryGroup = _db.SalaryGroup.Where(i => i.salaryGroupID.Equals(Application.receiverSalaryGroupID)).Single();


            Application.receiverFamilyNo = 0;

            await _db.Receiver.AddAsync(Application);
            await _db.SaveChangesAsync();

           /* string emailBody = "Thank you for trusting Zero Hunger. "
                    + "We had received your application to become a receiver to us. "
                    + "The below is a summary of your information:\n"
                    + "Name: " + Application.receiverName
                    + "\nIC no: " + Application.receiverIC
                    + "\nOccupation: " + Application.receiverOccupation
                    + "\nSalary: RM" + Application.receiverSalaryGroup.salaryRange
                    + "\nPhone No.: " + Application.receiverPhone
                    + "\nEmail: " + Application.receiverEmail
                    + "\nAddress: " + Application.receiverAdrs1 + Application.receiverAdrs2
                    + "\n\nWe will contact you as soon as possible. Please contact us if there is any problem.";

            SendEmail(emailBody, Application.receiverEmail);*/

            //if(Application.receiverFamilyNo == 0)
            //{
            //    await _db.Receiver.AddAsync(application);
            //    await _db.SaveChangesAsync();
            //    return RedirectToPage("Index");
            //}
            //else
            //{
            return RedirectToPage("RegisterReceiver_Family", Application);
            //}
            
        }
    }

}

