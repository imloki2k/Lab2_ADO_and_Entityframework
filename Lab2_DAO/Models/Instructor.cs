using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_DAO.Models
{
    internal class Instructor
    {
        public Instructor()
        {
            Courses = new HashSet<Course>();
        }

        public int InstructorId { get; set; }
        public string InstructorFirstName { get; set; }
        public string InstructorMidName { get; set; }
        public string InstructorLastName { get; set; }
        public int? DepartmentId { get; set; }

        public Instructor(int instructorId, string instructorFirstName, string instructorMidName, string instructorLastName, int? departmentId)
        {
            InstructorId = instructorId;
            InstructorFirstName = instructorFirstName;
            InstructorMidName = instructorMidName;
            InstructorLastName = instructorLastName;
            DepartmentId = departmentId;
        }

        public virtual Department Department { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
