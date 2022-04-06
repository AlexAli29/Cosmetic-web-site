using cosmetic_web.Models;
using Microsoft.EntityFrameworkCore;

namespace cosmetic_web.Data;

    public class ApplicationDbContext:DbContext
    {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>  options  ) : base( options )    
    {
         
    }

        public DbSet<Category> Categories { get; set; }
        
        public DbSet<BasketItem> BasketItems { get; set; }

    }

