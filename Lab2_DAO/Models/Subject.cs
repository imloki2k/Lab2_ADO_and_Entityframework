using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_DAO.Models
{
    internal class Subject
    {
        public Subject()
        {
            Courses = new HashSet<Course>();
        }

        public int SubjectId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public int? DepartmentId { get; set; }

        public Subject(int subjectId, string subjectCode, string subjectName, int? departmentId)
        {
            SubjectId = subjectId;
            SubjectCode = subjectCode;
            SubjectName = subjectName;
            DepartmentId = departmentId;
        }

        public virtual Department Department { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
