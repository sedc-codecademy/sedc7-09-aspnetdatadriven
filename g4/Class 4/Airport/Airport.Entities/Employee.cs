namespace Airport.Entities
{
    public class Employee : BaseEntity
    {
        public string FullName { get; set; }

        public int BusinessObjectId { get; set; }

        public BusinessObject ResponsibleFor { get; set; }
    }
}