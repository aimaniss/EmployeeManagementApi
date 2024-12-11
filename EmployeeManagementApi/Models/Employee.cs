namespace EmployeeManagementAPI.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DateBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }

        // Foreign Key to Department
        public int DepartmentID { get; set; }

        // Navigation property to Department
        public Department Department { get; set; }
    }
}
