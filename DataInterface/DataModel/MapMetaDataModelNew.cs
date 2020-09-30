using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoterAnalysisParser
{
    public class MapMetaDataModelNew
    {
        public string VA_Data_Id { get; set; }
        public string color { get; set; }
        public int colorIndex { get; set; }
        public int colorValue { get; set; }
        public int numColorBands { get; set; }
        public int  band { get; set; }
        public string bandLabel { get; set; }
        public int bandLo { get; set; }
        public int bandHi { get; set; }
        public string Title { get; set; }

    }
}
