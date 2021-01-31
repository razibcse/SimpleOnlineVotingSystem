using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simple_Online_Voitng_System.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Voter> VoterList { get; set; }
        public DbSet<Chairman> ChairmanList { get; set; }
        public DbSet<MemberMan> MemberManList { get; set; }
        public DbSet<MemberWoman> MemberWomenList { get; set; }
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             base.OnConfiguring(optionsBuilder);
        }

        public ApplicationDbContext()
        {
        }
    }
}
