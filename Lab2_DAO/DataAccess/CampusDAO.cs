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
    internal class CampusDAO
    {
        public static List<Campus> GetAllCampuses()
        {
            string sql = "select * from CAMPUSES";
            DataTable dt = DAO.GetDataBySql(sql,null);
            List<Campus> campuses = new List<Campus>();
            foreach (DataRow dr in dt.Rows)
            {
                campuses.Add(new Campus(
                    Convert.ToInt32(dr["CampusId"]),
                    dr["CampusCode"].ToString(),
                    dr["CampusName"].ToString(),
                    dr["Description"].ToString()
                    ));
            }
            return campuses;
        }

        public static Campus GetCampusById(int campusId)
        {
            string sql = "select * from CAMPUSES where CampusId = @cid";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@cid", SqlDbType.Int);
            parameters[0].Value = campusId;
            DataTable dt = DAO.GetDataBySql(sql,parameters);
            if (dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return new Campus(
                    Convert.ToInt32(dr["CampusId"]),
                    dr["CampusCode"].ToString(),
                    dr["CampusName"].ToString(),
                    dr["Description"].ToString()
                    );
        }

        public static Campus GetCampusByCampusName(string campusName)
        {
            string sql = "select * from CAMPUSES where CampusName = @name";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@name", SqlDbType.NVarChar);
            parameters[0].Value = campusName;
            DataTable dt = DAO.GetDataBySql(sql, parameters);
            if (dt.Rows.Count == 0) return null;
            DataRow dr = dt.Rows[0];
            return new Campus(
                    Convert.ToInt32(dr["CampusId"]),
                    dr["CampusCode"].ToString(),
                    dr["CampusName"].ToString(),
                    dr["Description"].ToString()
                    );
        }
    }
}
