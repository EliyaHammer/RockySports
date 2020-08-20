namespace RockyClasses
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<DaysOfWeek> DaysOfWeeks { get; set; }
        public virtual DbSet<IsAbsance> IsAbsances { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DaysOfWeek>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.DaysOfWeek)
                .HasForeignKey(e => e.DayOfWeek);

            modelBuilder.Entity<IsAbsance>()
                .Property(e => e.IsAbsance1)
                .IsFixedLength();

            modelBuilder.Entity<IsAbsance>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.IsAbsance1)
                .HasForeignKey(e => e.IsAbsance)
                .WillCascadeOnDelete(false);
        }
    }
}
