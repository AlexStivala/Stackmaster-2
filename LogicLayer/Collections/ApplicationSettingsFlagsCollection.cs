using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataInterface.DataModel;
using DataInterface.DataAccess;
using System.Data.SqlClient;
using System.ComponentModel;

namespace LogicLayer.Collections
{
    /// <summary>
    /// Class for operations related to the available races
    /// </summary>
    public class ApplicationSettingsFlagsCollection
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public BindingList<ApplicationSettingsFlagsModel> applicationFlags;
        public string ElectionsDBConnectionString { get; set; }
        public static Boolean UseSimulatedTime { get; set; }
        public static Boolean ShowRacesBeforePollClosing { get; set; }
        public static Boolean ShowTenthsofPercent { get; set; }
        public static Boolean PollClosingLockout { get; set; }
        #endregion

        #region Public Methods
        // Constructor - instantiates list collection
        public ApplicationSettingsFlagsCollection()
        {
            // Create list
            applicationFlags = new BindingList<ApplicationSettingsFlagsModel>();
        }

        /// <summary>
        /// Get the available list from the SQL DB; clears out existing collection first
        /// </summary>
        public BindingList<ApplicationSettingsFlagsModel> GetFlagsCollection()
        {
            DataTable dataTable;

            // Clear out the current collection
            applicationFlags.Clear();

            try
            {
                ApplicationSettingsFlagsAccess applicationSettingsFlagsAccess = new ApplicationSettingsFlagsAccess();
                applicationSettingsFlagsAccess.ElectionsDBConnectionString = ElectionsDBConnectionString;
                dataTable = applicationSettingsFlagsAccess.GetFlags();

                foreach (DataRow row in dataTable.Rows)
                {
                    var newFlags = new ApplicationSettingsFlagsModel()
                    {
                        PollClosingLockoutEnable = Convert.ToBoolean (row["PollClosingLockoutEnable"] ?? 0),
                        ZeroPrecinctsLockoutEnable = Convert.ToBoolean(row["ZeroPrecinctsLockoutEnable"] ?? 0),
                        ZeroPrecinctsLockoutThreshold = Convert.ToSingle(row["ZeroPrecinctsLockoutThreshold"] ?? 0.0),
                        UseExpectedVoteIn = Convert.ToBoolean(row["UseExpectedVoteIn"] ?? 0),
                        UseSimulatedElectionDayTime = Convert.ToBoolean(row["UseSimulatedElectionDayTime"] ?? 0),
                        UseAPCallsForTier2HouseRaces = Convert.ToBoolean(row["UseAPCallsForTier2HouseRaces"] ?? 0),
                        UseTenthsOfPercent = Convert.ToBoolean(row["UseTenthsOfPercent"] ?? 0),
                        OptionalFlag1 = Convert.ToBoolean(row["OptionalFlag1"] ?? 0),
                        OptionalFlag2 = Convert.ToBoolean(row["OptionalFlag2"] ?? 0),
                        OptionalFlag3 = Convert.ToBoolean(row["OptionalFlag3"] ?? 0),
                        OptionalFlag4 = Convert.ToBoolean(row["OptionalFlag4"] ?? 0),
                        OptionalFlag5 = Convert.ToBoolean(row["OptionalFlag5"] ?? 0),
                        OptionalParameter1 = Convert.ToSingle (row["OptionalParameter1"] ?? 0),
                        OptionalParameter2 = Convert.ToSingle (row["OptionalParameter2"] ?? 0),
                        OptionalParameter3 = Convert.ToSingle (row["OptionalParameter3"] ?? 0),
                        OptionalParameter4 = Convert.ToSingle (row["OptionalParameter4"] ?? 0),
                        OptionalParameter5 = Convert.ToSingle (row["OptionalParameter5"] ?? 0),
    
                    };
                    applicationFlags.Add(newFlags);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("ApplicationSettingsFlagsCollection Exception occurred: " + ex.Message);
                //log.Debug("ApplicationSettingsFlagsCollection Exception occurred", ex);
            }

            ApplicationSettingsFlagsModel flags = null;
            flags = applicationFlags[0];
            UseSimulatedTime = flags.UseSimulatedElectionDayTime;
            ShowRacesBeforePollClosing = flags.OptionalFlag1;
            ShowTenthsofPercent = flags.UseTenthsOfPercent;
            PollClosingLockout = flags.PollClosingLockoutEnable;
            

            // Return 
            return applicationFlags;
        }
        
        public Boolean GetSimTimeStatus()
        {
            return UseSimulatedTime;
        }


        #endregion
    }
}
