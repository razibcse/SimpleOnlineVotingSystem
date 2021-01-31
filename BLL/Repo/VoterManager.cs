using BLL.Base;
using BLL.Interfaces;
using Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Repo
{
    public class VoterManager:Manager<Voter>,IVoterManager
    {
        private IvoterRepository voterRepository;
        public VoterManager(IvoterRepository repository):base(repository)
        {
            voterRepository = repository;
        }
    }
}
