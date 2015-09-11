using Entities.EntityModels;
using Microsoft.AspNet.Identity;
using Persistance.Identity.Stores;

namespace Services.Services
{
    public class ApplicationRoleManager : RoleManager<CustomRole, int>, IApplicationUserManager
    {
        //public ApplicationRoleManager(IRoleStore<CustomRole, int> store) : base(store)
        //{
        //}

        public ApplicationRoleManager() : base(new CustomRoleStore())
        {
            this.RoleValidator = new MyRoleValidator(this);
        }

        //public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        //{
        //    //var manager = new ApplicationRoleManager(new CustomRoleStore(context.Get<ApplicationDbContext>()));
        //    var manager = new ApplicationRoleManager(new CustomRoleStore());
        //    // Configure validation logic for usernames
        //    manager.RoleValidator = new MyRoleValidator(manager) { };
        //    return manager;
        //}

    }
}
