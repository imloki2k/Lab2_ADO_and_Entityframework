using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_DAO.Models
{
    internal class Course
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseDescription { get; set; }
        public string SubjectCode { get; set; }
        public string InstructorName { get; set; }
        public string TermName { get; set; }
        public string CampusName { get; set; }
        public int subjectid { get; set; }
        public int? instructorid { get; set; }
        public int? termid { get; set; }
        public int? campusid { get; set; }

        public Course(string courseCode, string courseDescription, int subjectid, int? instructorid, int? termid, int? campusid)
        {
            CourseCode = courseCode;
            CourseDescription = courseDescription;
            this.subjectid = subjectid;
            this.instructorid = instructorid;
            this.termid = termid;
            this.campusid = campusid;
        }

        public Course(int courseId, string courseCode, string courseDescription, string subjectCode, string instructorName, string termName, string campusName)
        {
            CourseId = courseId;
            CourseCode = courseCode;
            CourseDescription = courseDescription;
            SubjectCode = subjectCode;
            InstructorName = instructorName;
            TermName = termName;
            CampusName = campusName;
        }
    }
}
