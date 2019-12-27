using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.Enums
{
    using System.ComponentModel;

    public enum StackElementTypes : int
    {
        [Description("Race Board (1-Way)")]
        Race_Board_1_Way = 1,

        [Description("Race Board (1-Way Select)")]
        Race_Board_1_Way_Select = 2,

        [Description("Race Board (2-Way)")]
        Race_Board_2_Way = 3,

        [Description("Race Board (2-Way Select)")]
        Race_Board_2_Way_Select = 4,

        [Description("Race Board (3-Way)")]
        Race_Board_3_Way = 5,

        [Description("Race Board (3-Way Select)")]
        Race_Board_3_Way_Select = 6,

        [Description("Race Board (4-Way)")]
        Race_Board_4_Way = 7,

        [Description("Race Board (4-Way Select)")]
        Race_Board_4_Way_Select = 8,
        
        [Description("Exit Poll (Full-Screen)")]
        Exit_Poll_Full_Screen = 9,
        
        [Description("Balance of Power (House, Current)")]
        Balance_of_Power_House_Current = 10,

        [Description("Balance of Power (Senate, Current)")]
        Balance_of_Power_Senate_Current = 11,
        
        [Description("Balance of Power (House, New)")]
        Balance_of_Power_House_New = 12,
                
        [Description("Balance of Power (Senate, New)")]
        Balance_of_Power_Senate_New = 13,
    
        [Description("Balance of Power (House, Net Gain)")]
        Balance_of_Power_House_Net_Gain = 14,

        [Description("Balance of Power (Senate, Net Gain)")]
        Balance_of_Power_Senate_Net_Gain = 15,
        
        [Description("Electoral College")]
        Electoral_College = 16,

        [Description("Referendums")]
        Referendums = 17,

        [Description("Voter Analysis (Full-Screen)")]
        Voter_Analysis_Full_Screen = 18,

        [Description("Voter Analysis (Ticker)")]
        Voter_Analysis_Ticker = 19,

        [Description("Voter Analysis (Maps)")]
        Voter_Analysis_Maps = 20,

        [Description("Race Board (All-Way)")]
        Race_Board_All_Way = 21,


    }
}
