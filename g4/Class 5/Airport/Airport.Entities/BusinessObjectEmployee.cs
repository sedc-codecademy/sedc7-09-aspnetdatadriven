namespace Airport.Entities
{
    public class BusinessObjectEmployee : BaseEntity
    {
        public int BusinessObjectId { get; set; }

        public BusinessObject BusinessObject { get; set; }

        public int ResponsibleEmployeeId { get; set; }

        public Employee ResponsibleEmployee { get; set; }
    }
}
