using BLL.Base;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IMemberWomenVoteManager : IManager<MemberWoman>
    {
        bool AddVote(int id, string voterId);
        MemberWoman GetWinner();
    }
}
