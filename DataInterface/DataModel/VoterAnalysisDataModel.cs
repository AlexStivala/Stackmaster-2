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
        public string State { get; set; }
        public string Title { get; set; }
        public string Response { get; set; }
        public string VA_Data_Id { get; set; }
        public string Updated { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public string party { get; set; }
        public int percent { get; set; }
        public string asOf { get; set; }
        
    }

    public class VoterAnalysisManualDataModel
    {
        public string r_type { get; set; }
        public string State { get; set; }
        public string Title { get; set; }
        public string Response { get; set; }
        public string VA_Data_Id { get; set; }
        public string Updated { get; set; }
        public string Percent { get; set; }
        public string asOf { get; set; }

    }
}
