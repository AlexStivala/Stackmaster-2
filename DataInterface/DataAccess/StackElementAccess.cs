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

    /// <summary>
    /// Class for handling database access to a specific stack and its elements (store/recall)
    /// </summary>
    public class StackElementAccess
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public string MainDBConnectionString { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to get the list of sublist elements from the SQL DB and pass it back to the logic layer as a DataTable
        /// </summary>
        public DataTable GetStackElements(Double stackID)
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Instantiate the connection
                log.Debug($"GetStackElements stackID: {stackID}");
                log.Debug($"GetStackElements DBconn: {MainDBConnectionString}");
                using (SqlConnection connection = new SqlConnection(MainDBConnectionString))
                {
                    // Create the command and set its properties
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                        {
                            cmd.CommandText = SQLCommands.sqlGetStackElements;
                            cmd.Parameters.Add("@StackID", SqlDbType.Float).Value = stackID;
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
                log.Error("StackElementAccess Exception occurred: " + ex.Message);
                //log.Debug("StackElementAccess Exception occurred", ex);
            }

            return dataTable;
        }

        /// <summary>
        /// Method to save playlist elements for a specified playlist to the DB
        /// </summary>
        public void SaveStackElements(DataTable stackElements, Double stackID, Boolean clearStackBeforeAdding)
        {
            if (stackElements.Rows.Count > 0)
            {
                // Save out the stack
                try
                {
                    // Instantiate the connection
                    log.Debug($"SaveStackElements DBconn: {MainDBConnectionString}");

                    using (SqlConnection connection = new SqlConnection(MainDBConnectionString))
                    {
                        connection.Open();

                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {

                                SqlTransaction transaction;
                                // Start a local transaction.
                                transaction = connection.BeginTransaction("Save Stack Elements to DB");

                                // Must assign both transaction object and connection 
                                // to Command object for a pending local transaction
                                cmd.Connection = connection;
                                cmd.Transaction = transaction;

                                try
                                {
                                    //Specify base command
                                    cmd.CommandText = SQLCommands.sqlSaveStackElements;

                                    //Set parameters
                                    //cmd.CommandType = CommandType.StoredProcedure;

                                    // Set the stackElements data table as the parameter for the stored procedure
                                    //cmd.Parameters.Add("@Stack_Elements_In", SqlDbType.Structured).Value = stackElements;
                                    //Setup table-valued parameter
                                    SqlParameter tableParam = cmd.Parameters.Add(new SqlParameter("@Stack_Elements_In", SqlDbType.Structured));
                                    tableParam.Value = stackElements;
                                    //tableParam.TypeName = "dbo.UDTT_MSE_Stack_Elements";
                                    tableParam.TypeName = "dbo.UDTT_Stack_Elements";

                                    cmd.Parameters.Add("@StackID", SqlDbType.Float).Value = stackID;
                                    cmd.Parameters.Add("@ClearStackBeforeAdding", SqlDbType.Bit).Value = clearStackBeforeAdding;

                                    sqlDataAdapter.SelectCommand = cmd;
                                    sqlDataAdapter.SelectCommand.Connection = connection;
                                    sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                                    // Execute stored proc to store top-level metadata
                                    sqlDataAdapter.SelectCommand.ExecuteNonQuery();

                                    //Attempt to commit the transaction
                                    transaction.Commit();
                                    log.Debug($"Saving Stack Elements Complete");


                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    log.Error("StackElementAccess Exception occurred: " + ex.Message);

                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                    log.Error("StackElementAccess Exception occurred: " + ex.Message);
                    //log.Debug("StackElementAccess Exception occurred", ex);
                }
            }
        }

        /// <summary>
        /// Method to save playlist elements for a specified playlist to the DB
        /// </summary>
        public void SaveStackElementsDiscrete(DataTable stackElements)
        //public void SaveStackElements(StackModel stackData, DataTable stackElements)
        {
            if (stackElements.Rows.Count > 0)
            {
                try
                {
                    // Instantiate the connection
                    using (SqlConnection connection = new SqlConnection(MainDBConnectionString))
                    {
                        connection.Open();

                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                        {
                            // Iterate through data table
                            foreach (DataRow row in stackElements.Rows)
                            {
                                using (SqlCommand cmd = new SqlCommand())
                                {

                                    SqlTransaction transaction;
                                    // Start a local transaction.
                                    transaction = connection.BeginTransaction("Save Stack Elements");

                                    // Must assign both transaction object and connection 
                                    // to Command object for a pending local transaction
                                    cmd.Connection = connection;
                                    cmd.Transaction = transaction;

                                    try
                                    {
                                        //Specify base command
                                        cmd.CommandText = SQLCommands.sqlSaveStackElementsDiscrete;
                                        //Set parameters
                                        cmd.Parameters.Add("@fkey_StackID", SqlDbType.Float).Value = row["fkey_StackID"];
                                        cmd.Parameters.Add("@Stack_Element_ID", SqlDbType.BigInt).Value = row["Stack_Element_ID"];
                                        cmd.Parameters.Add("@Stack_Element_Type", SqlDbType.SmallInt).Value = row["Stack_Element_Type"];
                                        cmd.Parameters.Add("@Stack_Element_Description", SqlDbType.Text).Value = row["Stack_Element_Description"];
                                        cmd.Parameters.Add("@Stack_Element_TemplateID", SqlDbType.Text).Value = row["Stack_Element_TemplateID"];
                                        
                                        // General race/election info
                                        cmd.Parameters.Add("@Election_Type", SqlDbType.Text).Value = row["Election_Type"];
                                        cmd.Parameters.Add("@Office_Code", SqlDbType.Text).Value = row["Office_Code"];
                                        cmd.Parameters.Add("@State_Number", SqlDbType.Int).Value = row["State_Number"];
                                        cmd.Parameters.Add("@State_Mnemonic", SqlDbType.Text).Value = row["State_Mnemonic"];
                                        cmd.Parameters.Add("@State_Name", SqlDbType.Text).Value = row["State_Name"];
                                        cmd.Parameters.Add("@CD", SqlDbType.SmallInt).Value = row["CD"];
                                        cmd.Parameters.Add("@County_Number", SqlDbType.BigInt).Value = row["County_Number"];
                                        cmd.Parameters.Add("@County_Name", SqlDbType.Text).Value = row["County_Name"];
                                        cmd.Parameters.Add("@Listbox_Description", SqlDbType.Text).Value = row["Listbox_Description"];
                                        
                                        // Specific to race boards
                                        cmd.Parameters.Add("@Race_ID", SqlDbType.BigInt).Value = row["Race_ID"];
                                        cmd.Parameters.Add("@Race_RecordType", SqlDbType.Text).Value = row["Race_RecordType"];
                                        cmd.Parameters.Add("@Race_Office", SqlDbType.Text).Value = row["Race_Office"];
                                        cmd.Parameters.Add("@Race_District", SqlDbType.SmallInt).Value = row["Race_District"];
                                        cmd.Parameters.Add("@Race_CandidateID_1", SqlDbType.BigInt).Value = row["Race_CandidateID_1"];
                                        cmd.Parameters.Add("@Race_CandidateID_2", SqlDbType.BigInt).Value = row["Race_CandidateID_2"];
                                        cmd.Parameters.Add("@Race_CandidateID_3", SqlDbType.BigInt).Value = row["Race_CandidateID_3"];
                                        cmd.Parameters.Add("@Race_CandidateID_4", SqlDbType.BigInt).Value = row["Race_CandidateID_4"];
                                        cmd.Parameters.Add("@Race_PollClosingTime", SqlDbType.DateTime).Value = row["Race_PollClosingTime"];
                                        cmd.Parameters.Add("@Race_UseAPRaceCall", SqlDbType.Bit).Value = row["Race_UseAPRaceCall"];

                                        //Specific to exit polls
                                        cmd.Parameters.Add("@ExitPoll_mxID", SqlDbType.BigInt).Value = row["ExitPoll_mxID"];
                                        cmd.Parameters.Add("@ExitPoll_BoardID", SqlDbType.SmallInt).Value = row["ExitPoll_BoardID"];
                                        cmd.Parameters.Add("@ExitPoll_ShortMxLabel", SqlDbType.Text).Value = row["ExitPoll_ShortMxLabel"];
                                        cmd.Parameters.Add("@ExitPoll_NumRows", SqlDbType.SmallInt).Value = row["ExitPoll_NumRows"];
                                        cmd.Parameters.Add("@ExitPoll_xRow", SqlDbType.SmallInt).Value = row["ExitPoll_xRow"];
                                        cmd.Parameters.Add("@ExitPoll_BaseQuestion", SqlDbType.Bit).Value = row["ExitPoll_BaseQuestion"];
                                        cmd.Parameters.Add("@ExitPoll_RowQuestion", SqlDbType.Bit).Value = row["ExitPoll_RowQuestion"];
                                        cmd.Parameters.Add("@ExitPoll_Subtitle", SqlDbType.Text).Value = row["ExitPoll_Subtitle"];
                                        cmd.Parameters.Add("@ExitPoll_Suffix", SqlDbType.Text).Value = row["ExitPoll_Suffix"];
                                        cmd.Parameters.Add("@ExitPoll_HeaderText_1", SqlDbType.Text).Value = row["ExitPoll_HeaderText_1"];
                                        cmd.Parameters.Add("@ExitPoll_HeaderText_2", SqlDbType.Text).Value = row["ExitPoll_HeaderText_2"];
                                        cmd.Parameters.Add("@ExitPoll_SubsetName", SqlDbType.Text).Value = row["ExitPoll_SubsetName"];
                                        cmd.Parameters.Add("@ExitPoll_SubsetID", SqlDbType.BigInt).Value = row["ExitPoll_SubsetID"];

                                        sqlDataAdapter.SelectCommand = cmd;
                                        sqlDataAdapter.SelectCommand.Connection = connection;
                                        sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                                        // Execute stored proc to store top-level metadata
                                        sqlDataAdapter.SelectCommand.ExecuteNonQuery();

                                        //Attempt to commit the transaction
                                        transaction.Commit();
                                    }
                                    catch (Exception)
                                    {
                                        transaction.Rollback();
                                    }
                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                    log.Error("StackElementAccess Exception occurred: " + ex.Message);
                    //log.Debug("StackElementAccess Exception occurred", ex);
                }
            }
        }
        #endregion
    }
}
