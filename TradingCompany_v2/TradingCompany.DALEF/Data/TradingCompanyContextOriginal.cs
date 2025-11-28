using Microsoft.EntityFrameworkCore;
using TradingCompany.DALEF.Entity;
using TradingCompany.DALEF.Entity.User;

namespace TradingCompany.DALEF.Data
{
    public partial class TradingCompanyContextOriginal : DbContext
    {
        public TradingCompanyContextOriginal()
        {
        }
        public TradingCompanyContextOriginal(DbContextOptions<TradingCompanyContextOriginal> options)
            : base(options)
        {
        }
        public DbSet<ActionEntity> Actions { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<StatusEntity> Statuses { get; set; }
        public DbSet<ActionProductEntity> ActionProducts { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("abracadabra");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ActionEntity>(entity =>
            {
                entity.HasOne(a => a.Status)
                      .WithMany(s => s.Actions)
                      .HasConstraintName("FK_Actions_Status");
            });

            
            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasConstraintName("FK_Products_Categories");
            });

            
            modelBuilder.Entity<ActionProductEntity>(entity =>
            {
                entity.HasKey(ap => ap.ActionProductId);

                entity.HasOne(ap => ap.Action)
                      .WithMany(a => a.ActionProducts)
                      .HasForeignKey(ap => ap.ActionId)
                      .HasConstraintName("FK_ActionProducts_Actions")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ap => ap.Product)
                      .WithMany(p => p.ActionProducts)
                      .HasForeignKey(ap => ap.ProductId)
                      .HasConstraintName("FK_ActionProducts_Products")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            
            modelBuilder.Entity<UserRoleEntity>(entity =>
            {
                entity.HasKey(ur => ur.UserRoleId);

                
                entity.HasOne(ur => ur.User)
                      .WithMany(u => u.UserRoles)
                      .HasForeignKey(ur => ur.UserId)
                      .OnDelete(DeleteBehavior.Cascade); 
                
                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(ur => ur.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);



    }
}
