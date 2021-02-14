namespace OfficeEquipmentManager.DatabaseData
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelContext : DbContext
    {
        public ModelContext()
            : base("name=ModelContext2")
        {
        }

        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Barcode> Barcode { get; set; }
        public virtual DbSet<Booker> Booker { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentCategory> EquipmentCategory { get; set; }
        public virtual DbSet<EquipmentStatus> EquipmentStatus { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<Booker>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<EquipmentCategory>()
                .HasMany(e => e.Equipment)
                .WithRequired(e => e.EquipmentCategory)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EquipmentStatus>()
                .HasMany(e => e.Equipment)
                .WithRequired(e => e.EquipmentStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Administrator)
                .WithRequired(e => e.User);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Booker)
                .WithRequired(e => e.User);
        }
    }
}
