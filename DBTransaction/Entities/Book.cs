namespace DBTransaction.Entities
{
    public class Account
    {
        public string AccountNumber { get; set; } = null!;

        public string AccountHolder { get; set; } = null!;
        public decimal Balance { get; set; }
    }
}
