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
    public class ExitPollDataCollection
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
        public static BindingList<ExitPollDataModel> GetExitPollDataCollection(string ElectionsDBConnectionString, string EPType, int mxid, string state, string ofc, short JCode, short Row, short SubsetID, string elecType)
        {
            DataTable dataTable;

            // Clear out the current collection
            //_exitPollData.Clear();
            var exitPollRecords = new BindingList<ExitPollDataModel>();

            try
            {
                ExitPollDataAccess exitPollDataAccess  = new ExitPollDataAccess();
                exitPollDataAccess.ElectionsDBConnectionString = ElectionsDBConnectionString;
                dataTable = exitPollDataAccess.GetExitPollData(EPType, mxid, state, ofc, JCode, Row, SubsetID, elecType);

                foreach (DataRow row in dataTable.Rows)
                {
                    // Base Question
                    if (EPType == "B")
                    {
                        var newExitPollData = new ExitPollDataModel()
                        {

                            //Specific to exit polls base questions
                            mxID = row["mxID"].ToString() ?? "",
                            state = row["State"].ToString() ?? "",
                            office = row["Office"].ToString() ?? "",
                            CD = Convert.ToInt16(row["CD"] ?? 0),
                            pct = Convert.ToInt16(row["Pct"] ?? 0),
                            header = row["Header"].ToString() ?? "",
                            rowLabel = row["RowLabel"].ToString() ?? "",
                            rowNum = Convert.ToInt16(row["RowOrder"] ?? 0),
                            Question = row["Question"].ToString() ?? "",
                            subsetID = Convert.ToInt16(row["SubsetID"] ?? 0),
                            subsetName = row["SubsetName"].ToString() ?? "",
                            qseq = Convert.ToInt16(row["qseq"] ?? 0),
                            xrow = Convert.ToInt16(row["xrow"] ?? 0),
                            sweep = Convert.ToInt16(row["sweep"] ?? 0),
                            candID = Convert.ToInt16(row["cID"] ?? 0),
                        };
                        exitPollRecords.Add(newExitPollData);
                    }
                    // Row Question
                    else if (EPType == "R")
                    {
                        var newExitPollData = new ExitPollDataModel()
                        {
                            //Specific to exit polls row questions
                            mxID = row["mxID"].ToString() ?? "",
                            state = row["State"].ToString() ?? "",
                            stateName = row["StateName"].ToString() ?? "",
                            office = row["Office"].ToString() ?? "",
                            CD = Convert.ToInt16(row["CD"] ?? 0),
                            pct = Convert.ToInt16(row["Pct"] ?? 0),
                            header = row["Header"].ToString() ?? "",
                            rowLabel = row["RowLabel"].ToString() ?? "",
                            candID = Convert.ToInt16(row["CandidateID"] ?? 0),
                            party = row["Party"].ToString() ?? "",
                            Question = row["Question"].ToString() ?? "",
                            subsetID = Convert.ToInt16(row["SubsetID"] ?? 0),
                            subsetName = row["SubsetName"].ToString() ?? "",
                            qseq = Convert.ToInt16(row["qSeq"] ?? 0),
                            xrow = Convert.ToInt16(row["xRow"] ?? 0),
                            sweep = Convert.ToInt16(row["sweep"] ?? 0),
                            rowNum = Convert.ToInt16(row["outputOrder"] ?? 0),
                            };
                        exitPollRecords.Add(newExitPollData);
                    }
                    // Manual Question
                    else if (EPType == "M")
                    {
                        string Q = String.Empty;
                        Int16 QID = Convert.ToInt16(row["QuestionID"] ?? 0);
                        Q = GetManualExitPollQuestion(ElectionsDBConnectionString, QID);
                            
                        var newExitPollData = new ExitPollDataModel()
                        {

                            //Specific to exit polls manual questions
                            mxID = row["QuestionID"].ToString() ?? "",
                            Question = Q,
                            pct = Convert.ToInt16(row["Pct"] ?? 0),
                            rowLabel = row["RowText"].ToString() ?? "",
                            rowNum = Convert.ToInt16(row["Row"] ?? 0),
                        
                        };
                        exitPollRecords.Add(newExitPollData);
                    }

                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("ExitPollsCollection Exception occurred: " + ex.Message);
                //log.Debug("ExitPollsCollection Exception occurred", ex);
            }

            // Return 
            return exitPollRecords;
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
                //log.Debug("TimeFunctions Exception occurred", ex);
            }

            return Q;
        }
        #endregion
    }
}
