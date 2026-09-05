using EFMigration.Enums;
namespace EFMigration.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public ScheduleEnum Title { get; set; }
        public bool SUN { get; set; }
        public bool MON { get; set; }
        public bool TUE { get; set; }
        public bool WED { get; set; }
        public bool THU { get; set; }
        public bool FRI { get; set; }
        public bool SAT { get; set; }

        // we disided not to add SectionSchedule table 
        // the relation between section and schedule is one to many
        // it is not many to many because one section can have only one schedule 
        // and one schedule can have many sections
        // so we can add ScheduleId to Section table
        public ICollection<Section> Sections { get; set; } = new List<Section>();
        //public ICollection<SectionSchedule> SectionSchedules { get; set; } = new List<SectionSchedule>();

        public override string ToString()
        {
            return $"Title: {Title} | Id: {Id}";
        }
    }
}