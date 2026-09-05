// we disided not to add SectionSchedule table 
// the relation between section and schedule is one to many
// it is not many to many because one section can have only one schedule 
// and one schedule can have many sections
// so we can add ScheduleId to Section table

// namespace EFMigration.Entities
// {
//     public class SectionSchedule
//     {
//         public int Id { get; set; }
//         public int SectionId { get; set; }
//         public Section Section { get; set; } = null!;
//         // public int ScheduleId { get; set; }
//         // public Schedule Schedule { get; set; } = null!;
//         // public TimeSpan StartTime { get; set; }
//         // public TimeSpan EndTime { get; set; }

//         public override string ToString()
//         {
//             return $"SectionId: {SectionId} | ScheduleId: {ScheduleId} | StartTime: {StartTime} | EndTime: {EndTime}";
//         }
//     }
// }