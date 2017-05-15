using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using VhakoneWeb.Models;
using System.Net.Mail;
using System.Net;

namespace VhakoneWeb
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return configSendGridasync(message);
        }
        private Task configSendGridasync(IdentityMessage message)
        {
            var smtp = new System.Net.Mail.SmtpClient();
            var mail = new System.Net.Mail.MailMessage();

            mail.IsBodyHtml = true;
            mail.From = new System.Net.Mail.MailAddress("registration@vhakone.co.za", "registration Mail");
            mail.To.Add(message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;

            smtp.Timeout = 1000;
            smtp.Credentials = new NetworkCredential("registration@vhakone.co.za", "P@$$w0rd");
            smtp.Port = 25;
            smtp.Host = "curry.aserv.co.za";
            smtp.EnableSsl = true;
            var t = Task.Run(() => smtp.SendAsync(mail, null));

            return t;
            //    var from = new MailAddress("career@vhakone.co.za");
            //    var to = new MailAddress(message.Destination);

            //    var useDefaultCredentials = false;
            //    var enableSsl = false;
            //    var replyto = "registration@vhakone.co.za"; // set here your email; 
            //    var userName = string.Empty;
            //    var password = string.Empty;
            //    var port = 25;
            //    var host = "curry.aserv.co.za";

            //    userName = "registration@vhakone.co.za"; // setup here the username; 
            //    password = "P@$$w0rd"; // setup here the password; 
            //   /* bool.TryParse("true", out useDefaultCredentials); //setup here if it uses defaault credentials 
            //    bool.TryParse("true", out enableSsl); //setup here if it uses ssl 
            //    int.TryParse("465", out port); //setup here the port 
            //    host = "curry.aserv.co.za"; //setup here the host */

            //    using (var mail = new MailMessage(from, to))
            //    {
            //        mail.Subject = message.Subject;
            //        mail.Body = message.Body;
            //        mail.IsBodyHtml = true;

            //        mail.ReplyToList.Add(new MailAddress(replyto, "Registration"));
            //        mail.ReplyToList.Add(from);
            //        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.Delay |
            //                                           DeliveryNotificationOptions.OnFailure |
            //                                           DeliveryNotificationOptions.OnSuccess;

            //        using (var client = new SmtpClient())
            //        {
            //            client.Host = host;
            //            client.EnableSsl = enableSsl;
            //            client.Port = port;
            //            client.UseDefaultCredentials = useDefaultCredentials;

            //            if (!client.UseDefaultCredentials && !string.IsNullOrEmpty(userName) &&
            //                !string.IsNullOrEmpty(password))
            //            {
            //                client.Credentials = new NetworkCredential(userName, password);
            //            }


            //            if (client.Credentials != null)
            //            {
            //                client.Timeout = 1000;

            //                var t = Task.Run(() => client.SendAsync(mail, null));

            //                return t;

            //                //return client.SendMailAsync(mail);
            //            }
            //            else
            //            {
            //                return Task.FromResult(0);
            //            }
            //        }
            //    }
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
