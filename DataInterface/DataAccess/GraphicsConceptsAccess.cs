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

    public class GraphicsConceptsAccess
    {
        #region Logger instantiation - uses reflection to get module name

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Properties and Members

        public string GraphicsDBConnectionString { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to get the list of available races for the current election event
        /// </summary>
        public DataTable GetGraphicsConcepts()
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Instantiate the connection
                using (SqlConnection connection = new SqlConnection(GraphicsDBConnectionString))
                {
                    // Create the command and set its properties
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                        {
                            cmd.CommandText = SQLCommands.sqlGetGraphicsConcepts;
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
                log.Error("GraphicsConceptsAccess Exception occurred: " + ex.Message);
                //log.Debug("GraphicsConceptsAccess Exception occurred", ex);
            }

            return dataTable;
        }
        public DataTable GetGraphicsConceptTypes()
        {
            DataTable dataTable = new DataTable();

            try
            {
                // Instantiate the connection
                using (SqlConnection connection = new SqlConnection(GraphicsDBConnectionString))
                {
                    // Create the command and set its properties
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                        {
                            cmd.CommandText = SQLCommands.sqlGetGraphicsConceptTypes;
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
                log.Error("GraphicsConceptsAccess Exception occurred: " + ex.Message);
                //log.Debug("GraphicsConceptsAccess Exception occurred", ex);
            }

            return dataTable;
        }

        #endregion
    }
}
