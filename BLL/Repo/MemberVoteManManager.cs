using BLL.Base;
using BLL.Interfaces;
using Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Repo
{
    public class MemberVoteManManager:Manager<MemberMan>,IMemberVoteManager
    {
        private IMemberVoteRepository repository;

        public MemberVoteManManager(IMemberVoteRepository repository):base(repository)
        {
            this.repository = repository;
        }

        public bool AddVote(int id, string userId)
        {
            return repository.AddVote(id, userId);
        }

        public MemberMan GetWinner()
        {
            return repository.GetWinnerMemberMan();
        }
    }
}
