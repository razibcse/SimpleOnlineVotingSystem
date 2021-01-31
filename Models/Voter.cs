using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Voter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsVotedChairman { get; set; }
        public bool IsVotedMemberMan { get; set; }
        public bool IvotedMemberWomen { get; set; }

    }
}
