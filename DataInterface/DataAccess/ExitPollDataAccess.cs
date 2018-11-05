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

    public class ExitPollDataAccess
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
        public DataTable GetExitPollData(string EPType, Int32 mxid, string state, string ofc, Int16 JCode, Int16 Row, Int16 SubsetID, string elecType)
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

                            // Base Question
                            if (EPType == "B")
                            {
                                cmd.CommandText = SQLCommands.sqlGetBaseExitPollData;
                                cmd.Parameters.Add("@mxid", SqlDbType.SmallInt).Value = mxid;
                                cmd.Parameters.Add("@state", SqlDbType.Text).Value = state;
                                cmd.Parameters.Add("@office", SqlDbType.Text).Value = ofc;
                                cmd.Parameters.Add("@jCode", SqlDbType.SmallInt).Value = JCode;
                                cmd.Parameters.Add("@subsetID", SqlDbType.SmallInt).Value = SubsetID;
                                cmd.Parameters.Add("@eType", SqlDbType.Text).Value = elecType;
                                
                            }
                            // Row Question
                            else if (EPType == "R")
                            {
                                cmd.CommandText = SQLCommands.sqlGetRowExitPollData;
                                cmd.Parameters.Add("@mxid", SqlDbType.SmallInt).Value = mxid;
                                cmd.Parameters.Add("@state", SqlDbType.Text).Value = state;
                                cmd.Parameters.Add("@office", SqlDbType.Text).Value = ofc;
                                cmd.Parameters.Add("@jCode", SqlDbType.SmallInt).Value = JCode;
                                cmd.Parameters.Add("@row", SqlDbType.SmallInt).Value = Row;
                                cmd.Parameters.Add("@subsetID", SqlDbType.SmallInt).Value = SubsetID;
                                cmd.Parameters.Add("@eType", SqlDbType.Text).Value = elecType;
                                cmd.Parameters.Add("@priAppCode", SqlDbType.Text).Value = "X";
                                cmd.Parameters.Add("@subsetID", SqlDbType.Bit).Value = 1;
                                
                            }
                            // Manual Question
                            else if (EPType == "M")
                            {
                                cmd.CommandText = SQLCommands.sqlGetManualExitPollData;
                                cmd.Parameters.Add("@questionID", SqlDbType.SmallInt).Value = mxid;
                                
                            }
                            
                            
                            
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
