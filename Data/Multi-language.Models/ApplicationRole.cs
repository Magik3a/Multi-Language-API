using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Multi_language.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {

        }

        public ApplicationRole(string name, int level) : base (name)
        {
            // TODO Extend roles table with something funny
            Level = level;
        }


        public virtual int Level { get; set; }
    }
}
