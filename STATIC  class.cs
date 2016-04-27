//#define sql
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace 课程设计
{
    public static class Randseed
    {
        public static int randseed;
    }
    public static class User
    {
        public static object id;
        public static string username;
        public static string usertel;
        public static string usermail;
        public static string userid;
        public static string userstuid;
        public static string useraccount;
    }
#if  sql
    public static class Sql
    {
        public static SqlConnection sqlcon()
        {
            SqlConnection s = new SqlConnection();
            s.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\inspiron 7000\Documents\vstest.mdf;Integrated Security=True;Connect Timeout=30";
            s.Open();
            return s;
        }

        public static void close(SqlConnection s)
        {
            s.Close();
        }

        public static void dataset(SqlConnection s)
        {
            SqlDataAdapter data_sql = new SqlDataAdapter
           ("SELECT * FROM vis where account='" + User.id + "'", s);
        }

        public static void sqlupdate(SqlConnection s,string need,string add)
        {  
            SqlDataAdapter data_sql = new SqlDataAdapter("UPDATE vis SET"+ "'"+need +"'"+"="+"'"+add+"'"+
                "where 'id' ="+"'"+User.id+"'", s);
                 data_sql.UpdateCommand("UPDATE vis SET" + "'" + need + "'" + "=" + "'" + add + "'" +
                "where 'id' =" + "'" + User.id + "'");
            //   cmand.Parameters.Add(new SqlParameter(""));
        
        }

    }
#endif
}
