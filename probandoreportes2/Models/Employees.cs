using System.ComponentModel.DataAnnotations;

namespace probandoreportes2.Models
{
    public class Employees
    {
        [Key]
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
