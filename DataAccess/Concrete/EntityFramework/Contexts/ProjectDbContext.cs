using Core.Entities.Concrete;
using Core.Entities.EntityStates;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    /// <summary>
    /// Because this context is followed by migration for more than one provider
    /// works on PostGreSql db by default. If you want to pass sql
    /// When adding AddDbContext, use MsDbContext derived from it.
    /// </summary>
    public class ProjectDbContext : DbContext
    {
        /// <summary>
        /// in constructor we get IConfiguration, parallel to more than one db
        /// we can create migration.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        /// <summary>
        /// Let's also implement the general version.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        protected ProjectDbContext(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;

        }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupClaim> GroupClaims { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<MobileLogin> MobileLogins { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Translate> Translates { get; set; }

        //*Stock Inventory Management*//
        //public DbSet<Product> Products { get; set; }
        //public DbSet<StockMovement> StockMovements { get; set; }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Supplier> Suppliers { get; set; }
        //public DbSet<ProductSupplier> ProductSuppliers { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderItem> OrderItems { get; set; }
        //public DbSet<Warehouse> Warehouses { get; set; }
        //public DbSet<Purchase> Purchases { get; set; }
        //public DbSet<PurchaseItem> PurchaseItems { get; set; }
        //public DbSet<Sale> Sales { get; set; }
        //public DbSet<SaleItem> SaleItems { get; set; }
        //public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }
          public DbSet<Customer> Customers { get; set; }
          public DbSet<CustomerAccount> CustomerAccounts { get; set; }
          public DbSet<Order> Orders { get; set; }
          public DbSet<Purchase> Purchases { get; set; }
          public DbSet<Product> Products { get; set; }
          public DbSet<Sale> Sales { get; set; }
          public DbSet<StockMovement> StockMovements { get; set; }
          public DbSet<Supplier> Suppliers { get; set; }
          public DbSet<Transaction> Transactions { get; set; }
          public DbSet<Warehouse> Warehouses { get; set; }
          public DbSet<ProductCategories> ProductCategories { get; set; }
          public DbSet<ProductSupplier> ProductSuppliers { get; set; }
          public DbSet<PurchaseItem> PurchaseItems { get; set; }
          public DbSet<SaleItem> SaleItems { get; set; }
          public DbSet<OrderItem> OrderItems { get; set; }


        protected IConfiguration Configuration { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DArchPgContext"))
                    .EnableSensitiveDataLogging());
            }
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var context = this.GetService<IHttpContextAccessor>().HttpContext;
            if (context != null && context.User != null)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var stateManager = new EntityStateManager();

                foreach (var entity in ChangeTracker.Entries().Where(x => x.State == EntityState.Modified ||
                                                                          x.State == EntityState.Added || x.State == EntityState.Deleted))
                {
                    stateManager.HandleState(entity, userId);
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var context = this.GetService<IHttpContextAccessor>().HttpContext;
            if (context != null && context.User != null)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var stateManager = new EntityStateManager();

                foreach (var entity in ChangeTracker.Entries().Where(x => x.State == EntityState.Modified ||
                                                                          x.State == EntityState.Added || x.State == EntityState.Deleted))
                {
                    stateManager.HandleState(entity, userId);
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            var context = this.GetService<IHttpContextAccessor>().HttpContext;
            if (context != null && context.User != null)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var stateManager = new EntityStateManager();

                foreach (var entity in ChangeTracker.Entries().Where(x => x.State == EntityState.Modified ||
                                                                          x.State == EntityState.Added || x.State == EntityState.Deleted))
                {
                    stateManager.HandleState(entity, userId);
                }
            }

            return base.SaveChanges();
        }
    }
}
