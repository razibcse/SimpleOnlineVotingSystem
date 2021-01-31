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
    public class MemberWomenVoteRepository:Repository<MemberWoman>,IMemberWomenVoteRepository
    {
        public MemberWomenVoteRepository(ApplicationDbContext db):base(db)
        {

        }

        public bool AddVote(int id, string voterId)
        {
            var voter = db.VoterList.Where(mail => mail.Email == voterId).FirstOrDefault();
            if (voter == null)
            {
                return false;
            }
            if (voter.IvotedMemberWomen == true)
            {
                return false;
            }
            voter.IvotedMemberWomen = true;
            db.Update(voter);
            bool isUpdated = db.SaveChanges() > 0;
            if (isUpdated)
            {
                var member = db.MemberWomenList.Find(id);
                if (member == null)
                {
                    voter.IvotedMemberWomen = false;
                    db.Update(voter);
                    db.SaveChanges();
                    return false;
                }
                member.TotalVote = member.TotalVote + 1;
                if (base.Update(member))
                {
                    return true;
                }

            }
            return false;
        }

        public MemberWoman GetWinnerWomanMembar()
        {
            var winner = db.MemberWomenList.OrderByDescending(vote => vote.TotalVote).FirstOrDefault();
            return winner;
        }
    }
}
