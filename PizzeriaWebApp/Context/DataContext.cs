using Microsoft.EntityFrameworkCore;
using PizzeriaWebApp.Models.Entities;

namespace PizzeriaWebApp.Context
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Ingredients)
                .WithMany(i => i.Products)
                .UsingEntity(j => j.ToTable("IngredientProduct"));

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j => j.ToTable("RoleUser"));

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithOne(op => op.Order)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithOne(op => op.Product)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
