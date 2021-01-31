using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Online_Voitng_System.Controllers
{
    public class WinnerController : Controller
    {
        private IMemberVoteManager memberManager;
        private ICharimenVoteManager charimenVoteManager;
        private IMemberWomenVoteManager memberWomenVoteManager;

        public WinnerController(
            IMemberVoteManager memberManager,
            ICharimenVoteManager charimenVoteManager,
            IMemberWomenVoteManager memberWomenVoteManager
            )
        {
            this.memberWomenVoteManager = memberWomenVoteManager;
            this.memberManager = memberManager;
            this.charimenVoteManager = charimenVoteManager;
        }
        public IActionResult Index()
        {
            var chairman = charimenVoteManager.GetWinner();
            var member = memberManager.GetWinner();
            var womanMember = memberWomenVoteManager.GetWinner();



            ViewData["chairman"] = chairman;
            ViewData["member"] = member;
            ViewData["womenmember"] = womanMember;
            return View();
        }
    }
}
