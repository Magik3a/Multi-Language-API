
namespace Multi_language.Models
{
    using Common.Consants;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class AppUser : IdentityUser
    {
        [StringLength(ValidationConstants.UserNames)]
        public string FirstName { get; set; }

        [StringLength(ValidationConstants.UserNames)]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }
        public DateTime? DateCreated { get; internal set; }
        public DateTime? DateModified { get; internal set; }
  
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
