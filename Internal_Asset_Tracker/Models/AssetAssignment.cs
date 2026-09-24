namespace Internal_Asset_Tracker.Models
{
    public class AssetAssignment
    {
        public int Id { get; set; }

      
        public int AssetId { get; set; }
        public Asset? Asset { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public DateTime AssignedDate { get; set; } 
        public DateTime? ReturnDate { get; set; }
        public bool IsActive { get; set; } 
    }
}
