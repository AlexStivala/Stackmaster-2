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
    public class StackElementsCollection
    {
        #region Logger instantiation - uses reflection to get module name
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Properties and Members
        public BindingList<StackElementModel> stackElements;
        public string MainDBConnectionString { get; set; }

        private int collectionCount { get; set; }
        public int CollectionCount
        {
            get { return this.collectionCount; }
            set { this.collectionCount = value; }
        }
        #endregion

        #region Public Methods
        // Constructor - instantiates list collection
        public StackElementsCollection()
        {
            // Create list
            stackElements = new BindingList<StackElementModel>();
        }

        /// <summary>
        /// Get the MSE Stack elements list from the SQL DB; clears out existing collection first
        /// </summary>
        public BindingList<StackElementModel> GetStackElementsCollection(Double stackID)
        {
            DataTable dataTable;

            // Clear out the current collection
            stackElements.Clear();

            try
            {
                StackElementAccess stackElementAccess = new StackElementAccess();
                stackElementAccess.MainDBConnectionString = MainDBConnectionString;
                dataTable = stackElementAccess.GetStackElements(stackID);

                foreach (DataRow row in dataTable.Rows)
                {
                    var newStackElement = new StackElementModel()
                    {
                        // Stack metadata
                        fkey_StackID = Convert.ToDouble(row["fkey_StackID"] ?? 0),
                        Stack_Element_ID = Convert.ToInt64(row["Stack_Element_ID"] ?? 0),
                        Stack_Element_Type = Convert.ToInt16(row["Stack_Element_Type"] ?? 0),
                        Stack_Element_Data_Type = Convert.ToInt16(row["Stack_Element_Data_Type"] ?? 0),
                        Stack_Element_Description = row["Stack_Element_Description"].ToString() ?? "",
                        Stack_Element_TemplateID = row["Stack_Element_TemplateID"].ToString() ?? "",

                        // General race/election info
                        Election_Type = row["Election_Type"].ToString() ?? "",
                        Office_Code = row["Office_Code"].ToString() ?? "",
                        State_Number = Convert.ToInt16(row["State_Number"] ?? 0),
                        State_Mnemonic = row["State_Mnemonic"].ToString() ?? "",
                        State_Name = row["State_Name"].ToString() ?? "",
                        CD = Convert.ToInt16(row["CD"] ?? 0),
                        County_Number = Convert.ToInt32(row["County_Number"] ?? 0),
                        County_Name = row["County_Name"].ToString() ?? "",
                        Listbox_Description = row["Listbox_Description"].ToString() ?? "",

                        // Specific to race boards
                        Race_ID = Convert.ToInt32(row["Race_ID"] ?? 0),
                        Race_Office = row["Race_Office"].ToString() ?? "",
                        Race_CandidateID_1 = Convert.ToInt32(row["Race_CandidateID_1"] ?? 0),
                        Race_CandidateID_2 = Convert.ToInt32(row["Race_CandidateID_2"] ?? 0),
                        Race_CandidateID_3 = Convert.ToInt32(row["Race_CandidateID_3"] ?? 0),
                        Race_CandidateID_4 = Convert.ToInt32(row["Race_CandidateID_4"] ?? 0),
                        Race_CandidateID_5 = Convert.ToInt32(row["Race_CandidateID_5"] ?? 0),
                        Race_PollClosingTime = Convert.ToDateTime(row["Race_PollClosingTime"] ?? 0),
                        Race_UseAPRaceCall = Convert.ToBoolean(row["Race_UseAPRaceCall"] ?? 0),

                        // Specific to Voter Analysis
                        VA_Data_ID = row["VA_Data_ID"].ToString() ?? "",
                        VA_Title = row["VA_Title"].ToString() ?? "",
                        VA_Type = row["VA_Type"].ToString() ?? "",
                        VA_Map_Color = row["VA_Map_Color"].ToString() ?? "",
                        VA_Map_ColorNum = Convert.ToInt32(row["VA_Map_ColorNum"] ?? 0),
                    };
                    stackElements.Add(newStackElement);

                    this.collectionCount = stackElements.Count;
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("StackElementsCollection Exception occurred: " + ex.Message);
                //log.Debug("StackElementsCollection Exception occurred", ex);
            }

            // Return 
            return stackElements;
        }

        /// <summary>
        /// Convert the stack elements collection to a data table object and save it out
        /// </summary>
        public void SaveStackElementsCollection(Double stackID, Boolean clearStackBeforeAdding)
        {
            try
            {
                log.Debug($"Saving Stack Elements");

                if (stackElements.Count > 0)
                {
                    DataTable dataTable = new DataTable();

                    // Stack metadata
                    dataTable.Columns.Add("fkey_StackID", typeof(Double));
                    dataTable.Columns.Add("Stack_Element_ID", typeof(Int64));
                    dataTable.Columns.Add("Stack_Element_Type", typeof(Int16));
                    dataTable.Columns.Add("Stack_Element_Data_Type", typeof(Int16));
                    dataTable.Columns.Add("Stack_Element_Description", typeof(String));
                    dataTable.Columns.Add("Stack_Element_TemplateID", typeof(String));
                    // General race/election info
                    dataTable.Columns.Add("Election_Type", typeof(String));
                    dataTable.Columns.Add("Office_Code", typeof(String));
                    dataTable.Columns.Add("State_Number", typeof(Int32));
                    dataTable.Columns.Add("State_Mnemonic", typeof(String));
                    dataTable.Columns.Add("State_Name", typeof(String));
                    dataTable.Columns.Add("CD", typeof(Int16));
                    dataTable.Columns.Add("County_Number", typeof(Int32));
                    dataTable.Columns.Add("County_Name", typeof(String));
                    dataTable.Columns.Add("Listbox_Description", typeof(String));
                    // Specific to race boards
                    dataTable.Columns.Add("Race_ID", typeof(Int32));
                    dataTable.Columns.Add("Race_Office", typeof(String));
                    dataTable.Columns.Add("Race_CandidateID_1", typeof(Int32));
                    dataTable.Columns.Add("Race_CandidateID_2", typeof(Int32));
                    dataTable.Columns.Add("Race_CandidateID_3", typeof(Int32));
                    dataTable.Columns.Add("Race_CandidateID_4", typeof(Int32));
                    dataTable.Columns.Add("Race_CandidateID_5", typeof(Int32));
                    dataTable.Columns.Add("Race_PollClosingTime", typeof(DateTime));
                    dataTable.Columns.Add("Race_UseAPRaceCall", typeof(Boolean));
                    //Specific to exit polls
                    dataTable.Columns.Add("VA_Data_ID", typeof(String));
                    dataTable.Columns.Add("VA_Title", typeof(String));
                    dataTable.Columns.Add("VA_Type", typeof(String));
                    dataTable.Columns.Add("VA_Map_Color", typeof(String));
                    dataTable.Columns.Add("VA_Map_ColorNum", typeof(Int32));

                    for (int i = 0; i < stackElements.Count; i++)
                    {
                        DataRow stackElement = dataTable.NewRow();
                        // Stack metadata
                        stackElement["fkey_StackID"] = stackID; //Use passed in stack ID for all elements
                        stackElement["Stack_Element_ID"] = i; //Re-sequence element ID
                        stackElement["Stack_Element_Type"] = stackElements[i].Stack_Element_Type;
                        stackElement["Stack_Element_Data_Type"] = stackElements[i].Stack_Element_Data_Type;
                        stackElement["Stack_Element_Description"] = stackElements[i].Stack_Element_Description;
                        stackElement["Stack_Element_TemplateID"] = stackElements[i].Stack_Element_TemplateID;

                        // General race/election info
                        stackElement["Election_Type"] = stackElements[i].Election_Type;
                        stackElement["Office_Code"] = stackElements[i].Office_Code;
                        stackElement["State_Number"] = stackElements[i].State_Number;
                        stackElement["State_Mnemonic"] = stackElements[i].State_Mnemonic;
                        stackElement["State_Name"] = stackElements[i].State_Name;
                        stackElement["CD"] = stackElements[i].CD;
                        stackElement["County_Number"] = stackElements[i].County_Number;
                        stackElement["County_Name"] = stackElements[i].County_Name;
                        stackElement["Listbox_Description"] = stackElements[i].Listbox_Description;

                        // Specific to race boards
                        stackElement["Race_ID"] = stackElements[i].Race_ID;
                        stackElement["Race_Office"] = stackElements[i].Race_Office;
                        stackElement["Race_CandidateID_1"] = stackElements[i].Race_CandidateID_1;
                        stackElement["Race_CandidateID_2"] = stackElements[i].Race_CandidateID_2;
                        stackElement["Race_CandidateID_3"] = stackElements[i].Race_CandidateID_3;
                        stackElement["Race_CandidateID_4"] = stackElements[i].Race_CandidateID_4;
                        stackElement["Race_CandidateID_5"] = stackElements[i].Race_CandidateID_5;
                        stackElement["Race_PollClosingTime"] = stackElements[i].Race_PollClosingTime;
                        stackElement["Race_UseAPRaceCall"] = stackElements[i].Race_UseAPRaceCall;

                        //Specific to exit polls
                        
                        stackElement["VA_Data_ID"] = stackElements[i].VA_Data_ID;
                        stackElement["VA_Title"] = stackElements[i].VA_Title;
                        stackElement["VA_Type"] = stackElements[i].VA_Type;
                        stackElement["VA_Map_Color"] = stackElements[i].VA_Map_Color;
                        stackElement["VA_Map_ColorNum"] = stackElements[i].VA_Map_ColorNum;

                        dataTable.Rows.Add(stackElement);
                    }
                    //Instantiate the data access object to save out the dataTable
                    StackElementAccess stackElementAccess = new StackElementAccess();
                    stackElementAccess.MainDBConnectionString = MainDBConnectionString;
                    log.Debug($"Save Stack Elements DBconn: {MainDBConnectionString}");
                    // Call method to save each element
                    stackElementAccess.SaveStackElements(dataTable, stackID, clearStackBeforeAdding);
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("StackElementsCollection Exception occurred: " + ex.Message);
                //log.Debug("StackElementsCollection Exception occurred", ex);
            }
        }

        /// <summary>
        /// Delete the specified element (by index) from the stack
        /// </summary>
        public void DeleteStackElement(Int16 elementIndex)
        {
            try
            {
                if (stackElements.Count > 0)
                {
                    stackElements.RemoveAt(elementIndex);
                    this.collectionCount = stackElements.Count;
                }
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("StackElementsCollection Exception occurred: " + ex.Message);
                //log.Debug("StackElementsCollection Exception occurred", ex);
            }
        }

        /// <summary>
        /// Append an element to the stack collection
        /// </summary>
        public void AppendStackElement(StackElementModel stackElement)
        {
            try
            {
                stackElements.Add(stackElement);
                this.collectionCount = stackElements.Count;
            }
            catch (Exception ex)
            {
                // Log error
                log.Error("StackElementsCollection Exception occurred: " + ex.Message);
                //log.Debug("StackElementsCollection Exception occurred", ex);
            }
        }

         /// <summary>
         /// Insert an element into the stack collection at the specified location
         /// </summary>
         public void AppendStackElement(Int16 insertPoint, StackElementModel stackElement)
         {
             try
             {
                 stackElements.Insert(insertPoint, stackElement);
                 this.collectionCount = stackElements.Count;
             }
             catch (Exception ex)
             {
                 // Log error
                 log.Error("StackElementsCollection Exception occurred: " + ex.Message);
                 //log.Debug("StackElementsCollection Exception occurred", ex);
             }
         }

         /// <summary>
         /// Move the specified element down in the list
         /// </summary>
         public void MoveStackElementDown(Int16 itemIndex)
         {
             try
             {
                 if (itemIndex < stackElements.Count - 1)
                 {
                     var item = stackElements[itemIndex];
                     stackElements.RemoveAt(itemIndex);
                     stackElements.Insert(itemIndex + 1, item);
                 }
             }
             catch (Exception ex)
             {
                 // Log error
                 log.Error("StackElementsCollection Exception occurred: " + ex.Message);
                 //log.Debug("StackElementsCollection Exception occurred", ex);
             }
         }

         /// <summary>
         /// Move the specified element up in the list
         /// </summary>
         public void MoveStackElementUp(Int16 itemIndex)
         {
             try
             {
                 if (itemIndex > 0)
                 {
                     var item = stackElements[itemIndex];
                     stackElements.RemoveAt(itemIndex);
                     stackElements.Insert(itemIndex - 1, item);
                 }
             }
             catch (Exception ex)
             {
                 // Log error
                 log.Error("StackElementsCollection Exception occurred: " + ex.Message);
                 //log.Debug("StackElementsCollection Exception occurred", ex);
             }
         }

         /// <summary>
         /// Get the specified stack element from the collection
         /// </summary>
         public StackElementModel GetStackElement(BindingList<StackElementModel> StackElements, int itemIndex)
         {

             StackElementModel stackElement = null;

             try
             {
                 stackElement = StackElements[itemIndex];
             }
             catch (Exception ex)
             {
                 // Log error
                 log.Error("StackElementsCollection Exception occurred: " + ex.Message);
                 //log.Debug("StackElementsCollection Exception occurred", ex);
             }

             return stackElement;
         }

        #endregion
    }
}
