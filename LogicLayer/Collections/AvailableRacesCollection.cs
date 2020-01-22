using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataInterface.DataModel;
using DataInterface.DataAccess;
using DataInterface.Enums;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using LogicLayer.CommonClasses;
using LogicLayer.Collections;

namespace LogicLayer.Collections
{
    using LogicLayer.CommonClasses;
    /// <summary>
    /// Class for operations related to the available races
    /// </summary>
    public class AvailableRacesCollection
    {
        
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public BindingList<AvailableRaceModel> availableRaces;
        public string ElectionsDBConnectionString { get; set; }
        // Define the collection used for storing state metadata for all 50 states + US
        public  StateMetadataCollection stateMetadataCollection;

        // Time for next poll closing
        public DateTime NextPollClosingTime;
        #endregion

        #region Public Methods
        // Constructor - instantiates list collection
        public AvailableRacesCollection()
        {
            // Create list
            availableRaces = new BindingList<AvailableRaceModel>();
        }

        /// <summary>
        /// Get the available list from the SQL DB; clears out existing collection first
        /// </summary>
        public BindingList<AvailableRaceModel> GetAvailableRaceCollection(bool isPrimary)
        {
            DataTable dataTable;

            // Clear out the current collection
            availableRaces.Clear();

            try
            {
                AvailableRaceAccess availableRaceAccess = new AvailableRaceAccess();
                availableRaceAccess.ElectionsDBConnectionString = ElectionsDBConnectionString;
                dataTable = availableRaceAccess.GetAvailableRaces(isPrimary);

                foreach (DataRow row in dataTable.Rows)
                {
                    var newAvailableRace = new AvailableRaceModel()
                    {
                        //Aruti - Changed to INT32 from INT16 to accomodate the new race IDS
                        Race_ID = Convert.ToInt32(row["Race_ID"] ?? 0),
                        Election_Type = row["Race_ElectionType"].ToString() ?? "",
                        Race_Description = row["Race_Description"].ToString() ?? "",
                        State_Number = Convert.ToInt16(row["State_ID"] ?? 0),
                        State_Mnemonic = row["State_Abbv"].ToString() ?? "",
                        State_Name = row["State_Name"].ToString() ?? "",
                        CD = Convert.ToInt16(row["Race_District"] ?? 0),
                        Race_Office = row["Race_OfficeCode"].ToString() ?? "",
                        Race_OfficeSortOrder = Convert.ToInt16(row["Race_OfficeSortOrder"] ?? 0),
                        Race_PollClosingTime = Convert.ToDateTime(row["Race_PollClosingTime_DateTime"] ?? 0),
                        Race_UseAPRaceCall = Convert.ToBoolean(row["Use_AP_Race_Call"] ?? 0),                       
                    };

                    if (newAvailableRace.Race_Office.Trim() == "S2")
                        newAvailableRace.Race_Description += " (S2)";

                    newAvailableRace.Party = newAvailableRace.Election_Type.Trim();
                    if (newAvailableRace.Election_Type.Trim() == "E")
                        newAvailableRace.Party = "D";
                    else if (newAvailableRace.Election_Type.Trim() == "S")
                        newAvailableRace.Party = "R";
                    
                    availableRaces.Add(newAvailableRace);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("AvailableRacesCollection Exception occurred: " + ex.Message);
                //log.Debug("AvailableRacesCollection Exception occurred", ex);
            }

            // Return 
            return availableRaces;
        }

        /// <summary>
        /// Get the filtered list from the SQL DB; clears out existing collection first
        /// </summary>
        public BindingList<AvailableRaceModel> GetFilteredRaceCollection(string raceOffice, Int16 filterCode, Int16 scfm, BindingList<StateMetadataModel> stateMetadata, bool isPrimary)
        {
            DataTable dataTable;
            //DateTime apCallTime;
            DateTime foxCallTime;
            DateTime raceCallTime;
            //string apCallTimeStr;
            DateTime TimeNow = TimeFunctions.GetTime();
            Boolean UseSimulatedTime = ApplicationSettingsFlagsCollection.UseSimulatedTime;
            TimeSpan jc15min = new TimeSpan(0, 15, 0);
            TimeSpan timeSinceCalled = new TimeSpan();

            // Clear out the current collection
            availableRaces.Clear();

            try
            {
                AvailableRaceAccess availableRaceAccess = new AvailableRaceAccess();
                availableRaceAccess.ElectionsDBConnectionString = ElectionsDBConnectionString;

                // Call the appropriate stored procedure based on the filter code and populate the dataTable
                switch (filterCode)
                {
                    case (Int16)BoardModes.Race_Board_Normal:
                        if (raceOffice == "A")
                            dataTable = availableRaceAccess.GetAvailableRaces(isPrimary);
                        else
                            dataTable = availableRaceAccess.GetAvailableRacesByOffice(raceOffice);
                        break;
                    case (Int16)BoardModes.Race_Board_Race_Called:
                        if (raceOffice == "A")
                            dataTable = availableRaceAccess.GetCalledRaces();
                        else
                            dataTable = availableRaceAccess.GetCalledRacesByOffice(raceOffice);
                        break;
                    case (Int16)BoardModes.Race_Board_Just_Called:
                        if (raceOffice == "A")
                            dataTable = availableRaceAccess.GetCalledRaces();
                        else
                            dataTable = availableRaceAccess.GetCalledRacesByOffice(raceOffice);
                        break;
                    case (Int16)BoardModes.Race_Board_To_Close_To_Call:
                        if (raceOffice == "A")
                            dataTable = availableRaceAccess.GetTooCloseToCallRaces();
                        else
                            dataTable = availableRaceAccess.GetTooCloseTooCallRacesByOffice(raceOffice);
                        break;
                    default:
                        dataTable = availableRaceAccess.GetAvailableRacesByOffice(raceOffice);
                        break;
                }

                foreach (DataRow row in dataTable.Rows)
                {
                    var newAvailableRace = new AvailableRaceModel()
                    {
                        Race_ID = Convert.ToInt16(row["Race_ID"] ?? 0),
                        Election_Type = row["Race_ElectionType"].ToString() ?? "",
                        Race_Description = row["Race_Description"].ToString() ?? "",
                        State_Number = Convert.ToInt16(row["State_ID"] ?? 0),
                        State_Mnemonic = row["State_Abbv"].ToString() ?? "",
                        State_Name = row["State_Name"].ToString() ?? "",
                        CD = Convert.ToInt16(row["Race_District"] ?? 0),
                        Race_Office = row["Race_OfficeCode"].ToString() ?? "",
                        Race_PollClosingTime =
                            TimeFunctions.ConvertTimeStr(row["Race_PollClosingTime"].ToString() ?? ""),
                        Race_UseAPRaceCall = Convert.ToBoolean(row["Use_AP_Race_Call"] ?? 0)
                    };

                    if (newAvailableRace.Race_Office.Trim() == "S2")
                        newAvailableRace.Race_Description += " (S2)";


                    Boolean isBGState = false;
                    Int16 stateId = Convert.ToInt16(row["State_ID"] ?? 0);

                    StateMetadataModel stateData = new StateMetadataModel();
                    StateMetadataCollection stateMetadataCollection = new StateMetadataCollection();

                    stateData = stateMetadataCollection.GetStateMetadata(stateMetadata, stateId);
                    isBGState = stateData.IsBattlegroundState;

                    DateTime StatePollClosingTime =
                        TimeFunctions.ConvertTimeStr(row["Race_PollClosingTime"].ToString() ?? "");

                    Boolean checkSpecialCaseFilterMode = false;


                    switch (filterCode)
                    {
                        // Normal
                        case (short)BoardModes.Race_Board_Normal:
                            checkSpecialCaseFilterMode = true;
                        break;

                       
                        
                        // Called
                        case (short)BoardModes.Race_Board_Race_Called:

                            if (ApplicationSettingsFlagsCollection.ShowRacesBeforePollClosing == true)
                                checkSpecialCaseFilterMode = true;
                            else if (TimeNow >= newAvailableRace.Race_PollClosingTime)
                                checkSpecialCaseFilterMode = true;
                        break;

                        
                        // Just Called
                        case (short)BoardModes.Race_Board_Just_Called:

                    
                            // code to support AP race call - currently not used

                            //apCallTimeStr = row["estTS"].ToString() ?? "";
                            //if (apCallTimeStr.Length >= 12)
                            //    apCallTime = TimeFunctions.ConvertTimeStr(apCallTimeStr);
                            //else
                            //    apCallTime = DateTime.MaxValue;

                            foxCallTime = Convert.ToDateTime(row["Race_WinnerCallTime"]);

                            //if (newAvailableRace.Race_UseAPRaceCall == true)
                            //    raceCallTime = apCallTime;
                            //else
                            //    raceCallTime = foxCallTime;

                            //if (raceCallTime < newAvailableRace.Race_PollClosingTime)
                            //    raceCallTime = newAvailableRace.Race_PollClosingTime;

                            raceCallTime = foxCallTime;
                            timeSinceCalled = TimeNow - raceCallTime;


                            if (ApplicationSettingsFlagsCollection.ShowRacesBeforePollClosing == true)
                                if (timeSinceCalled <= jc15min)
                                    checkSpecialCaseFilterMode = true;
                            else if (timeSinceCalled <= jc15min && TimeNow >= newAvailableRace.Race_PollClosingTime)
                                    checkSpecialCaseFilterMode = true;
                        break;


                        // Too Close To Call
                        case (short)BoardModes.Race_Board_To_Close_To_Call :
                            checkSpecialCaseFilterMode = true;
                        break;

                        default:
                            availableRaces.Add(newAvailableRace);
                        break;
                    }


                    if (checkSpecialCaseFilterMode)
                    switch (scfm)
                    {
                        // No Special filter
                        case (short)SpecialCaseFilterModes.None:
                            availableRaces.Add(newAvailableRace);
                            break;

                        // Battlground Only Mode and Is Battleground State
                        case (short)SpecialCaseFilterModes.Battleground_States_Only:
                            if (isBGState)
                                availableRaces.Add(newAvailableRace);
                            break;

                        // Next Poll Closing Mode and Next Poll Closing Time = State's Poll Closing Time
                        case (short)SpecialCaseFilterModes.Next_Poll_Closing_States_Only:
                            // Value defined above
                            NextPollClosingTime = TimeFunctions.GetNextPollClosingTime(TimeNow);
                            if (StatePollClosingTime == NextPollClosingTime)
                            {
                                availableRaces.Add(newAvailableRace);
                            }
                            break;

                        default:
                            availableRaces.Add(newAvailableRace);
                            break;
                    }

                }                
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("AvailableRacesCollection Exception occurred: " + ex.Message);
                //log.Debug("AvailableRacesCollection Exception occurred", ex);
            }

            // Return 
            return availableRaces;
        }


        /// <summary>
        /// Get the specified race from the collection
        /// </summary>
        public AvailableRaceModel GetRace(BindingList<AvailableRaceModel> AvailableRaces, int itemIndex)
        {
            AvailableRaceModel availableRace = null;
            try
            {
                availableRace = AvailableRaces[itemIndex];
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("AvailableRacesCollection Exception occurred: " + ex.Message);
                //log.Debug("AvailableRacesCollection Exception occurred", ex);
            }

            return availableRace;
        }
        #endregion
    }
}
