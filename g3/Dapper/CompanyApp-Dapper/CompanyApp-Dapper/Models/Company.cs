namespace CompanyApp_Dapper.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Company(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public Company()
        {
            
        }
    }
}
