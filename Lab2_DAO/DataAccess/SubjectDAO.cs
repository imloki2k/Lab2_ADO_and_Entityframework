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
    internal class SubjectDAO
    {
        public static List<Subject> GetAllSubjects() {
            string sql = "select * from SUBJECTS";
            DataTable dt = DAO.GetDataBySql(sql,null);
            List<Subject> subjects = new List<Subject>();
            foreach (DataRow dr in dt.Rows)
                subjects.Add(new Subject(
                    Convert.ToInt32(dr["SubjectId"]),
                    dr["SubjectCode"].ToString(),
                    dr["SubjectName"].ToString(),
                    Convert.ToInt32(dr["DepartmentId"])
                    ));
            return subjects;
        }


        public static Subject GetSubjectById(int subjectId)
        {
            string sql = "select * from SUBJECTS where SubjectId = @sid";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@sid", SqlDbType.Int);
            parameters[0].Value = subjectId;
            DataTable dt = DAO.GetDataBySql(sql, parameters);
            if (dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return new Subject(
                    Convert.ToInt32(dr["SubjectId"]),
                    dr["SubjectCode"].ToString(),
                    dr["SubjectName"].ToString(),
                    Convert.ToInt32(dr["DepartmentId"])
                    );
        }

        public static Subject GetSubjectBySubjecCode(string subjectCode)
        {
            string sql = "select * from SUBJECTS where SubjectCode = @code";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@code", SqlDbType.NVarChar);
            parameters[0].Value = subjectCode;
            DataTable dt = DAO.GetDataBySql(sql, parameters);
            if (dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return new Subject(
                    Convert.ToInt32(dr["SubjectId"]),
                    dr["SubjectCode"].ToString(),
                    dr["SubjectName"].ToString(),
                    Convert.ToInt32(dr["DepartmentId"])
                    );
        }

    }
}
