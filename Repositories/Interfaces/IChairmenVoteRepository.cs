using Models;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Interfaces
{
    public interface IChairmenVoteRepository:IRepository<Chairman>
    {
        bool AddVote(int id, string voterId);
        Chairman GetWinnerChairman();
    }
}
