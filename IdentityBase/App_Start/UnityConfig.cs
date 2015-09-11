using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Services.Services;

namespace IdentityBase
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            //container.RegisterType<RoleManager<CustomRole, int>, ApplicationRoleManager>();
            //container.RegisterType<ApplicationRoleManager>(new InjectionConstructor());
            //container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<IApplicationUserManager, ApplicationUserManager>();
            container.RegisterType<IApplicationUserManager, ApplicationRoleManager>();
            
            //container.RegisterType<CustomUserStore>(new InjectionConstructor());
            //container.RegisterType<IUserStore<ApplicationUser, int>>(new InjectionConstructor());
            //container.RegisterType<UserManager<ApplicationUser, int>, ApplicationUserManager>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}