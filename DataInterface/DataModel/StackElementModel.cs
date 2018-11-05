using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataInterface.DataModel
{
    /// <summary>
    /// Class definition for stack elements of type Race Board
    /// </summary>
    public class StackElementModel
    {
        // Stack metadata
        public Double fkey_StackID { get; set; }
        public Int64 Stack_Element_ID { get; set; }
        public Int16 Stack_Element_Type { get; set; }
        public Int16 Stack_Element_Data_Type { get; set; }
        public string Stack_Element_Description { get; set; }
        public string Stack_Element_TemplateID { get; set; }

        // General race/election info
        public string Election_Type { get; set; }
        public string Office_Code { get; set; }
        public Int16 State_Number { get; set; }
        public string State_Mnemonic { get; set; }
        public string State_Name { get; set; }
        public Int16 CD { get; set; }
        public Int32 County_Number { get; set; }
        public string County_Name { get; set; }
        [MaxLength(100)]
        public string Listbox_Description { get; set; }

        // Specific to race boards
        public Int32 Race_ID { get; set; }
        public string Race_RecordType { get; set; }
        public string Race_Office { get; set; }
        public Int16 Race_District { get; set; }
        public Int32 Race_CandidateID_1 { get; set; }
        public Int32 Race_CandidateID_2 { get; set; }
        public Int32 Race_CandidateID_3 { get; set; }
        public Int32 Race_CandidateID_4 { get; set; }
        public DateTime Race_PollClosingTime { get; set; }
        public Boolean Race_UseAPRaceCall { get; set; }

        //Specific to exit polls
        public Int32 ExitPoll_mxID { get; set; }
        public Int16 ExitPoll_BoardID { get; set; }
        public string ExitPoll_ShortMxLabel { get; set; }
        public Int16 ExitPoll_NumRows { get; set; }
        public Int16 ExitPoll_xRow { get; set; }
        public Boolean ExitPoll_BaseQuestion { get; set; }
        public Boolean ExitPoll_RowQuestion { get; set; }
        public string ExitPoll_Subtitle { get; set; }
        public string ExitPoll_Suffix { get; set; }
        public string ExitPoll_HeaderText_1 { get; set; }
        public string ExitPoll_HeaderText_2 { get; set; }
        public string ExitPoll_SubsetName { get; set; }
        public Int32 ExitPoll_SubsetID { get; set; }
    }
}
