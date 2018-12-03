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
        [MaxLength(200)]
        public string Listbox_Description { get; set; }

        // Specific to race boards
        public Int32 Race_ID { get; set; }
        public string Race_Office { get; set; }
        public Int32 Race_CandidateID_1 { get; set; }
        public Int32 Race_CandidateID_2 { get; set; }
        public Int32 Race_CandidateID_3 { get; set; }
        public Int32 Race_CandidateID_4 { get; set; }
        public DateTime Race_PollClosingTime { get; set; }
        public Boolean Race_UseAPRaceCall { get; set; }

        //Specific to exit polls
        public string VA_Data_ID { get; set; }
        public string VA_Title { get; set; }
        public string VA_Type { get; set; }
        public string VA_Map_Color { get; set; }
        public Int32 VA_Map_ColorNum { get; set; }
    }
}
