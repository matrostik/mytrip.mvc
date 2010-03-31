using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Core;

namespace Mytrip.Votes
{
    public class VotesSetting
    {
        public static string connectionString = CoreSetting.connectionStringSQL("VotesEntities");
    }
}
