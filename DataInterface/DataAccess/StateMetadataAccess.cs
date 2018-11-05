using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.DataAccess
{
    using System.Data;
    using System.Data.SqlClient;

    using DataInterface.DataModel;
    using DataInterface.SQL;

    /// <summary>
    /// Class for handling database access to the list of currently available stacks
    /// </summary>
    public class StateMetadataAccess
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public string ElectionsDBConnectionString { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to get the list of existing MSE Stacks and pass it back to the logic layer as a DataTable
        /// </summary>
        public DataTable GetStateMetadata()
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
                            cmd.CommandText = SQLCommands.sqlGetStateMetadata;
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
                log.Error("StateMetadataAccess Exception occurred: " + ex.Message);
                //log.Debug("StateMetadataAccess Exception occurred", ex);
            }
            return dataTable;
        }
        #endregion
    }
}
