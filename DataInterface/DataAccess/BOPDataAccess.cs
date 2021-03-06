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

    public class BOPDataAccess
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public string ElectionsDBConnectionString { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to get the Balance of Power Data
        /// </summary>
        //public DataTable GetBOPData(string raceOffice, DateTime timeStr, int atStake)
        public DataTable GetBOPDataCurrent(string raceOffice, DateTime timeStr)
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
                            cmd.CommandText = SQLCommands.sqlGetBOPCurrent;
                            
                            cmd.Parameters.Add("@Race_Office", SqlDbType.Text).Value = raceOffice;
                            cmd.Parameters.Add("@timeStr", SqlDbType.DateTime).Value = timeStr;
                            cmd.Parameters.Add("@new", SqlDbType.Bit).Value = 0;


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

        //public DataTable GetBOPData(string raceOffice, DateTime timeStr, int atStake)
        public DataTable GetBOPDataNewGain(string raceOffice, DateTime timeStr)
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
                            
                            cmd.CommandText = SQLCommands.sqlGetBOPData;

                            cmd.Parameters.Add("@Race_Office", SqlDbType.Text).Value = raceOffice;
                            cmd.Parameters.Add("@timeStr", SqlDbType.DateTime).Value = timeStr;

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
