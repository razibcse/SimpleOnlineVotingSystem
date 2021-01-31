using BLL.Base;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICharimenVoteManager:IManager<Chairman>
    {
        bool AddVote(int id, string voterId);
        Chairman GetWinner();
    }
}
