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
    internal class TermDAO
    {
        public static List<Term> GetAllTerms()
        {
            string sql = "select * from TERMS";
            DataTable dt = DAO.GetDataBySql(sql,null);
            List<Term> terms = new List<Term>();
            foreach (DataRow dr in dt.Rows)
            {
                terms.Add(new Term(
                    Convert.ToInt32(dr["TermId"]),
                    dr["TermName"].ToString(),
                    dr["Description"].ToString()
                    ));
            }
            return terms;
        }

        public static Term GetTermById(int termId)
        {
            string sql = "select * from TERMS where TermId = @tid";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@tid", SqlDbType.Int);
            parameters[0].Value = termId;
            DataTable dt = DAO.GetDataBySql(sql, parameters);
            if(dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return new Term(
                    Convert.ToInt32(dr["TermId"]),
                    dr["TermName"].ToString(),
                    dr["Description"].ToString()
                    );
        }

        public static Term GetTermByTermName(string termName)
        {
            string sql = "select * from TERMS where TermName = @tname";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@tname", SqlDbType.NVarChar);
            parameters[0].Value = termName;
            DataTable dt = DAO.GetDataBySql(sql, parameters);
            if (dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return new Term(
                    Convert.ToInt32(dr["TermId"]),
                    dr["TermName"].ToString(),
                    dr["Description"].ToString()
                    );
        }
    }
}
