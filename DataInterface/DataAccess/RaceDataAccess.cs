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
    /// Class for handling database access to data for a specified race
    /// </summary>
    public class RaceDataAccess
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public string ElectionsDBConnectionString { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to get race data from the Elections SQL DB and pass it back to the logic layer as a DataTable
        /// </summary>
        public DataTable GetRaceData(string electionMode, Int16 stateNumber, string raceOffice, Int16 cd, string electionType,
            bool candidateSelectEnable, int candidateId1, int candidateId2, int candidateId3, int candidateId4)
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
                            if (electionMode == "Primary")
                                cmd.CommandText = SQLCommands.sqlGetRaceDataPrimary;
                            else
                                cmd.CommandText = SQLCommands.sqlGetRaceData;

                            cmd.Parameters.Add("@State_Number", SqlDbType.SmallInt).Value = stateNumber;
                            cmd.Parameters.Add("@Race_Office", SqlDbType.Text).Value = raceOffice;
                            cmd.Parameters.Add("@CD", SqlDbType.SmallInt).Value = cd;
                            cmd.Parameters.Add("@Election_Type", SqlDbType.Text).Value = electionType;
                            cmd.Parameters.Add("@Primary_Application_Code", SqlDbType.Text).Value = "B";
                            cmd.Parameters.Add("@candidateSelectEnable", SqlDbType.Bit).Value = candidateSelectEnable;
                            cmd.Parameters.Add("@candidateId1", SqlDbType.Int).Value = candidateId1;
                            cmd.Parameters.Add("@candidateId2", SqlDbType.Int).Value = candidateId2;
                            cmd.Parameters.Add("@candidateId3", SqlDbType.Int).Value = candidateId3;
                            cmd.Parameters.Add("@candidateId4", SqlDbType.Int).Value = candidateId4;
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
                log.Error("RaceDataAccess Exception occurred: " + ex.Message);
                //log.Debug("RaceDataAccess Exception occurred", ex);
            }
            return dataTable;
        }

        public DataTable GetRaceDataCounty(string stCode, Int16 cnty, string raceOffice, string electionType)
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
                            cmd.CommandText = SQLCommands.sqlGetRaceData;
                            cmd.Parameters.Add("@stateAbbv", SqlDbType.Text).Value = stCode;
                            cmd.Parameters.Add("@cnty", SqlDbType.SmallInt).Value = cnty;
                            cmd.Parameters.Add("@Race_Office", SqlDbType.Text).Value = raceOffice;
                            cmd.Parameters.Add("@Election_Type", SqlDbType.Text).Value = electionType;
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
                log.Error("RaceDataAccess Exception occurred: " + ex.Message);
                //log.Debug("RaceDataAccess Exception occurred", ex);
            }

            return dataTable;
        }
        #endregion
    }
}
