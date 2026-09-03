namespace EntityTypesAndMapping.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }
}
