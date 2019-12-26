using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.DataModel
{
    // Class for available races
    public class AvailableRaceModel
    {
        public Int32 Race_ID { get; set; }
        public string Election_Type { get; set; }
        public string Race_Description { get; set; }
        public Int16 State_Number { get; set; }
        public string State_Mnemonic { get; set; }
        public string State_Name { get; set; }
        public Int16 CD { get; set; }
        public string Race_Office { get; set; }
        public Int16 Race_OfficeSortOrder { get; set; }
        public DateTime Race_PollClosingTime { get; set; }
        public Boolean Race_UseAPRaceCall { get; set; }
        public string Party { get; set; }

    }
}
