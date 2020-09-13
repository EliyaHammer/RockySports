namespace RockyClasses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime Date { get; set; }

        public TimeSpan? ChecksInOne { get; set; }

        public TimeSpan? ChecksOutOne { get; set; }

        public int? MinutesLate { get; set; }

        public int? MinutesEarlyLeave { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IsAbsance { get; set; }

        public TimeSpan? ChecksInTwo { get; set; }

        public TimeSpan? ChecksOutTwo { get; set; }

        public int? DayOfWeek { get; set; }

        public virtual DaysOfWeek DaysOfWeek { get; set; }

        public int? IsError { get; set; }

        public int? IsManuallyChanged { get; set; }

        public void CalculateIsError ()
        {
            TimeSpan zero = new TimeSpan(0, 0, 0);
            IsError = 0;

            if ((IsAbsance == 0) && (ChecksInOne != zero || ChecksInTwo != zero || ChecksOutOne != zero || ChecksOutTwo != zero))
            {
                if (ChecksInOne != zero)
                {
                    if ((ChecksOutOne == zero && ChecksInTwo != zero) || (ChecksOutOne == zero && ChecksOutTwo == zero))
                        IsError = 1;
                }

                if (ChecksInTwo != zero)
                {
                    if (ChecksOutTwo == zero)
                        IsError = 1;
                }

                if (ChecksInOne == zero && ChecksOutOne != zero)
                    IsError = 1;

                if (ChecksInOne == zero && ChecksInTwo == zero && ChecksOutTwo != zero)
                    IsError = 1;
            }
        }

        public void CalculateIsAbsence ()
        {
            TimeSpan zero = new TimeSpan(0, 0, 0);
            if (ChecksInOne != zero || ChecksInTwo != zero || ChecksOutOne != zero || ChecksOutTwo != zero)
                IsAbsance = 0;
            else
                IsAbsance = 1;
        }

    }
}
