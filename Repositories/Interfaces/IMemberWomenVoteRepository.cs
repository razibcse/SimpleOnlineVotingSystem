using Models;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Interfaces
{
    public interface IMemberWomenVoteRepository:IRepository<MemberWoman>
    {
        bool AddVote(int id, string voterId);
        MemberWoman GetWinnerWomanMembar();
    }
}
