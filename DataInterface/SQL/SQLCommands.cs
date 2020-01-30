using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.SQL
{
    /// <summary>
    /// Class for static SQL command strings
    /// </summary>
    public static class SQLCommands
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // SQL strings for stack related functions
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Sql to get the list of available MSE stack element types
        /// </summary>
        public static readonly string sqlGetStackElementTypes = "SELECT * FROM MSE_Stack_Element_Types";

        /// <summary>
        /// Sql to get the top-level stack metadata
        /// </summary>
        //public static readonly string sqlGetStacksList = "SELECT * FROM MSE_Stacks WHERE StackType = @StackType";
        public static readonly string sqlGetStacksList = "SELECT * FROM Stacks WHERE StackType = @StackType";

        /// <summary>
        /// Sql to get the elements for a specified stack
        /// </summary>
        //public static readonly string sqlGetStackElements = "SELECT * FROM MSE_Stack_Elements WHERE fkey_StackID = @StackID";
        public static readonly string sqlGetStackElements = "SELECT * FROM Stack_Elements WHERE fkey_StackID = @StackID";

        /// <summary>
        /// Sql to save the top-level stack metadata
        /// </summary>
        //public static readonly string sqlSaveStack = "MSE_Stack_Save_Metadata " +
        public static readonly string sqlSaveStack = "Stack_Save_Metadata " +
            "@ixStackID, " + 
	        "@StackName, " + 
	        "@StackType, " + 
            "@ShowName, " +
            "@ConceptID, " +
            "@ConceptName, " +
	        "@Notes";

        /// <summary>
        /// Sql to delete the specified stack
        /// </summary>
        //public static readonly string sqlDeleteStack = "DELETE FROM MSE_Stacks WHERE ixStackID = @StackID";
        public static readonly string sqlDeleteStack = "DELETE FROM Stacks WHERE ixStackID = @StackID";

        /// <summary>
        /// Sql to check for an existing stack with the same name
        /// </summary>
        //public static readonly string sqlCheckIfStackExists = "SELECT * FROM MSE_Stacks WHERE StackName = @StackName";
        public static readonly string sqlCheckIfStackExists = "SELECT * FROM Stacks WHERE StackName = @StackName";

        /// <summary>
        /// Sql to save the stack elements - takes table-valued parameter
        /// </summary>
        //public static readonly string sqlSaveStackElements = "dbo.MSE_Stack_Add_Elements " +
        public static readonly string sqlSaveStackElements = "dbo.Stack_Add_Elements " +
            "@Stack_Elements_In, " + 
            "@StackID, " + 
            "@ClearStackBeforeAdding";

        /// <summary>
        /// Sql to save the stack elements
        /// </summary>
     //   public static readonly string sqlSaveStackElementsDiscrete = "MSE_Stack_Add_Single_Element " +

        public static readonly string sqlSaveStackElementsDiscrete = "Stack_Add_Single_Element " +
            "@fkey_StackID, " +
            "@Stack_Element_ID, " +
            "@Stack_Element_Type, " +
            "@Stack_Element_Data_Type, " +
            "@Stack_Element_Description, " +
            "@Stack_Element_TemplateID, " +
            "@Election_Type, " +
            "@Office_Code, " +
            "@State_Number, " +
            "@State_Mnemonic, " +
            "@State_Name, " +
            "@CD, " +
            "@County_Number, " +
            "@County_Name, " +
            "@Listbox_Description, " +
            "@Race_ID, " +
            "@Race_Office, " +
            "@Race_CandidateID_1, " +
            "@Race_CandidateID_2, " +
            "@Race_CandidateID_3, " +
            "@Race_CandidateID_4, " +
            "@Race_PollClosingTime, " +
            "@Race_UseAPRaceCall, " +
            "@VA_Data_ID," +
            "@VA_Title," +
            "@VA_Type," +
            "@VA_Map_Color," +
            "@VA_Map_ColorNum";

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Race list related functions
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Sql to get the list of available races
        /// </summary>\
        /// Aruti - 4/30 - changed from getVDSRaceList to getRaceList
        //public static readonly string sqlGetAvailableRacesList = "getVDSRaceList";
        public static readonly string sqlGetAvailableRacesList = "getFGERaceList";

        public static readonly string sqlGetCalledRaceList = "getVDSCalledRaceList";

        public static readonly string sqlGetTooCloseTooCallRaceList = "getVDSTooCloseToCallRaceList";
        
        public static readonly string sqlGetRacesByOffice = "getVDSRacesByOffice " +
            "@Race_Office ";

        public static readonly string sqlGetCalledRacesByOffice = "getVDSCalledRacesByOffice " +
            "@Race_Office ";

        public static readonly string sqlGetTooCloseTooCallRacesByOffice = "getVDSTooCloseToCallRacesByOffice " +
            "@Race_Office ";



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Race data related functions
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Sql to get the data for the specified race
        /// </summary>
        public static readonly string sqlGetRaceData = "getFGERacesByState " +
            "@State_Number, " +
            "@Race_Office, " +
            "@CD, " +
            "@Election_Type, " +
            "@Primary_Application_Code, " +
            "@candidateSelectEnable, " +
            "@candidateId1, " +
            "@candidateId2, " +
            "@candidateId3, " +
            "@candidateId4";

        /// <summary>
        /// Sql to get the data for the specified race
        /// </summary>
        public static readonly string sqlGetRaceDataPrimary = "getFGERacesByState_Primaries " +
            "@State_Number, " +
            "@Race_Office, " +
            "@CD, " +
            "@Election_Type, " +
            "@Party ";
            

        /// <summary>
        /// Sql to get the data for the specified race
        /// </summary>
        public static readonly string sqlGetRaceDataCounty = "getFE_RacesByCounty " +
            "@st, " +
            "@cnty, " +
            "@ofc, " +
            "@etype ";






        /// <summary>
        /// Sql to get the state metadata
        /// </summary>
        public static readonly string sqlGetStateMetadata = "SELECT * FROM VDS_ElectionStateInfo";


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // BOP data related functions
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Sql to get the BOP data
        /// </summary>
        public static readonly string sqlGetBOPCurrent = "getVDSBalanceOfPowerAuto " +
            "@Race_Office, " +
            "@TimeStr, " +
            "@New";

        public static readonly string sqlGetBOPDataNew_Gain = "getVDSBalanceOfPowerAutoGain " +
            "@Race_Office, " +
            "@TimeStr";

        public static readonly string sqlGetBOPData = "getVDSBalanceOfPowerAutoGain " +
            "@Race_Office, " +
            "@TimeStr";

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Exit Poll data related functions
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Sql to get the Exit Poll questions
        /// </summary>
        public static readonly string sqlGetExitPollQuestions = "getVDSXTabQuestions_P";

        public static readonly string sqlGetManualExitPollQuestions = "getVDSManualFNCQuestions";


        //public static readonly string sqlGetVoterAnalysisQuestions_FullScreen = "getFE_VoterAnalysisQuestions_FullScreen";
        public static readonly string sqlGetVoterAnalysisQuestions_FullScreen = "getFE_VoterAnalysisQuestions_FS_New_Test";
        //public static readonly string sqlGetVoterAnalysisQuestions_Ticker = "getFE_VoterAnalysisQuestions_Ticker";
        public static readonly string sqlGetVoterAnalysisQuestions_Ticker = "getFE_VoterAnalysisQuestions_Tkr_New";
        public static readonly string sqlGetVoterAnalysisQuestions_Map = "getFE_VoterAnalysisQuestions_Map";
        public static readonly string sqlGetVoterAnalysisQuestions_Manual ="getFE_VoterAnalysisQuestions_MAN_New";

        /// <summary>
        /// Sql to get the applicaton settings flags
        /// </summary>
        public static readonly string sqlGetFlags = "SELECT * FROM VDS_Flags";

        /// <summary>
        /// Sql to get the simulated time
        /// </summary>
        public static readonly string sqlGetSimTime = "SELECT value FROM VDS_BOP_Data where VDS_BOP_Data.name = 'CURRENT_TEST_TIME'";

        /// <summary>
        /// Sql to get the next poll closing time
        /// </summary>
        public static readonly string sqlGetNextPollClosingTimeTime = "getVDSNextPollClosingTime " +
                                                                      "@Use_Date, " +
                                                                      "@CurrentDateTime";
        /// <summary>
        /// Sql to get the referendums
        /// </summary>
        public static readonly string sqlGetReferendums = "SELECT * FROM VDS_ElectionReferendumInfo";

        /// <summary>
        /// Sql to get the referendums
        /// </summary>
        public static readonly string sqlGetReferendumData = "getVDSElectionReferendumInfo " +
                                                             "@State_Number, " +
                                                             "0, "+
                                                             "@Race_Office ";

        /// <summary>
        /// Sql to get the Base Exit Poll Data
        /// </summary>
        public static readonly string sqlGetBaseExitPollData = "getFGEBaseExitPollData " +
                                                               "@mxid, " +
                                                               "@state" +
                                                               "@office " +
                                                               "@jCode " +
                                                               "@subsetID " +
                                                               "@eType";

        /// <summary>
        /// Sql to get the Row Exit Poll Data
        /// </summary>
        public static readonly string sqlGetRowExitPollData = "getFGERowExitPollData " +
                                                               "@mxid, " +
                                                               "@state" +
                                                               "@office " +
                                                               "@jCode " +
                                                               "@row " +
                                                               "@subsetID " +
                                                               "@eType " +
                                                               "@priAppCode " +
                                                               "@ptySrtOrd";

        /// <summary>
        /// Sql to get the Manual Exit Poll Data
        /// </summary>
        public static readonly string sqlGetManualExitPollData = "getFGEManualExitPollData " +
                                                                 "@questionID";
        /// <summary>
        /// Sql to get a Manual Exit Poll Question
        /// </summary>
        public static readonly string sqlGetManualExitPollQuestion = "SELECT QuestionText FROM ManualFNCQuestions WHERE ixQuestionID = @questionID";

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Graphics Concept related functions
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Sql to get the Graphics Concepts Tab data
        /// </summary>
        public static readonly string sqlGetGraphicsConcepts = "SELECT * FROM MSE_Stack_Graphics_Concepts";

        /// <summary>
        /// Sql to get the Graphics Concepts Tab data
        /// </summary>
        public static readonly string sqlGetGraphicsConceptTypes = "SELECT DISTINCT ConceptID, ConceptName from MSE_Stack_Graphics_Concepts";

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Application log related functions
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Sql to make an entry in the applications log
        /// </summary>
        public static readonly string sqlSetFGEApplicationLogEntry = "setFGEApplicationLogEntry " +
                                                                       "@Application_Name, " +
                                                                       "@Application_Description, " +
                                                                       "@HostPC_Name, " +
                                                                       "@HostPC_IP_Address, " +
                                                                       "@Engine_Enabled_1, " +
                                                                       "@Engine_IP_Address_1, " +
                                                                       "@Engine_Enabled_2, " +
                                                                       "@Engine_IP_Address_2, " +
                                                                       "@Entry_Text, " +
                                                                       "@Application_Version, " +
                                                                       "@Application_ID, " + 
                                                                       "@Comments, " + 
                                                                       "@CurrentSystemTime";

        /// <summary>
        /// Sql to get the Manual Exit Poll Data
        /// </summary>
        public static readonly string sqlGetVoterAnalysisQuestionData = "SELECT question, answer, preface, variable_percent FROM FE_VoterAnalysisData_FullScreen WHERE VA_Data_Id = " +
                                                                        "@VA_Data_Id";

        public static readonly string sqlGetVoterAnalysisAnswerData = "SELECT question, answer, preface, name, id, party, result_percent FROM FE_VoterAnalysisData_FullScreen WHERE id > '0' AND VA_Data_Id = " +
                                                                        "@VA_Data_Id";

        public static readonly string sqlGetVoterAnalysisQuestionData_Tkr = "SELECT question, answer, preface, variable_percent FROM FE_VoterAnalysisData_Ticker WHERE VA_Data_Id = " +
                                                                        "@VA_Data_Id";

        public static readonly string sqlGetVoterAnalysisAnswerData_Tkr = "SELECT question, answer, preface, name, id, party, result_percent FROM FE_VoterAnalysisData_Ticker WHERE id > '0' AND VA_Data_Id = " +
                                                                        "@VA_Data_Id";

        //public static readonly string sqlGetVoterAnalysisDataFS = "getFE_VoterAnalysisData_Fullscreen " +
        public static readonly string sqlGetVoterAnalysisDataFS = "getFE_VoterAnalysisData_FS_New " +
                                                                        "@VA_Data_Id, " +
                                                                        "@r_type";

        public static readonly string sqlGetVoterAnalysisDataTkr = "getFE_VoterAnalysisData_Ticker " +
                                                                        "@VA_Data_Id, " +
                                                                        "@r_type";

        public static readonly string sqlGetVoterAnalysisManualData = "getFE_VoterAnalysisData_MAN_New " +
                                                                       "@VA_Data_Id ";

        /// <summary>
        /// Sql to get the counties for state
        /// </summary>
        public static readonly string sqlGetCountiesByState = "getVDSCountiesByState_P " +
                                                             "@State_Number, " +
                                                             "@Race_Office, " + 
                                                             "0, " +
                                                             "@Election_Type ";



    }
}
