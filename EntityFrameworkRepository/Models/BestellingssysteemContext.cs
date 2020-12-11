using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EntityFrameworkRepository.Models
{
    public partial class BestellingssysteemContext : DbContext
    {
        #region Properties
        // Belangrijk: voor DbSet<> worden tabellen aangemaakt!
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        #endregion

        #region Ctor
        public BestellingssysteemContext()
        {
        }

        public BestellingssysteemContext(DbContextOptions<BestellingssysteemContext> options)
            : base(options)
        {
        }
        #endregion

        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // ConnectionString www.connectionstrings.com
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Bestellingssysteem;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("CUSTOMER");

                entity.HasIndex(e => e.Name, "IX_CUSTOMER_NAME");

                entity.Property(e => e.Id).HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.AutoTimeCreation)
                    .HasColumnName("AUTO_TIME_CREATION")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.AutoTimeUpdate)
                    .HasColumnName("AUTO_TIME_UPDATE")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");

                entity.Property(e => e.AutoUpdatedBy)
                    .IsRequired()
                    .HasColumnName("AUTO_UPDATED_BY")
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.AutoValid)
                    .HasColumnName("AUTO_VALID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("ORDER");

                entity.Property(e => e.Id).HasColumnName("ORDER_ID");

                entity.Property(e => e.AutoTimeCreation)
                    .HasColumnName("AUTO_TIME_CREATION")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.AutoTimeUpdate)
                    .HasColumnName("AUTO_TIME_UPDATE")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");

                entity.Property(e => e.AutoUpdatedBy)
                    .IsRequired()
                    .HasColumnName("AUTO_UPDATED_BY")
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.AutoValid)
                    .HasColumnName("AUTO_VALID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.Paid).HasColumnName("PAID");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Time).HasColumnName("TIME");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_ORDER_CUSTOMER");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.ToTable("ORDER_PRODUCT");

                entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");

                entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");

                entity.Property(e => e.Amount).HasColumnName("AMOUNT");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_ORDER_PRODUCT_ORDER");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ORDER_PRODUCT_PRODUCT");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCT");

                entity.Property(e => e.Id).HasColumnName("PRODUCT_ID");

                entity.Property(e => e.AutoTimeCreation)
                    .HasColumnName("AUTO_TIME_CREATION")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.AutoTimeUpdate)
                    .HasColumnName("AUTO_TIME_UPDATE")
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.AutoUpdateCount).HasColumnName("AUTO_UPDATE_COUNT");

                entity.Property(e => e.AutoUpdatedBy)
                    .IsRequired()
                    .HasColumnName("AUTO_UPDATED_BY")
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("NAME");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Valid)
                    .HasColumnName("VALID")
                    .HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
    #endregion
}
