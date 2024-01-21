using Examm.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Examm.Contexts
{
    public class ExammDbContext:IdentityDbContext
    {
        public ExammDbContext (DbContextOptions options) : base(options){ }
        public DbSet<Slider> Sliders { get; set; }
    }
}
