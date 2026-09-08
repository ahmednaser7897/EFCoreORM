namespace EFRawSQLQuery.Entities
{
    public class Enrollment
    {
        public int SectionId { get; set; }
        public int ParticipantId { get; set; }

        public Section Section { get; set; } = null!;
        public Participant Participant { get; set; } = null!;

        public override string ToString()
        {
            return $"Enrollment ==> Section Id: {SectionId} | Participant Id: {ParticipantId}";
        }
    }
}
