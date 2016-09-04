
namespace Multi_language.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity;

    public class MultiLanguageDbContext : IdentityDbContext<AppUser>, IDbContext
    {
        public MultiLanguageDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public virtual IDbSet<Languages> languages { get; set; }

        public static MultiLanguageDbContext Create()
        {
            return new MultiLanguageDbContext();
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");

            modelBuilder.Entity<IdentityRole>()
                .ToTable("Roles");

            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRoles");

            modelBuilder.Entity<IdentityUserClaim>()
               .ToTable("UserClaims");

            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("UserLogins");
        }
    }
}
