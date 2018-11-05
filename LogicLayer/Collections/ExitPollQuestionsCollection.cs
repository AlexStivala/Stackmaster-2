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
    public class ExitPollQuestionsCollection
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public BindingList<ExitPollQuestionsModel> exitPollQuestions;
        public string ElectionsDBConnectionString { get; set; }
        #endregion

        #region Public Methods
        // Constructor - instantiates list collection
        public ExitPollQuestionsCollection()
        {
            // Create list
            exitPollQuestions = new BindingList<ExitPollQuestionsModel>();
        }

        /// <summary>
        /// Get the Exit Poll Questions list from the SQL DB; clears out existing collection first
        /// </summary>
        public BindingList<ExitPollQuestionsModel> GetExitPollQuestionsCollection(Boolean manual)
        {
            DataTable dataTable;

            // Clear out the current collection
            exitPollQuestions.Clear();

            try
            {
                ExitPollAccess exitPollAccess = new ExitPollAccess();
                exitPollAccess.ElectionsDBConnectionString = ElectionsDBConnectionString;
                dataTable = exitPollAccess.GetExitPollQuestions(manual);

                foreach (DataRow row in dataTable.Rows)
                {
                    if (manual == false)
                    {
                        var newExitPollQuestion = new ExitPollQuestionsModel()
                        {

                            //Specific to exit polls
                            mxID = row["mxID"].ToString() ?? "",
                            fullMxLabel = row["fullMxLabel"].ToString() ?? "",
                            shortMxLabel = row["shortMxLabel"].ToString() ?? "",
                            FNCMxLabel = row["FNCMxLabel"].ToString() ?? "",
                            stateNum = Convert.ToInt16(row["st"] ?? 0),
                            state = row["State"].ToString() ?? "",
                            stateName = row["StateName"].ToString() ?? "",
                            shortName = row["ShortName"].ToString() ?? "",
                            electionType = row["ElectionType"].ToString() ?? "",
                            office = row["Office"].ToString() ?? "",
                            CD = Convert.ToInt16(row["CD"] ?? 0),
                            numRows = Convert.ToInt16(row["Rows"] ?? 0),
                            numColumns = Convert.ToInt16(row["Columns"] ?? 0),
                            baseQuestion = Convert.ToBoolean(row["BaseOnAir"] ?? 0),
                            rowQuestion = Convert.ToBoolean(row["RowOnAir"] ?? 0),
                            rowNum = Convert.ToInt16(row["Row"] ?? 0),
                            rowLabel = row["RowLabel"].ToString() ?? "",
                            rowText = row["RowText"].ToString() ?? "",
                            FNCRowQuestion = row["FNCRowQuestion"].ToString() ?? "",
                            subsetID = Convert.ToInt32(row["SubsetID"] ?? 0),
                            subsetName = row["SubsetName"].ToString() ?? "",
                            suffix = "",
                            headerText1 = "",
                            headerText2 = "",

                        };


                        newExitPollQuestion.shortMxLabel = newExitPollQuestion.shortMxLabel.Trim();

                        if (newExitPollQuestion.CD != 0)
                            newExitPollQuestion.stateOffice = newExitPollQuestion.stateOffice +
                                                              Convert.ToString(newExitPollQuestion.CD);

                        if (newExitPollQuestion.numRows >= 2 && newExitPollQuestion.rowNum == 1 &&
                            newExitPollQuestion.baseQuestion)
                        {
                            if (newExitPollQuestion.mxID.Length == 2)
                                newExitPollQuestion.mxID = "00" + newExitPollQuestion.mxID;
                            else if (newExitPollQuestion.mxID.Length == 3)
                                newExitPollQuestion.mxID = "0" + newExitPollQuestion.mxID;

                            newExitPollQuestion.questionType = "B  ";
                        }
                        else
                        {
                            string rowNumStr = Convert.ToString(newExitPollQuestion.rowNum);
                            newExitPollQuestion.questionType = "R-" + rowNumStr;
                            newExitPollQuestion.mxID = newExitPollQuestion.mxID + rowNumStr;
                        }

                        newExitPollQuestion.stateOffice = newExitPollQuestion.mxID + " " + newExitPollQuestion.state +
                                                          " " + newExitPollQuestion.office;
                        newExitPollQuestion.listBoxDescription = newExitPollQuestion.questionType + " " +
                                                                 newExitPollQuestion.stateOffice;

                        newExitPollQuestion.listBoxDescription = newExitPollQuestion.listBoxDescription + " " +
                                                                 newExitPollQuestion.shortMxLabel + " " +
                                                                 newExitPollQuestion.fullMxLabel + " " +
                                                                 newExitPollQuestion.rowLabel;

                        exitPollQuestions.Add(newExitPollQuestion);
                    }
                    else
                    {
                            var newExitPollQuestion = new ExitPollQuestionsModel()
                        {

                            //Specific to exit polls
                            mxID = row["ixQuestionID"].ToString() ?? "",
                            fullMxLabel = row["QuestionText"].ToString() ?? "",
                            shortMxLabel = row["Mnemonic"].ToString() ?? "",
                            //FNCMxLabel = row["FNCMxLabel"].ToString() ?? "",
                            stateNum = Convert.ToInt16(row["st"] ?? 0),
                            state = row["State"].ToString() ?? "",
                            stateName = row["StateName"].ToString() ?? "",
                            shortName = row["ShortName"].ToString() ?? "",
                            electionType =  "G",
                            office = row["Office"].ToString() ?? "",
                            CD = Convert.ToInt16(row["CD"] ?? 0),
                            numRows = Convert.ToInt16(row["Rows"] ?? 0),

                            //numColumns = Convert.ToInt16(row["Columns"] ?? 0),
                            //baseQuestion = Convert.ToBoolean(row["BaseOnAir"] ?? 0),
                            //rowQuestion = Convert.ToBoolean(row["RowOnAir"] ?? 0),
                            //rowNum = Convert.ToInt16(row["Row"] ?? 0),
                            //rowLabel = row["RowLabel"].ToString() ?? "",
                            //rowText = row["RowText"].ToString() ?? "",
                            FNCRowQuestion = row["QuestionText"].ToString() ?? "",

                            subsetID = 0,
                            subsetName =  "",
                            suffix = row["Suffix"].ToString() ?? "",
                            headerText1 = row["QuestionText"].ToString() ?? "",
                            headerText2 = "",

                        };


                        newExitPollQuestion.shortMxLabel = newExitPollQuestion.shortMxLabel.Trim();

                        if (newExitPollQuestion.CD != 0)
                            newExitPollQuestion.stateOffice = newExitPollQuestion.stateOffice +
                                                              Convert.ToString(newExitPollQuestion.CD);


                        newExitPollQuestion.questionType = "M  ";
                        

                        newExitPollQuestion.stateOffice = newExitPollQuestion.mxID + " " + newExitPollQuestion.state +
                                                          " " + newExitPollQuestion.office;
                        newExitPollQuestion.listBoxDescription = newExitPollQuestion.questionType + " " +
                                                                 newExitPollQuestion.stateOffice;

                        newExitPollQuestion.listBoxDescription = newExitPollQuestion.listBoxDescription + " " +
                                                                 newExitPollQuestion.shortMxLabel + " " +
                                                                 newExitPollQuestion.fullMxLabel + " " +
                                                                 newExitPollQuestion.rowLabel;


                        // Base Question
                        //ElectionTypeCode := ElectionTypeCode[1];
                        //if (Length(mxIDStr) = 2) then mxIDStr := '00' + mxIDStr
                        //else if (Length(mxIDStr) = 3) then mxIDStr := '0' + mxIDStr;
                        //  mxIDStr := mxIDStr + '0';

                        //if SubID > 0 then
                        //  SubsetStr := '-' + SubName
                        //else
                        //  SubsetStr := '';


                        //Listbox_Description := mxIDStr + '-' + State + '-' + Trim(Office)  + '-' + ElectionTypeCode + '-FS  (' +
                        //Trim(ShortMxLabel) + SubsetStr + '-Base ' + BaseMxLabelStr + ')';


                        // Row Question
                        //if (Length(mxIDStr) = 2) then mxIDStr := '00' + mxIDStr
                        //else if (Length(mxIDStr) = 3) then mxIDStr := '0' + mxIDStr;

                        //mxIDStr := mxIDStr + IntToStr(RowNum);

                        //Listbox_Description := mxIDStr + '-' + State + '-' + Trim(Office) + '-' + ElectionTypeCode +
                        //'-FS  (' + Trim(ShortMxLabel) + SubsetStr + '-R' + IntToStr(RowNum) +  ' ' +
                        //RowQuestionStr + ')';





                        exitPollQuestions.Add(newExitPollQuestion);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("ExitPollsCollection Exception occurred: " + ex.Message);
                //log.Debug("ExitPollsCollection Exception occurred", ex);
            }

            // Return 
            return exitPollQuestions;
        }

        /// <summary>
        /// Get the specified exit poll from the collection
        /// </summary>
        public ExitPollQuestionsModel GetExitPoll(BindingList<ExitPollQuestionsModel> exitPollQuestions, int itemIndex)
        {
            ExitPollQuestionsModel ExitPoll = null;
            try
            {
                ExitPoll = exitPollQuestions[itemIndex];
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("AvailableRacesCollection Exception occurred: " + ex.Message);
                //log.Debug("AvailableRacesCollection Exception occurred", ex);
            }

            return ExitPoll;
        }
        #endregion
    }
}
