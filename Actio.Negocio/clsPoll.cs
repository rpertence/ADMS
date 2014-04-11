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
    public class clsPoll
    {
         private static readonly string _connectionString;

        static clsPoll()
        {
            _connectionString = "Data Source=sqlserver.actiocomunicacao.com.br; DATABASE=actiocom;UID=actiocom_bytech; PWD=sw0rdfish;";
        }


        #region Fields
        private int _pollId;
        private DateTime _addedDate;
        private string _addedBy ;
        private string _questionText;
        private bool _isCurrent ;
        private bool _isArchived ;
        private DateTime _archivedDate ;
        private int _votes;

        #endregion
        #region Properties
       

        public int PollID
        {
            get { return _pollId; }
            set { _pollId = value; }
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

        public string QuestionText
        {
            get { return _questionText; }
            set { _questionText = value; }
        }

        public bool IsCurrent
        {
            get { return _isCurrent; }
            set { _isCurrent = value; }
        }

        public bool IsArchived
        {
            get { return _isArchived; }
            set { _isArchived = value; }
        }

        public DateTime ArchivedDate
        {
            get { return _archivedDate; }
            set { _archivedDate = value; }
        }

        public int Votes
        {
            get { return _votes; }
            set { _votes = value; }
        }
        #endregion
        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static int InsertPoll(string questionText, bool isCurrent)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
               
                SqlCommand cmd = new SqlCommand("vsd_Polls_InsertPoll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AddedBy", SqlDbType.NVarChar).Value = clsUtil.UserOnLine;
                cmd.Parameters.Add("@QuestionText", SqlDbType.NVarChar).Value = questionText;
                cmd.Parameters.Add("@IsCurrent", SqlDbType.Bit).Value = isCurrent;
                cmd.Parameters.Add("@PollID", SqlDbType.Int).Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                return (int)cmd.Parameters["@PollID"].Value;
            }
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static bool UpdatePoll(int pollId, string questionText, bool isCurrent)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("vsd_Polls_UpdatePoll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PollID", SqlDbType.Int).Value = pollId;
                cmd.Parameters.Add("@QuestionText", SqlDbType.NVarChar).Value = questionText;
                cmd.Parameters.Add("@IsCurrent", SqlDbType.Bit).Value = isCurrent;
                cn.Open();
                int ret = cmd.ExecuteNonQuery();
                return (ret == 1);
            }
        }

        
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static bool DeletePoll(int pollID)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("vsd_Polls_DeletePoll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PollID", SqlDbType.Int).Value = pollID;
                cn.Open();
                int ret = cmd.ExecuteNonQuery();
                return (ret == 1);
            }
        }

        
        [DataObjectMethodAttribute(DataObjectMethodType.Update)]
        public static bool ArchivePoll(int pollId)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("vsd_Polls_ArchivePoll", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PollID", SqlDbType.Int).Value = pollId;
                cn.Open();
                int ret = cmd.ExecuteNonQuery();
                return (ret == 1);
            }
        }

        
        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public static List<clsPoll> GetPolls(bool includeActive, bool includeArchived)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("vsd_Polls_GetPolls", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IncludeActive", SqlDbType.Bit).Value = includeActive;
                cmd.Parameters.Add("@IncludeArchived", SqlDbType.Bit).Value = includeArchived;
                List<clsPoll> results = new List<clsPoll>();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    results.Add(new clsPoll(reader));
                return results;
            }
        }


        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public static int GetCurrentPollId()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("vsd_Polls_GetCurrentPollID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PollID", SqlDbType.Int).Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                return (int)cmd.Parameters["@PollID"].Value;
            }
        }

       
        [DataObjectMethodAttribute(DataObjectMethodType.Select)]
        public static clsPoll GetPollByID(int pollID)
        {
            clsPoll result;
            using (SqlConnection cn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("vsd_Polls_GetPollByID", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PollID", SqlDbType.Int).Value = pollID;
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    result = new clsPoll(reader);
                else
                    return null;
            }
            return result;
        }

        
    





        #endregion
        #region DataReader

        /// <summary>
        /// DataReader
        /// </summary>
        /// <param name="reader"></param>
        public clsPoll(SqlDataReader reader)
        {
            _pollId = (int)reader["PollID"];
            _addedDate = (DateTime)reader["AddedDate"];
            _addedBy = (string)reader["AddedBy"];
            _questionText = (string)reader["QuestionText"];
            _isCurrent = (bool)reader["IsCurrent"];
            _isArchived = (bool)reader["IsArchived"];
            if (reader["ArchivedDate"] != DBNull.Value)
                _archivedDate = (DateTime)reader["ArchivedDate"];
            if (reader["Votes"] != DBNull.Value)
                _votes = (int)reader["Votes"];
        }
        #endregion

    }

}



