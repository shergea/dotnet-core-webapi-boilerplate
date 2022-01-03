using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebApi.Model;
using WebApi.Model.Interfaces;

namespace WebApi.DAL
{
    public partial class MsSQLContext : DbContext
    {
        public MsSQLContext(DbContextOptions<MsSQLContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplySoftDeletesFilter(modelBuilder);
            modelBuilder.Entity<User>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<RefreshToken>()
                .HasKey(c => c.Token);
        }


        #region SoftDelete
        protected void ApplySoftDeletesFilter(ModelBuilder modelBuilder)
        {
            foreach (var tp in modelBuilder.Model.GetEntityTypes())
            {
                var t = tp.ClrType;
                if (typeof(ISoftDelete).IsAssignableFrom(t))
                {
                    var method = SetGlobalQueryForSoftDeleteMethodInfo.MakeGenericMethod(t);
                    method.Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        private static readonly MethodInfo SetGlobalQueryForSoftDeleteMethodInfo = typeof(MsSQLContext).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForSoftDelete");

        private void SetGlobalQueryForSoftDelete<T>(ModelBuilder builder) where T : class
        {
            builder.Entity<T>().HasQueryFilter(item => EF.Property<Nullable<DateTimeOffset>>(item, "DeletedAt") == null);
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var addedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
            addedEntities.ForEach(e =>
            {
                if (e.Metadata.FindProperty("CreatedAt") != null) e.Property("CreatedAt").CurrentValue = DateTimeOffset.Now;
            });
            var editedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).ToList();
            editedEntities.ForEach(e =>
            {
                if (e.Metadata.FindProperty("UpdatedAt") != null) e.Property("UpdatedAt").CurrentValue = DateTimeOffset.Now;
            });
            var deletedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted).ToList();
            deletedEntities.ForEach(e =>
            {
                if (e.Entity is ISoftDelete)
                {
                    e.State = EntityState.Modified;
                    e.Property("DeletedAt").CurrentValue = DateTimeOffset.Now;
                }
            });
        }
        #endregion
    }
}