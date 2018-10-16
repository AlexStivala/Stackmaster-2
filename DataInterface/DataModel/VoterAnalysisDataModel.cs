using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.DataModel
{
    public class VoterAnalysisDataModel
    {
        public string r_type { get; set; }
        public string preface { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public string VA_Data_Id { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public string party { get; set; }
        public int percent { get; set; }

    }
}
