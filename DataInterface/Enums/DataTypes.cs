﻿using System;
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

        [Description("Exit Polls")]
        Exit_Polls = 1,

        [Description("Balance of Power")]
        Balance_of_Power = 2,

       [Description("Referendums")]
        Referendums = 3,

    }
}
