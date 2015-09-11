using Entities.EntityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Persistance.Identity.Stores;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ApplicationUserManager : UserManager<ApplicationUser, int>, IApplicationUserManager
    {
        //public ApplicationUserManager(IUserStore<ApplicationUser, int> store)
        //    : base(store)
        //{
        //}

        public ApplicationUserManager() : base(new CustomUserStore())
        {
            this.UserValidator = new MyUserValidator(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = false,
            };
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            this.RegisterTwoFactorProvider("PhoneCode", new MyPhoneNumberTokenProvider
            {
                MessageFormat = "Your security code is: {0}"
            });
            this.RegisterTwoFactorProvider("EmailCode", new MyEmailTokenProvider
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is: {0}"
            });
            this.EmailService = new Persistance.Identity.Services.EmailService();
            this.SmsService = new Persistance.Identity.Services.SmsService();

            IdentityFactoryOptions<ApplicationUserManager> aa = new IdentityFactoryOptions<ApplicationUserManager>();
            aa.DataProtectionProvider = new DpapiDataProtectionProvider("ASP.NET Identity");
            var dataProtectionProvider = aa.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                this.UserTokenProvider = new MyDataProtectorTokenProvider(dataProtectionProvider.Create("ASP.NET Identity"));
            }
        }

        //public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        //{
        //    //var manager = new ApplicationUserManager(new CustomUserStore(context.Get<ApplicationDbContext>()));
        //    var manager = new ApplicationUserManager(new CustomUserStore());
        //    // Configure validation logic for usernames
        //    manager.UserValidator = new MyUserValidator(manager)
        //    {
        //        AllowOnlyAlphanumericUserNames = false,
        //        RequireUniqueEmail = true
        //    };
        //    // Configure validation logic for passwords
        //    manager.PasswordValidator = new PasswordValidator
        //    {
        //        RequiredLength = 6,
        //        RequireNonLetterOrDigit = true,
        //        RequireDigit = true,
        //        RequireLowercase = true,
        //        RequireUppercase = true,
        //    };
        //    // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
        //    // You can write your own provider and plug in here.
        //    manager.RegisterTwoFactorProvider("PhoneCode", new MyPhoneNumberTokenProvider
        //    {
        //        MessageFormat = "Your security code is: {0}"
        //    });
        //    manager.RegisterTwoFactorProvider("EmailCode", new MyEmailTokenProvider
        //    {
        //        Subject = "Security Code",
        //        BodyFormat = "Your security code is: {0}"
        //    });
        //    manager.EmailService = new Persistance.Identity.Services.EmailService();
        //    manager.SmsService = new Persistance.Identity.Services.SmsService();
        //    var dataProtectionProvider = options.DataProtectionProvider;
        //    if (dataProtectionProvider != null)
        //    {
        //        manager.UserTokenProvider = new MyDataProtectorTokenProvider(dataProtectionProvider.Create("ASP.NET Identity"));
        //    }
        //    return manager;
        //}

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager, ApplicationUser user)
        {
            return Task.FromResult(GenerateUserIdentity(manager, user));
        }

        public ClaimsIdentity GenerateUserIdentity(ApplicationUserManager manager, ApplicationUser user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity<ApplicationUser, int>(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }


    #region Validators
    public class MyUserValidator : UserValidator<ApplicationUser, int>
    {
        public MyUserValidator(UserManager<ApplicationUser, int> manager) : base(manager)
        {
        }
    }

    public class MyRoleValidator : RoleValidator<CustomRole, int>
    {
        public MyRoleValidator(RoleManager<CustomRole, int> manager)
            : base(manager)
        {
        }
    }
    #endregion

    #region Providers
    public class MyPhoneNumberTokenProvider : PhoneNumberTokenProvider<ApplicationUser, int>
    {

    }

    public class MyEmailTokenProvider : EmailTokenProvider<ApplicationUser, int>
    {

    }

    public class MyDataProtectorTokenProvider : DataProtectorTokenProvider<ApplicationUser, int>
    {
        public MyDataProtectorTokenProvider(IDataProtector protector)
            : base(protector)
        {
        }
    }
    #endregion

}
