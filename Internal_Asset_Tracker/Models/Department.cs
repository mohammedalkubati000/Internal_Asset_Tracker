namespace Internal_Asset_Tracker.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
