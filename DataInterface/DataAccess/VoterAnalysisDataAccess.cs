using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    using DataInterface.DataModel;
    using DataInterface.SQL;

    public class VoterAnalysisDataAccess
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public string ElectionsDBConnectionString { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to get the Exit Poll Data
        /// </summary>
        public DataTable GetVoterAnalysisData(string r_type, string VA_Data_Id, int ft)
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Instantiate the connection
                using (SqlConnection connection = new SqlConnection(ElectionsDBConnectionString))
                {
                    // Create the command and set its properties
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                        {
                            
                            if (ft == 0)
                            {
                                // Question
                                //if (r_type == "Q")
                                //cmd.CommandText = SQLCommands.sqlGetVoterAnalysisQuestionData;
                                // Answer
                                //else if (r_type == "A")
                                //cmd.CommandText = SQLCommands.sqlGetVoterAnalysisAnswerData;
                                cmd.CommandText = SQLCommands.sqlGetVoterAnalysisDataFS;

                            }
                            else
                            {
                                //// Question
                                //if (r_type == "Q")
                                //cmd.CommandText = SQLCommands.sqlGetVoterAnalysisQuestionData_Tkr;
                                // Answer
                                //else if (r_type == "A")
                                //cmd.CommandText = SQLCommands.sqlGetVoterAnalysisAnswerData_Tkr;
                                cmd.CommandText = SQLCommands.sqlGetVoterAnalysisDataTkr;

                            }

                            cmd.Parameters.Add("@VA_Data_Id", SqlDbType.VarChar).Value = VA_Data_Id;
                            cmd.Parameters.Add("@r_type", SqlDbType.VarChar).Value = r_type;

                            sqlDataAdapter.SelectCommand = cmd;
                            sqlDataAdapter.SelectCommand.Connection = connection;
                            sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                            // Fill the datatable from adapter
                            sqlDataAdapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("ExitPollDataAccess Exception occurred: " + ex.Message);
                //log.Debug("ExitPollDataAccess Exception occurred", ex);
            }

            return dataTable;
        }

        public DataTable GetVoterAnalysisManualData(string VA_Data_Id)
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Instantiate the connection
                using (SqlConnection connection = new SqlConnection(ElectionsDBConnectionString))
                {
                    // Create the command and set its properties
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                        {
                            cmd.CommandText = SQLCommands.sqlGetVoterAnalysisManualData;

                            cmd.Parameters.Add("@VA_Data_Id", SqlDbType.VarChar).Value = VA_Data_Id;
                            
                            sqlDataAdapter.SelectCommand = cmd;
                            sqlDataAdapter.SelectCommand.Connection = connection;
                            sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                            // Fill the datatable from adapter
                            sqlDataAdapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("ExitPollDataAccess Exception occurred: " + ex.Message);
                //log.Debug("ExitPollDataAccess Exception occurred", ex);
            }

            return dataTable;
        }

        #endregion
    }
}
