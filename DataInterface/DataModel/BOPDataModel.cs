using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.DataModel
{
    public class BOPDataModel
    {
        public Int16 BOPType { get; set; }
        public string Branch { get; set; }
        public string Session { get; set; }
        public Int16 Total { get; set; }
        public Int16 DemCurrent { get; set; }
        public Int16 DemNew { get; set; }
        public Int16 DemDelta { get; set; }
        public Int16 DemGain { get; set; }
        public Int16 RepCurrent { get; set; }
        public Int16 RepNew { get; set; }
        public Int16 RepDelta { get; set; }
        public Int16 RepGain { get; set; }
        public Int16 IndCurrent { get; set; }
        public Int16 IndNew { get; set; }
        public Int16 IndDelta { get; set; }
        public Int16 IndGain { get; set; }
    }

}
