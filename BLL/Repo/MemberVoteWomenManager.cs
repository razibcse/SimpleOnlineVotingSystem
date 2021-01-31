using BLL.Base;
using BLL.Interfaces;
using Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Repo
{
    public class MemberVoteWomenManager:Manager<MemberWoman>,IMemberWomenVoteManager
    {
        private IMemberWomenVoteRepository repository;
        public MemberVoteWomenManager(IMemberWomenVoteRepository repository) :base(repository)
        {
            this.repository = repository;
        }

        public bool AddVote(int id, string voterId)
        {
            return repository.AddVote(id, voterId);
        }

        public MemberWoman GetWinner()
        {
            return repository.GetWinnerWomanMembar();
        }
    }
}
