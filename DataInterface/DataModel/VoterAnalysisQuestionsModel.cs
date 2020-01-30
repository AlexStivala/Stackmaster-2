using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.DataModel
{
    public class VoterAnalysisQuestionsModel
    {
        public string state { get; set; }
        public string ofc { get; set; }
        public string qcode { get; set; }
        public string filter { get; set; }
        public string r_type { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public string VA_Data_Id { get; set; }
        public string race_id { get; set; }
        public string preface { get; set; }
        public int stateId { get; set; }
    }

    public class VoterAnalysisQuestionsModelNew
    {
        public string state { get; set; }
        public string ofc { get; set; }
        public string r_type { get; set; }
        public string qcode { get; set; }
        public string filter { get; set; }
        public string header { get; set; }
        public string VA_Data_Id { get; set; }
        
    }


    public class VoterAnalysisManualQuestionsModelNew
    {
        public string st { get; set; }
        public string r_type { get; set; }
        public string state { get; set; }
        public string race { get; set; }
        public string question { get; set; }
        public string VA_Data_Id { get; set; }
        
    }
}
