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

            // Вказуємо, що `actionproduct_id` є первинним ключем (PK)
            modelBuilder.Entity<ActionProductEntity>()
                .HasKey(ap => ap.ActionProductId);

            // Налаштовуємо зв'язок "Один-до-багатьох":
            // Action -> ActionProducts
            modelBuilder.Entity<ActionProductEntity>()
                .HasOne(ap => ap.Action)           // Кожен ActionProduct має одну Акцію
                .WithMany(a => a.ActionProducts)  // Кожна Акція має багато ActionProducts
                .HasForeignKey(ap => ap.ActionId); // Через ключ action_id

            // Налаштовуємо зв'язок "Один-до-багатьох":
            // Product -> ActionProducts
            modelBuilder.Entity<ActionProductEntity>()
                .HasOne(ap => ap.Product)         // Кожен ActionProduct має один Продукт
                .WithMany(p => p.ActionProducts)  // Кожен Продукт має багато ActionProducts
                .HasForeignKey(ap => ap.ProductId); // Через ключ product_id

            modelBuilder.Entity<UserRoleEntity>()
                .HasKey(ur => ur.UserRoleId);

            modelBuilder.Entity<UserRoleEntity>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRoleEntity>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);



    }
}
