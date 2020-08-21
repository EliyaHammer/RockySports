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
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DaysOfWeek>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.DaysOfWeek)
                .HasForeignKey(e => e.DayOfWeek);
        }
    }
}
