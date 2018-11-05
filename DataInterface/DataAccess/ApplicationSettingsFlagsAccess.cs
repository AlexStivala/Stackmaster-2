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

    public class ApplicationSettingsFlagsAccess
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public string ElectionsDBConnectionString { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to get the list of available races for the current election event
        /// </summary>
        public DataTable GetFlags()
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
                            cmd.CommandText = SQLCommands.sqlGetFlags;
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
                log.Error("ApplicationSettingsFlagsAccess Exception occurred while getting settings: " + ex.Message);
                //log.Debug("ApplicationSettingsFlagsAccess Exception occurred while getting settings", ex);
            }

            return dataTable;
        }

        /// <summary>
        /// Method to post an entry to the election applications log
        /// </summary>
        public void PostApplicationLogEntry(
                            string application_Name, 
                            string application_Description,
                            string hostPC_Name,
                            string hostPC_IP_Address,
                            Boolean engine_Enabled_1,
                            string engine_IP_Address_1,
                            Boolean engine_Enabled_2,
                            string engine_IP_Address_2,
                            string entry_Text,
                            string application_Version,
                            int application_ID,
                            string comments,
                            DateTime currentSystemTime,
                            Boolean engine_Enabled_3,
                            string engine_IP_Address_3,
                            Boolean engine_Enabled_4,
                            string engine_IP_Address_4
                           )
        {
            try
            {
                // Instantiate the connection
                using (SqlConnection connection = new SqlConnection(ElectionsDBConnectionString))
                {
                    connection.Open();

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {

                            SqlTransaction transaction;
                            // Start a local transaction.
                            transaction = connection.BeginTransaction("Post Application Log Entry");

                            // Must assign both transaction object and connection 
                            // to Command object for a pending local transaction
                            cmd.Connection = connection;
                            cmd.Transaction = transaction;

                            try
                            {
                                //Specify base command
                                cmd.CommandText = SQLCommands.sqlSetFGEApplicationLogEntry;

                                //Set parameters
                                cmd.Parameters.Add("@Application_Name", SqlDbType.NVarChar).Value = application_Name;
                                cmd.Parameters.Add("@Application_Description", SqlDbType.NVarChar).Value = application_Description;
                                cmd.Parameters.Add("@HostPC_Name", SqlDbType.NVarChar).Value = hostPC_Name;
                                cmd.Parameters.Add("@HostPC_IP_Address", SqlDbType.NVarChar).Value = hostPC_IP_Address;
                                cmd.Parameters.Add("@Engine_Enabled_1", SqlDbType.Bit).Value = engine_Enabled_1;
                                cmd.Parameters.Add("Engine_IP_Address_1", SqlDbType.NVarChar).Value = engine_IP_Address_1;
                                cmd.Parameters.Add("@Engine_Enabled_2", SqlDbType.Bit).Value = engine_Enabled_2;
                                cmd.Parameters.Add("Engine_IP_Address_2", SqlDbType.NVarChar).Value = engine_IP_Address_2;
                                cmd.Parameters.Add("Entry_Text", SqlDbType.NVarChar).Value = entry_Text;
                                cmd.Parameters.Add("@Application_Version", SqlDbType.NVarChar).Value = application_Version;
                                cmd.Parameters.Add("@Application_ID", SqlDbType.Int).Value = application_ID;
                                cmd.Parameters.Add("@Comments", SqlDbType.NVarChar).Value = comments;
                                cmd.Parameters.Add("@CurrentSystemTime", SqlDbType.DateTime).Value = currentSystemTime;
                                cmd.Parameters.Add("@Engine_Enabled_3", SqlDbType.Bit).Value = engine_Enabled_3;
                                cmd.Parameters.Add("Engine_IP_Address_3", SqlDbType.NVarChar).Value = engine_IP_Address_3;
                                cmd.Parameters.Add("@Engine_Enabled_4", SqlDbType.Bit).Value = engine_Enabled_4;
                                cmd.Parameters.Add("Engine_IP_Address_4", SqlDbType.NVarChar).Value = engine_IP_Address_4;

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
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("ApplicationSettingsFlagsAccess while posting application log entry: " + ex.Message);
                //log.Debug("ApplicationSettingsFlagsAccess while posting application log entry", ex);
            }
        }

        #endregion
    }
}