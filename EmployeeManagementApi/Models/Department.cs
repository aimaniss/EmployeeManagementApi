namespace EmployeeManagementAPI.Models
{
    public class Department
    {
        // Primary key
        public int DepartmentID { get; set; }

        // Department name
        public string DepartmentName { get; set; }

        // Location of the department
        public string Location { get; set; }

        // Navigation property for related employees (optional, if needed)
        public ICollection<Employee> Employees { get; set; }
    }
}

