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
    /// Class for operations related to state metadata
    /// </summary>
    public class StateMetadataCollection
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public BindingList<StateMetadataModel> stateMetadata;
        public string ElectionsDBConnectionString { get; set; }
        #endregion

        #region Public Methods
        // Constructor - instantiates list collection
        public StateMetadataCollection()
        {
            // Create list
            stateMetadata = new BindingList<StateMetadataModel>();
        }

        /// <summary>
        /// Get the state metadata from the elections database
        /// </summary>
        public BindingList<StateMetadataModel> GetStateMetadataCollection()
        {
            DataTable dataTable;

            // Clear out the current collection
            stateMetadata.Clear();

            try
            {
                StateMetadataAccess stateMetadataAccess = new StateMetadataAccess();
                stateMetadataAccess.ElectionsDBConnectionString = ElectionsDBConnectionString;
                dataTable = stateMetadataAccess.GetStateMetadata();

                foreach (DataRow row in dataTable.Rows)
                {
                    var newState = new StateMetadataModel()
                    {
                        State_ID = Convert.ToInt16(row["State_ID"] ?? 0),
                        State_Abbv = row["State_Abbv"].ToString() ?? "",
                        State_Name = row["State_Name"].ToString() ?? "",
                        IsBattlegroundState = Convert.ToBoolean(row["IsBattlegroundState"] ?? 0),
                        IsAtLargeHouseState = Convert.ToBoolean(row["IsAtLargeHouseState"] ?? 0),
                        State_PollClosingTime_DateTime = Convert.ToDateTime(row["State_PollClosingTime_DateTime"] ?? 0),
                    };
                    stateMetadata.Add(newState);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("StateMetadataCollection Exception occurred: " + ex.Message);
                //log.Debug("StateMetadataCollection Exception occurred", ex);
            }

            // Return 
            return stateMetadata;
        }

        /// <summary>
        /// Get data for the specified state from the collection
        /// </summary>
        public StateMetadataModel  GetStateMetadata(BindingList<StateMetadataModel> stateMetadata, Int16 stateID)
        {
            StateMetadataModel stateInfo = null;

            try
            {
                if (stateMetadata.Count > 0)
                {
                    for (int i = 0; i < stateMetadata.Count; ++i)
                    {
                        if (stateMetadata[i].State_ID == stateID)
                        {
                            stateInfo = stateMetadata[i];
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("StateMetadataCollection Exception occurred: " + ex.Message);
                //log.Debug("StateMetadataCollection Exception occurred", ex);
            }

            return stateInfo;
        }

        #endregion
    }
}
