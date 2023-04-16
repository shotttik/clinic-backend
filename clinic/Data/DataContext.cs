
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Transactions;
using clinic.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CodeVerification> CodeVerifications { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
            .HasOne(d => d.Category)
            .WithMany(c => c.Doctors)
            .HasForeignKey(d => d.CategoryId);
            
           
        }
    }

}
