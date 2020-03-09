using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.DataModel
{
    public class RaceDataModel
    {

        public string Office { get; set; }
        public string OfficeName { get; set; }
        public string StateName { get; set; }
        public string StateAbbv { get; set; }
        public Boolean IsAtLargeHouseState { get; set; }
        public string cntyName { get; set; }
        public Int16 CD { get; set; }
        public string eType { get; set; }
        public Int32 TotalPrecincts { get; set; }
        public Int32 PrecinctsReporting { get; set; }
        public float PercentExpectedVote { get; set; }
        public Int32 CandidateID { get; set; }
        public string FoxID { get; set; }
        public string CandidateLastName { get; set; }
        public string LastNameAir { get; set; }
        public string CandidateFirstName { get; set; }
        public Boolean UseHeadshotFNC { get; set; }
        public string HeadshotPathFNC { get; set; }
        public Boolean UseHeadshotFBN { get; set; }
        public string HeadshotPathFBN { get; set; }
        public string CandidatePartyID { get; set; }
        public string cStat { get; set; }
        public string estTS { get; set; }
        public string InIncumbentPartyFlag { get; set; }
        public string IsIncumbentFlag { get; set; }
        public Int32 CandidateVoteCount { get; set; }
        public Int32 TotalVoteCount { get; set; }
        public Boolean RaceWinnerCalled { get; set; }
        public DateTime RaceWinnerCallTime { get; set; }
        public Boolean RaceTooCloseToCall { get; set; }
        public Int32 RaceWinnerCandidateID { get; set; }
        public DateTime RacePollClosingTime { get; set; }
        public Boolean RaceUseAPRaceCall { get; set; }
        public Boolean RaceIgnoreGain { get; set; }
        public int DemDelegatesAvailable { get; set; }
        public int RepDelegatesAvailable { get; set; }
        public int ElectoralVotesAvailable { get; set; }

    }
}
