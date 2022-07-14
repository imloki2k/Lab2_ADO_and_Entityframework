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
    internal class InstructorDAO
    {
        public static List<Instructor> GetInstructors()
        {
            string sql = "select * from INSTRUCTORS";
            DataTable dt = DAO.GetDataBySql(sql,null);
            List<Instructor> instructors = new List<Instructor>();
            foreach (DataRow dr in dt.Rows)
            {
                instructors.Add(new Instructor(
                    Convert.ToInt32(dr["InstructorId"]),
                    dr["InstructorFirstName"].ToString(),
                    dr["InstructorMidName"].ToString(),
                    dr["InstructorLastName"].ToString(),
                    Convert.ToInt32(dr["DepartmentId"])
                    ));
            }
            return instructors;
        }


        public static Instructor GetInstructorById(int InstructorId)
        {
            string sql = "select * from INSTRUCTORS where InstructorId = @iid";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@iid",SqlDbType.Int);
            parameters[0].Value = InstructorId;
            DataTable dt = DAO.GetDataBySql(sql,parameters);
            if (dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return new Instructor(
                    Convert.ToInt32(dr["InstructorId"]),
                    dr["InstructorFirstName"].ToString(),
                    dr["InstructorMidName"].ToString(),
                    dr["InstructorLastName"].ToString(),
                    Convert.ToInt32(dr["DepartmentId"])
                    );
        }
    }
}
