using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Simple_Online_Voitng_System.Data;
using Simple_Online_Voitng_System.Service;

namespace Simple_Online_Voitng_System.Controllers
{
    [Authorize]
    public class VotersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IVoterManager voterManager;
        private IUserService userService;

        public VotersController(
            ApplicationDbContext context,
            IVoterManager voterManager,
            IUserService userService
            )
        {
            _context = context;
            this.voterManager = voterManager;
            this.userService = userService;
        }

        // GET: Voters
        public IActionResult Index()
        {
             return View(voterManager.GetAll());
            
        }

        // GET: Voters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voter = await _context.VoterList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voter == null)
            {
                return NotFound();
            }

            return View(voter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Email")] Voter voter)
        {

            if (ModelState.IsValid)
            {
                if (voter.Email == userService.Email())
                {
                    return Forbid();
                }
                var admin = userService.AdminEmail();
                var name = userService.Email();
                if (admin == name)
                {
                    bool isAdded = voterManager.Add(voter);
                    if (isAdded == true)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            return Forbid();
        }

        // GET: Voters/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var admin = userService.AdminEmail();
            //var name = userService.Email();
            //if (admin == name)
            //{
            //    var voter = voterManager.GetById((int)id);
            //    if (voter == null)
            //    {
            //        return NotFound();
            //    }
            //    return View(voter);
            //}

            var voter = voterManager.GetById((int)id);
            if (voter == null)
            {
                return NotFound();
            }
            return View(voter);
        }

        // POST: Voters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,IsVotedChairman,IsVotedMemberMan,IvotedMemberWomen")] Voter voter)
        {
            if (id != voter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoterExists(voter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(voter);
        }

        // GET: Voters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voter = await _context.VoterList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voter == null)
            {
                return NotFound();
            }

            return View(voter);
        }

        // POST: Voters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voter = await _context.VoterList.FindAsync(id);
            _context.VoterList.Remove(voter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoterExists(int id)
        {
            return _context.VoterList.Any(e => e.Id == id);
        }
    }
}
