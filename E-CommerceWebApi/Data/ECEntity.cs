using E_CommerceWebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace E_CommerceWebApi.Data
{
    public class ECEntity : IdentityDbContext<ApplicationUser>
    {
        public ECEntity(DbContextOptions<ECEntity> options) : base(options)
        {

        }
        public ECEntity()
        {

        }
        public DbSet<Product> Product { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
    }
}
