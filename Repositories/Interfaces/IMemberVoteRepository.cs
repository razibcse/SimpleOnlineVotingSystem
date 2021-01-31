using Models;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Interfaces
{
    public interface IMemberVoteRepository:IRepository<MemberMan>
    {
        bool AddVote(int id, string voterId);
        MemberMan GetWinnerMemberMan();
    }
}
