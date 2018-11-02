using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.DataModel
{
    public class VoterAnalysisMapDataModel
    {
        public string State { get; set; }
        public int Percent { get; set; }
        public int RowNumber { get; set; }
        public string RowLabel { get; set; }
        public int MapId { get; set; }
        public int Color { get; set; }
        public int Position { get; set; }

    }
}
