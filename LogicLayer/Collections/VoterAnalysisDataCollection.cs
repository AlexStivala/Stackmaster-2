using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataInterface.DataModel;
using DataInterface.DataAccess;
using DataInterface.SQL;
using System.Data.SqlClient;
using System.ComponentModel;


namespace LogicLayer.Collections
{
    /// <summary>
    /// Class for operations related to the available races
    /// </summary>
    public class VoterAnalysisDataCollection
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        //   private readonly BindingList<ExitPollDataModel> _exitPollData = new BindingList<ExitPollDataModel>();
        //public string ElectionsDBConnectionString { get; set; }
        #endregion

        #region Public Methods
        // Constructor - instantiates list collection
        //public ExitPollDataCollection()
        //{
        //}

        /// <summary>
        /// Get the exit poll data from the SQL DB; clears out existing collection first
        /// </summary>
        public static BindingList<VoterAnalysisDataModel> GetVoterAnalysisDataCollection(string r_type, string VA_Data_Id, string ElectionsDBConnectionString, int ft)
        {
            DataTable dataTable;

            // Clear out the current collection
            //_exitPollData.Clear();
            var voterAnalysisRecords = new BindingList<VoterAnalysisDataModel>();

            try
            {
                VoterAnalysisDataAccess voterAnalysisDataAccess = new VoterAnalysisDataAccess();
                voterAnalysisDataAccess.ElectionsDBConnectionString = ElectionsDBConnectionString;
                dataTable = voterAnalysisDataAccess.GetVoterAnalysisData(r_type, VA_Data_Id, ft);

                foreach (DataRow row in dataTable.Rows)
                {
                    
                    
                    // Base Question
                    if (r_type == "Q")
                    {
                        var voterAnalysisData = new VoterAnalysisDataModel()
                        {

                            //Specific to exit polls base questions
                            question = row["question"].ToString() ?? "",
                            answer = row["answer"].ToString() ?? "",
                            preface = row["preface"].ToString() ?? "",
                            percent = Convert.ToInt32(row["variable_percent"] ?? 0)
                        };

                        voterAnalysisData.r_type = r_type;
                        voterAnalysisData.VA_Data_Id = VA_Data_Id;
                        voterAnalysisRecords.Add(voterAnalysisData);
                    }
                    // Row Question
                    else if (r_type == "A")
                    {
                        var voterAnalysisData = new VoterAnalysisDataModel()
                        {
                            //Specific to exit polls row questions
                            question = row["question"].ToString() ?? "",
                            answer = row["answer"].ToString() ?? "",
                            preface = row["preface"].ToString() ?? "",
                            percent = Convert.ToInt32(row["result_percent"] ?? 0),
                            name = row["name"].ToString() ?? "",
                            party = row["party"].ToString() ?? "",
                            id = Convert.ToInt32(row["id"] ?? 0)

                        };
                        voterAnalysisData.r_type = r_type;
                        voterAnalysisData.VA_Data_Id = VA_Data_Id;
                        voterAnalysisRecords.Add(voterAnalysisData);
                    }
                    // Manual Question
                    else if (r_type == "M")
                    {
                        string Q = String.Empty;
                        Int16 QID = Convert.ToInt16(row["QuestionID"] ?? 0);
                        Q = GetManualExitPollQuestion(ElectionsDBConnectionString, QID);

                        var newExitPollData = new VoterAnalysisDataModel()
                        {

                            //Specific to exit polls manual questions
                            
                        };
                        voterAnalysisRecords.Add(newExitPollData);
                    }

                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("ExitPollsCollection Exception occurred: " + ex.Message);
                log.Debug("ExitPollsCollection Exception occurred", ex);
            }

            // Return 
            return voterAnalysisRecords;
        }


        // Get the Manual Exit Poll Question from the DB
        public static string GetManualExitPollQuestion(string ElectionsDBConnectionString, Int16 QuestionID)
        {
            string Q = String.Empty;
            try
            {
                // Instantiate the connection
                using (SqlConnection sqlconn = new SqlConnection(ElectionsDBConnectionString))
                {
                    // Create the command and set its properties
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlconn.Open();
                        cmd.CommandText = SQLCommands.sqlGetManualExitPollQuestion;
                        cmd.Parameters.Add("@questionID", SqlDbType.SmallInt).Value = QuestionID;
                        cmd.Connection = sqlconn;
                        Q = (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("TimeFunctions Exception occurred: " + ex.Message);
                log.Debug("TimeFunctions Exception occurred", ex);
            }

            return Q;
        }
        #endregion
    }
}
