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
    public class ReferendumsCollection
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public BindingList<ReferendumModel> referendums;
        public string ElectionsDBConnectionString { get; set; }
        #endregion

        #region Public Methods
        // Constructor - instantiates list collection
        public ReferendumsCollection()
        {
            // Create list
            referendums = new BindingList<ReferendumModel>();
        }

        /// <summary>
        /// Get the Referendum list from the SQL DB; clears out existing collection first
        /// </summary>
        public BindingList<ReferendumModel> GetReferendumsCollection()
        {
            DataTable dataTable;

            // Clear out the current collection
            referendums.Clear();

            try
            {
                ReferendumAccess referendumAccess = new ReferendumAccess();
                referendumAccess.ElectionsDBConnectionString = ElectionsDBConnectionString;
                dataTable = referendumAccess.GetReferendums();

                foreach (DataRow row in dataTable.Rows)
                {
                        var newReferendum = new ReferendumModel() 
                        {

                            //Specific to referendums
                            race_ID = Convert.ToInt16(row["Race_ID"] ?? 0),
                            race_ElectionType = row["Race_ElectionType"].ToString() ?? "",
                            race_Description = row["Race_Description"].ToString() ?? "",
                            state_ID = Convert.ToInt16(row["State_ID"] ?? 0),
                            state_Abbv = row["State_Abbv"].ToString() ?? "",
                            state_Name = row["State_Name"].ToString() ?? "",
                            race_District = Convert.ToInt16(row["Race_District"] ?? 0),
                            race_OfficeCode = row["Race_OfficeCode"].ToString() ?? "",
                            race_WinnerCalled = Convert.ToBoolean(row["Race_WinnerCalled"] ?? 0),
                            race_WinnerCandidateID = Convert.ToInt32(row["Race_WinnerCandidateID"] ?? 0),

                        };

                        referendums.Add(newReferendum);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("ExitPollsCollection Exception occurred: " + ex.Message);
                //log.Debug("ExitPollsCollection Exception occurred", ex);
            }

            // Return 
            return referendums;
        }

        /// <summary>
        /// Get the specified referendum from the collection
        /// </summary>
        public ReferendumModel GetReferendum(BindingList<ReferendumModel> referendums, int itemIndex)
        {
            ReferendumModel referendum = null;
            try
            {
                referendum = referendums[itemIndex];
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("AvailableRacesCollection Exception occurred: " + ex.Message);
                //log.Debug("AvailableRacesCollection Exception occurred", ex);
            }

            return referendum;
        }
        #endregion
    }
}
