using BLL.Base;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IMemberVoteManager:IManager<MemberMan>
    {
        bool AddVote(int id, string userId);
        MemberMan GetWinner();
    }
}

