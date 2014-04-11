using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace PizzaDev.Poll
 
{
    [DataObject(true)]
    public class clsOptions 
    {
         private static readonly string _connectionString;

        static clsOptions()
        {
            _connectionString = "Data Source=sqlserver.actiocomunicacao.com.br; DATABASE=actiocom;UID=actiocom_rccbh; PWD=sw0rdfish;";
          //  _connectionString = WebConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        }


        #region Fields
        private int _optionID;
        private int _pollID;
        private DateTime _addedDate;
        private string _addedBy;
        private string _optionText;
        private int _votes;
        private decimal _percentage;
        #endregion
        #region Properties
        public int OptionID
        {
            get { return _optionID; }
            set { _optionID = value; }
        }

        public int PollID
        {
            get { return _pollID; }
            set { _pollID = value; }
        }

        public DateTime AddedDate
        {
            get { return _addedDate; }
            set { _addedDate = value; }
        }

        public string AddedBy
        {
            get { return _addedBy; }
            set { _addedBy = value; }
        }

        public string OptionText
        {
            get { return _optionText; }
            set { _optionText = value; }
        }

        public int Votes
        {
            get { return _votes; }
            set { _votes = value; }
        }

        public decimal Percentage
        {
            get { return _percentage; }
            set { _percentage = value; }
        }

        #endregion
        #region Methods


        
        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public static List<clsOptions> GetOptions(int pollID)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("vsd_Polls_GetOptions", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PollID", SqlDbType.Int).Value = pollID;
                List<clsOptions> results = new List<clsOptions>();
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    results.Add(new clsOptions(reader));
                return results;
            }
        }

       

        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public static clsOptions GetOptionByID(int optionID)
        {
            clsOptions results;
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("vsd_Polls_GetOptionByID", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OptionID", SqlDbType.Int).Value = optionID;

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    results = new clsOptions(reader);
                else
                    return null;
            }
            return results;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert)]
        public static int InsertOption(int pollID, string optionText)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("vsd_Polls_InsertOption", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = clsUtil.UserOnLine;
                cmd.Parameters.Add("@PollID", SqlDbType.Int).Value = pollID;
                cmd.Parameters.Add("@OptionText", SqlDbType.NVarChar).Value = optionText;
                cmd.Parameters.Add("@OptionID", SqlDbType.Int).Direction = ParameterDirection.Output;
                cn.Open();
                cmd.ExecuteNonQuery();
                return (int)cmd.Parameters["@OptionID"].Value;
            }
        }

        
        [DataObjectMethodAttribute(DataObjectMethodType.Update)]
        public static bool UpdateOption(int optionID, string optionText)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("vsd_Polls_UpdateOption", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OptionID", SqlDbType.Int).Value = optionID;
                cmd.Parameters.Add("@OptionText", SqlDbType.NVarChar).Value = optionText;
                cn.Open();
                int ret = cmd.ExecuteNonQuery();
                return (ret == 1);
            }
        }

       
        [DataObjectMethodAttribute(DataObjectMethodType.Delete)]
        public static bool DeleteOption(int optionID)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("vsd_Polls_DeleteOption", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OptionID", SqlDbType.Int).Value = optionID;
                cn.Open();
                int ret = cmd.ExecuteNonQuery();
                return (ret == 1);
            }
        }

        
        [DataObjectMethodAttribute(DataObjectMethodType.Update)]
        public static bool InsertVote(int optionID)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("vsd_Polls_InsertVote", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OptionID", SqlDbType.Int).Value = optionID;
                cn.Open();
                int ret = cmd.ExecuteNonQuery();
                return (ret == 1);
            }
        }

        #endregion
        #region DataReader
          /// <summary>
         /// DataReader
         /// </summary>
         /// <param name="reader"></param>
         public clsOptions(SqlDataReader reader)
         {
             _optionID = (int)reader["OptionID"];
             _addedDate = (DateTime)reader["AddedDate"];
             _addedBy = (string)reader["AddedBy"];
             _pollID = (int)reader["PollID"];
             _optionText = (string)reader["OptionText"];
             _votes = (int)reader["Votes"];
             if (reader["Percentage"] != DBNull.Value)
                 _percentage = (decimal)reader["Percentage"];
         }
        #endregion

       
    }
}



