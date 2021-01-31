using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
using Simple_Online_Voitng_System.Data;
using Simple_Online_Voitng_System.Service;

namespace Simple_Online_Voitng_System.Controllers
{
    [Authorize]
    public class MemberMenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment environment;
        private IUserService userService;
        private IMemberVoteManager memberManager;

        public MemberMenController
            (
            ApplicationDbContext context,
            IWebHostEnvironment environment,
            IUserService userService,
            IMemberVoteManager memberManager
            )
        {
            _context = context;
            this.environment = environment;
            this.userService = userService;
            this.memberManager = memberManager;
        }

        // GET: MemberMen
        public IActionResult Index()
        {
            return View(memberManager.GetAll());
        }

        // GET: MemberMen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberMan = await _context.MemberManList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberMan == null)
            {
                return NotFound();
            }

            return View(memberMan);
        }

        // GET: MemberMen/Create
        public IActionResult Create()
        {
            if (userService.Email() == userService.AdminEmail())
            {
                return View();
            }
            return Forbid();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateChairmanCadidate candidate)
        {
            if (ModelState.IsValid)
            {
                if (userService.Email() == userService.AdminEmail())
                {
                    MemberMan member = new MemberMan();
                    var uploadFolder = Path.Combine(environment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + candidate.ProfilePicPath.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    candidate.ProfilePicPath.CopyTo(new FileStream(filePath, FileMode.Create));

                    member.ProfilePicPath = uniqueFileName;

                    uploadFolder = Path.Combine(environment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + candidate.SymbolPath.FileName;
                    filePath = Path.Combine(uploadFolder, uniqueFileName);
                    candidate.SymbolPath.CopyTo(new FileStream(filePath, FileMode.Create));

                    member.SymbolPath = uniqueFileName;
                    member.Name = candidate.Name;
                    member.Email = candidate.Email;

                    if (memberManager.Add(member))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                
            }
            return View(candidate);
        }

        // GET: MemberMen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberMan = await _context.MemberManList.FindAsync(id);
            if (memberMan == null)
            {
                return NotFound();
            }
            return View(memberMan);
        }

        // POST: MemberMen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,ProfilePicPath,SymbolPath,TotalVote")] MemberMan memberMan)
        {
            if (id != memberMan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberMan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberManExists(memberMan.Id))
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
            return View(memberMan);
        }

        // GET: MemberMen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberMan = await _context.MemberManList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberMan == null)
            {
                return NotFound();
            }

            return View(memberMan);
        }

        // POST: MemberMen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberMan = await _context.MemberManList.FindAsync(id);
            _context.MemberManList.Remove(memberMan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberManExists(int id)
        {
            return _context.MemberManList.Any(e => e.Id == id);
        }

        public IActionResult AddVote(int? data)
        {
            if (data == null)
            {
                return NotFound();
            }

            bool isVoted = memberManager.AddVote((int)data, userService.Email());
            if (isVoted == true)
            {
                return View();
            }

            return Forbid();
        }

    }
}
