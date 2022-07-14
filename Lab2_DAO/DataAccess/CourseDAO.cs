using Lab2_DAO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_DAO.DataAccess
{
    internal class CourseDAO
    {
        public static List<Course> GetAllCourses()
        {
            string sql = "select * from COURSES";
            DataTable dt = DAO.GetDataBySql(sql, null);
            List<Course> courses = new List<Course>();
            foreach (DataRow dr in dt.Rows)
            {
                string first = InstructorDAO.GetInstructorById(Convert.ToInt32(dr["InstructorId"])).InstructorFirstName;
                string mid = InstructorDAO.GetInstructorById(Convert.ToInt32(dr["InstructorId"])).InstructorMidName;
                string last = InstructorDAO.GetInstructorById(Convert.ToInt32(dr["InstructorId"])).InstructorLastName;
                courses.Add(new Course(
                    Convert.ToInt32(dr["CourseId"]),
                    dr["CourseCode"].ToString(),
                    dr["CourseDescription"].ToString(),
                    SubjectDAO.GetSubjectById(Convert.ToInt32(dr["SubjectId"])).SubjectCode.ToString(),
                    first + " " + mid + " " + last,
                    TermDAO.GetTermById(Convert.ToInt32(dr["TermId"])).TermName.ToString(),
                    CampusDAO.GetCampusById(Convert.ToInt32(dr["CampusID"])).CampusName.ToString()
                    ));
            }
            return courses;
        }

        public static List<Course> GetCoursesBySubjectCode(string subjectCode)
        {
            string sql = "SELECT * FROM [COURSES] as c " +
                "inner join SUBJECTS as s on c.SubjectId = s.SubjectId " +
                "inner join INSTRUCTORS as i on c.InstructorId = i.InstructorId " +
                "inner join TERMS as t on c.TermId = t.TermId " +
                "inner join CAMPUSES as ca on c.CampusID = ca.CampusId " +
                "where SubjectCode = @subjectCode";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@subjectCode", SqlDbType.NVarChar);
            parameters[0].Value = subjectCode;
            DataTable dt = DAO.GetDataBySql(sql, parameters);
            List<Course> courses = new List<Course>();
            foreach (DataRow dr in dt.Rows)
            {
                string first = InstructorDAO.GetInstructorById(Convert.ToInt32(dr["InstructorId"])).InstructorFirstName;
                string mid = InstructorDAO.GetInstructorById(Convert.ToInt32(dr["InstructorId"])).InstructorMidName;
                string last = InstructorDAO.GetInstructorById(Convert.ToInt32(dr["InstructorId"])).InstructorLastName;
                courses.Add(new Course(
                    Convert.ToInt32(dr["CourseId"]),
                    dr["CourseCode"].ToString(),
                    dr["CourseDescription"].ToString(),
                    SubjectDAO.GetSubjectById(Convert.ToInt32(dr["SubjectId"])).SubjectCode.ToString(),
                    first + " " + mid + " " + last,
                    TermDAO.GetTermById(Convert.ToInt32(dr["TermId"])).TermName.ToString(),
                    CampusDAO.GetCampusById(Convert.ToInt32(dr["CampusID"])).CampusName.ToString()
                    ));
            }
            return courses;
        }

        public static List<Course> GetCoursesByInstructorName(int id)
        {
            string sql = "SELECT * FROM [COURSES] as c " +
                "inner join SUBJECTS as s on c.SubjectId = s.SubjectId " +
                "inner join INSTRUCTORS as i on c.InstructorId = i.InstructorId " +
                "inner join TERMS as t on c.TermId = t.TermId " +
                "inner join CAMPUSES as ca on c.CampusID = ca.CampusId " +
                "where i.InstructorId = @id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = id;
            DataTable dt = DAO.GetDataBySql(sql, parameters);
            List<Course> courses = new List<Course>();
            foreach (DataRow dr in dt.Rows)
            {
                string first = InstructorDAO.GetInstructorById(Convert.ToInt32(dr["InstructorId"])).InstructorFirstName;
                string mid = InstructorDAO.GetInstructorById(Convert.ToInt32(dr["InstructorId"])).InstructorMidName;
                string last = InstructorDAO.GetInstructorById(Convert.ToInt32(dr["InstructorId"])).InstructorLastName;
                courses.Add(new Course(
                    Convert.ToInt32(dr["CourseId"]),
                    dr["CourseCode"].ToString(),
                    dr["CourseDescription"].ToString(),
                    SubjectDAO.GetSubjectById(Convert.ToInt32(dr["SubjectId"])).SubjectCode.ToString(),
                    first + " " + mid + " " + last,
                    TermDAO.GetTermById(Convert.ToInt32(dr["TermId"])).TermName.ToString(),
                    CampusDAO.GetCampusById(Convert.ToInt32(dr["CampusID"])).CampusName.ToString()
                    ));
            }
            return courses;
        }

        public static int DeleteCourseById(int cid)
        {
            string sql = "DELETE FROM [COURSES] " +
                "WHERE CourseId = @id";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@id", SqlDbType.Int);
            parameters[0].Value = cid;
            return DAO.ExecuteSql(sql, parameters);
        }

        public static int InsertCourse(Course c)
        {
            string sql = "INSERT INTO [COURSES] " +
                "([CourseCode] " +
                ",[CourseDescription] " +
                ",[SubjectId] " +
                ",[InstructorId] " +
                ",[TermId] " +
                ",[CampusID]) " +
                "VALUES " +
                "(@code " +
                ",@description " +
                ",@sid " +
                ",@iid " +
                ",@tid " +
                ",@cid)";

            SqlParameter p1 = new SqlParameter("@code", SqlDbType.NVarChar);
            p1.Value = c.CourseCode;
            SqlParameter p2 = new SqlParameter("@description", SqlDbType.NVarChar);
            p2.Value = c.CourseDescription;
            SqlParameter p3 = new SqlParameter("@sid", SqlDbType.Int);
            p3.Value = c.subjectid;
            SqlParameter p4 = new SqlParameter("@iid", SqlDbType.Int);
            p4.Value = c.instructorid;
            SqlParameter p5 = new SqlParameter("@tid", SqlDbType.Int);
            p5.Value = c.termid;
            SqlParameter p6 = new SqlParameter("@cid", SqlDbType.Int);
            p6.Value = c.campusid;
            SqlParameter[] parameters = { p1, p2, p3, p4, p5, p6 };
            return DAO.ExecuteSql(sql, parameters);

        }

        public static int EditCourse(Course c, int id)
        {
            string sql = @"UPDATE [COURSES]
            SET [CourseCode] = @code
            ,[CourseDescription] = @description
            ,[SubjectId] = @sid
            ,[InstructorId] = @iid
            ,[TermId] = @tid
            ,[CampusID] = @cid
             WHERE CourseId = @id";

            SqlParameter p1 = new SqlParameter("@code", SqlDbType.NVarChar);
            p1.Value = c.CourseCode;
            SqlParameter p2 = new SqlParameter("@description", SqlDbType.NVarChar);
            p2.Value = c.CourseDescription;
            SqlParameter p3 = new SqlParameter("@sid", SqlDbType.Int);
            p3.Value = c.subjectid;
            SqlParameter p4 = new SqlParameter("@iid", SqlDbType.Int);
            p4.Value = c.instructorid;
            SqlParameter p5 = new SqlParameter("@tid", SqlDbType.Int);
            p5.Value = c.termid;
            SqlParameter p6 = new SqlParameter("@cid", SqlDbType.Int);
            p6.Value = c.campusid;
            SqlParameter p7 = new SqlParameter("@id", SqlDbType.Int);
            p7.Value = id;
            SqlParameter[] parameters = { p1, p2, p3, p4, p5, p6, p7 };
            return DAO.ExecuteSql(sql, parameters);
        }

    }
}
