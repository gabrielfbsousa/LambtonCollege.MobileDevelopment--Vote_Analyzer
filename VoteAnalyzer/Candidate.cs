using System;
using System.Collections.Generic;

namespace VoteAnalyzer
{
    public class Candidate
    {

        private string name;
        private string party;
        private string role;
        private string number;
        private List<string> corruptionCases;
        private List<string> candidateVotes;
        private int rating;

        public Candidate()
        {
            corruptionCases = new List<string>();
            corruptionCases.Add(" ");
            candidateVotes = new List<string>();
        }

        public string Name { get => name; set => name = value; }
        public string Party { get => party; set => party = value; }
        public string Role { get => role; set => role = value; }
        public List<string> CorruptionCases { get => corruptionCases; set => corruptionCases = value; }
        public List<string> CandidateVotes { get => candidateVotes; set => candidateVotes = value; }
        public int Rating { get => rating; set => rating = value; }
        public string Number { get => number; set => number = value; }
    }
}
