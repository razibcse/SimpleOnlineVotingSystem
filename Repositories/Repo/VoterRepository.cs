using Models;
using Repositories.Base;
using Repositories.Interfaces;
using Simple_Online_Voitng_System.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Repo
{
    public class VoterRepository:Repository<Voter>,IvoterRepository
    {
        public VoterRepository(ApplicationDbContext db):base(db)
        {

        }
    }
}
