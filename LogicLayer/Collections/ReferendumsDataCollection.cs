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
    public class ReferendumsDataCollection
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public BindingList<ReferendumDataModel> referendumsData;
        public string ElectionsDBConnectionString { get; set; }
        #endregion

        #region Public Methods
        // Constructor - instantiates list collection
        public ReferendumsDataCollection()
        {
            // Create list
            referendumsData = new BindingList<ReferendumDataModel>();
        }

        /// <summary>
        /// Get the Referendum list from the SQL DB; clears out existing collection first
        /// </summary>
        public BindingList<ReferendumDataModel> GetReferendumsDataCollection(Int16 stateNum, string raceOffice)
        {
            DataTable dataTable;

            // Clear out the current collection
            referendumsData.Clear();

            try
            {
                ReferendumDataAccess referendumDataAccess = new ReferendumDataAccess();
                referendumDataAccess.ElectionsDBConnectionString = ElectionsDBConnectionString;
                dataTable = referendumDataAccess.GetReferendumData(stateNum, raceOffice);

                foreach (DataRow row in dataTable.Rows)
                {
                    var newReferendumsData = new ReferendumDataModel() 
                    {

                        //Specific to referendums
                        StateName = row["jName"].ToString().Trim() ?? "",
                        WinnerCalled = Convert.ToBoolean(row["Race_WinnerCalled"] ?? 0),
                        WinnerCandidateID = Convert.ToInt32(row["Race_WinnerCandidateID"] ?? 0),
                        MainTitle = row["Main_Title"].ToString() ?? "",
                        Detailtext = row["Detail_text"].ToString() ?? "",
                        Description = row["Description"].ToString() ?? "",
                        PropRefID = row["Prop_Ref_ID"].ToString() ?? "",
                        VoteCount = Convert.ToInt32(row["cVote"] ?? 0),
                        TotalVotes = Convert.ToInt32(row["voteSum"] ?? 0),
                        CandLastName = row["candLastName"].ToString() ?? "",
                        cStat = row["cStat"].ToString() ?? "",
                        PrecinctsReporting = Convert.ToInt32(row["pctsRep"] ?? 0),
                        TotalPrecincts = Convert.ToInt32(row["totPcts"] ?? 0),
        
                    };

                    referendumsData.Add(newReferendumsData);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("ExitPollsCollection Exception occurred: " + ex.Message);
                //log.Debug("ExitPollsCollection Exception occurred", ex);
            }

            // Return 
            return referendumsData;
        }

        #endregion
    }
}
