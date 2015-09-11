using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityBase.Models.IdentityModels
{
    public class MyIdentityRole<TKey, TUserRole> : IRole<TKey> where TUserRole : Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<TKey>
    {
        // Summary:
        //     Constructor
        public MyIdentityRole();

        // Summary:
        //     Role id
        public TKey Id { get; set; }
        //
        // Summary:
        //     Role name
        public string Name { get; set; }
        //
        // Summary:
        //     Navigation property for users in the role
        public virtual ICollection<TUserRole> Users { get; }
    }
}