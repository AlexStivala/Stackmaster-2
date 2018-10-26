using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.Enums
{
    using System.ComponentModel;

   public enum DataTypes : int
    {
        [Description("Race Boards")]
        Race_Boards = 0,

        [Description("Voter Analysis")]
        Voter_Analysis = 1,

        [Description("Balance of Power")]
        Balance_of_Power = 2,

       [Description("Referendums")]
        Referendums = 3,

        [Description("SidePanel")]
        Side_Panel = 4,

    }
}
