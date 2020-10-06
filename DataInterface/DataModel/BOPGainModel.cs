using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.DataModel
{
    public class BOPGainModel
    {
        public Int16 HouseRep { get; set; }
        public Int16 HouseDem { get; set; }
        public Int16 HouseInd { get; set; }
        public Int16 HouseRepGain { get; set; }
        public Int16 HouseDemGain { get; set; }
        public Int16 HouseIndGain { get; set; }
        public Int16 SenateRep { get; set; }
        public Int16 SenateDem { get; set; }
        public Int16 SenateInd { get; set; }
        public Int16 SenateRepGain { get; set; }
        public Int16 SenateDemGain { get; set; }
        public Int16 SenateIndGain { get; set; }
        public string HouseCtrl { get; set; }
        public string SenateCtrl { get; set; }

    }

}
