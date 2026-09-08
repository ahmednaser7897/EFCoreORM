namespace EFRawSQLQuery.Entities
{
    public class Corporate : Participant
    {
        public string? Company { get; set; }
        public string? JobTitle { get; set; }

        public override string ToString()
        {
            return $"Corporate ==> Id: {Id} | Name: {LName}, {FName} | Job Title: {JobTitle} | Company: {Company}";
        }
    }
}
