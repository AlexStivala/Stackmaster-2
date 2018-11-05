using System;
using System.Data;

namespace LogicLayer.CommonClasses
{
    using System.Data.SqlClient;

    using DataInterface.SQL;
    using LogicLayer.Collections;
    
    /// <summary>
    /// Class for functions related to time settings
    /// </summary>
    public static class TimeFunctions
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public static string ElectionsDBConnectionString { get; set; }
        #endregion

        #region Time Functions

        // Converts Time str in the format YYYYMMDDHHMMSS (i.e. poll closing, simulated time)  to datetime
        public static DateTime ConvertTimeStr(string dtStr)
        {
            string sec = "00";
            DateTime convertedTime = DateTime.MaxValue;
            if (dtStr.Length >= 12)
            {
                string yr = dtStr.Substring(0, 4);
                string mon = dtStr.Substring(4, 2);
                string day = dtStr.Substring(6, 2);
                string hr = dtStr.Substring(8, 2);
                string min = dtStr.Substring(10, 2);

                if (dtStr.Length >= 14)
                    sec = dtStr.Substring(12, 2);

                convertedTime = Convert.ToDateTime(mon + "/" + day + "/" + yr + " " + hr + ":" + min + ":" + sec);
            }

            return convertedTime;
        }


        // Get the simulated time from the DB
        public static string GetSimulatedTimeStr()
        {
            string simTimeStr = String.Empty;
            try
            {
                // Instantiate the connection
                using (SqlConnection sqlconn = new SqlConnection(ElectionsDBConnectionString))
                {
                    // Create the command and set its properties
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlconn.Open();
                        cmd.CommandText = SQLCommands.sqlGetSimTime;
                        cmd.Connection = sqlconn;
                        simTimeStr = (string)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("TimeFunctions Exception occurred: " + ex.Message);
                //log.Debug("TimeFunctions Exception occurred", ex);
            }

            return simTimeStr;

        }

        // Get the simulated time and converts to datetime
        public static DateTime GetSimulatedTime()
        {
            string simulatedTimeStr = GetSimulatedTimeStr();
            DateTime st = ConvertTimeStr(simulatedTimeStr);
            return st;
        }

        // Get the time to use based on simulated time setting
        public static DateTime GetTime()
        {
            DateTime referenceTime = DateTime.MaxValue;
            Boolean UseSimulatedTime = ApplicationSettingsFlagsCollection.UseSimulatedTime;

            if (UseSimulatedTime == true)
                referenceTime = TimeFunctions.GetSimulatedTime();
            else
                referenceTime = DateTime.Now;

            return referenceTime;
        }

        // Get the simulated time from the DB
        public static DateTime GetNextPollClosingTime(DateTime currentTime)
        {
            DateTime NextPollClosingTime = DateTime.MaxValue;
            try
            {
                // Instantiate the connection
                using (SqlConnection sqlconn = new SqlConnection(ElectionsDBConnectionString))
                {
                    // Create the command and set its properties
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        sqlconn.Open();
                        cmd.CommandText = SQLCommands.sqlGetNextPollClosingTimeTime;
                        cmd.Parameters.Add("@Use_Date", SqlDbType.SmallInt).Value = 1;
                        cmd.Parameters.Add("@CurrentDateTime", SqlDbType.DateTime).Value =  currentTime;
                        cmd.Connection = sqlconn;
                    
                        NextPollClosingTime = Convert.ToDateTime(cmd.ExecuteScalar());


                        }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("TimeFunctions Exception occurred: " + ex.Message);
                //log.Debug("TimeFunctions Exception occurred", ex);
            }

            return NextPollClosingTime;

        }


        #endregion
    }
    
}
