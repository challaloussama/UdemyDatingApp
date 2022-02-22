using Microsoft.EntityFrameworkCore;
using UdemyLearning.Entities;

namespace UdemyLearning.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) :base(options)
        {

        }
        public DbSet<AppUser> Users { get; set; }
    }

 
} 
