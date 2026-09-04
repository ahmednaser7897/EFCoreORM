namespace EFMigration.Entities
{
    public class SectionSchedule
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; } = null!;
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public override string ToString()
        {
            return $"SectionId: {SectionId} | ScheduleId: {ScheduleId} | StartTime: {StartTime} | EndTime: {EndTime}";
        }
    }
}