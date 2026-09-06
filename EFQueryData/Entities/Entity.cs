namespace EFQueryData.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public override string ToString()
        {
            return $"Entity ==> Id: {Id}";
        }
    }
}
