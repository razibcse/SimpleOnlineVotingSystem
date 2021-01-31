using Models;
using Repositories.Base;
using Repositories.Interfaces;
using Simple_Online_Voitng_System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.Repo
{
    public class ChairmenVoteRepositry : Repository<Chairman>, IChairmenVoteRepository
    {
        public ChairmenVoteRepositry(ApplicationDbContext db):base(db)
        {

        }
        public bool AddVote(int id, string voterId)
        {
            var voter = db.VoterList.Where(mail => mail.Email == voterId).FirstOrDefault();
            if (voter == null)
            {
                return false;
            }
            if (voter.IsVotedChairman == true)
            {
                return false;
            }
            voter.IsVotedChairman = true;
            db.Update(voter);
            bool isUpdated= db.SaveChanges()>0;
            if (isUpdated)
            {
                var chairman = db.ChairmanList.Find(id);
                if (chairman == null)
                {
                    voter.IsVotedChairman = false;
                    db.Update(voter);
                    db.SaveChanges();
                    return false;
                }
                chairman.TotalVote = chairman.TotalVote + 1;
                if (base.Update(chairman))
                {
                    return true;
                }
                
            }
            return false;
        }

        public Chairman GetWinnerChairman()
        {
            var winner = db.ChairmanList.OrderByDescending(vote => vote.TotalVote).FirstOrDefault();
            return winner;
        }
    }
}
