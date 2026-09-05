using System.ComponentModel.DataAnnotations.Schema;

namespace EFMigration.Entities
{
    // Course and section has one to many relationship
    // where course may has many sections (Optional) 
    // where section must has a course (Required)
    // ---------------------------------------------
    // Instructor and section has one to many relationship
    // where instructor may has many sections (Optional) 
    // where section may has an instructor (Optional)
    // ---------------------------------------------
    // Summary: in one to many relationship we can add the foreign key in the many side (one side)
    // because the many side has the many sections
    // and in the other side we can just add the navigation property
    public class Section
    {
        public int Id { get; set; }
        public string SectionName { get; set; } = null!;

        // where section "must" has a course (Required)
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        // where section "may" has an instructor (Optional)
        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();

        // we disided not to add SectionSchedule table 
        // the relation between section and schedule is one to many
        // it is not many to many because one section can have only one schedule 
        // and one schedule can have many sections
        // so we can add ScheduleId to Section table
        //public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        //public ICollection<SectionSchedule> SectionSchedules { get; set; } = new List<SectionSchedule>();
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public TimeSlot TimeSlot { get; set; } = null!;
        public override string ToString()
        {
            return $"Section Name: {SectionName} | Id: {Id} ";
        }
    }
    public class TimeSlot
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public override string ToString()
        {
            return $"{StartTime.ToString("hh\\:mm")} - {EndTime.ToString("hh\\:mm")}";
        }
    }
}
