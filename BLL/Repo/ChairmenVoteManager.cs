using BLL.Base;
using BLL.Interfaces;
using Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Repo
{
    public class ChairmenVoteManager:Manager<Chairman>, ICharimenVoteManager
    {
        private IChairmenVoteRepository repository;
        public ChairmenVoteManager(IChairmenVoteRepository repository):base(repository)
        {
            this.repository = repository;
        }

        public bool AddVote(int id, string voterId)
        {

            return repository.AddVote(id, voterId);
        }

        public Chairman GetWinner()
        {
            return repository.GetWinnerChairman();
        }
    }
}
