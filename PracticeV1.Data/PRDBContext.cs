using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Data
{
    public class PRDBContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public PRDBContext(DbContextOptions<PRDBContext> options) : base(options) 
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
