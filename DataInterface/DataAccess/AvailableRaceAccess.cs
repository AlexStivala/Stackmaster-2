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

    public class AvailableRaceAccess
    {
        #region Logger instantiation - uses reflection to get module name

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Properties and Members

        public string ElectionsDBConnectionString { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to get the list of available races for the current election event
        /// </summary>
        public DataTable GetAvailableRaces()
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
                            cmd.CommandText = SQLCommands.sqlGetAvailableRacesList;
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
                log.Error("AvailableRaceAccess Exception occurred: " + ex.Message);
                //log.Debug("AvailableRaceAccess Exception occurred", ex);
            }

            return dataTable;
        }

        public DataTable GetCalledRaces()
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
                            cmd.CommandText = SQLCommands.sqlGetCalledRaceList;
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
                log.Error("AvailableRaceAccess Exception occurred: " + ex.Message);
                //log.Debug("AvailableRaceAccess Exception occurred", ex);
            }

            return dataTable;
        }

        public DataTable GetTooCloseToCallRaces()
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
                            cmd.CommandText = SQLCommands.sqlGetTooCloseTooCallRaceList;
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
                log.Error("AvailableRaceAccess Exception occurred: " + ex.Message);
                //log.Debug("AvailableRaceAccess Exception occurred", ex);
            }

            return dataTable;
        }

        public DataTable GetAvailableRacesByOffice(string ofc)
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

                            cmd.CommandText = SQLCommands.sqlGetRacesByOffice;
                            cmd.Parameters.Add("@Race_Office", SqlDbType.Text).Value = ofc;
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
                log.Error("AvailableRaceAccess Exception occurred: " + ex.Message);
                //log.Debug("AvailableRaceAccess Exception occurred", ex);
            }

            return dataTable;
        }

        public DataTable GetCalledRacesByOffice(string ofc)
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

                            cmd.CommandText = SQLCommands.sqlGetCalledRacesByOffice;
                            cmd.Parameters.Add("@Race_Office", SqlDbType.Text).Value = ofc;
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
                log.Error("AvailableRaceAccess Exception occurred: " + ex.Message);
                //log.Debug("AvailableRaceAccess Exception occurred", ex);
            }

            return dataTable;
        }

        public DataTable GetTooCloseTooCallRacesByOffice(string ofc)
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

                            cmd.CommandText = SQLCommands.sqlGetTooCloseTooCallRacesByOffice;
                            cmd.Parameters.Add("@Race_Office", SqlDbType.Text).Value = ofc;
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
                log.Error("AvailableRaceAccess Exception occurred: " + ex.Message);
                //log.Debug("AvailableRaceAccess Exception occurred", ex);
            }

            return dataTable;
        }
        
        #endregion
    }
}
