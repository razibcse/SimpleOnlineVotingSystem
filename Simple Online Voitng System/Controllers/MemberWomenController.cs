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
    public class MemberWomenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment environment;
        private IUserService userService;
        private IMemberWomenVoteManager memberManager;

        public MemberWomenController(
            ApplicationDbContext context,
            IWebHostEnvironment environment,
            IUserService userService,
            IMemberWomenVoteManager memberManager
            )
        {
            _context = context;
            this.environment = environment;
            this.userService = userService;
            this.memberManager = memberManager;
        }

        // GET: MemberWomen
        public IActionResult Index()
        {
            return View(memberManager.GetAll());
        }

        // GET: MemberWomen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberWoman = await _context.MemberWomenList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberWoman == null)
            {
                return NotFound();
            }

            return View(memberWoman);
        }

        // GET: MemberWomen/Create
        public IActionResult Create()
        {
            if (userService.Email() == userService.AdminEmail())
            {
                return View();
            }
            return Forbid();
        }

        // POST: MemberWomen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateChairmanCadidate candidate)
        {
            if (ModelState.IsValid)
            {
                if (userService.Email() == userService.AdminEmail())
                {
                    MemberWoman member = new MemberWoman();
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

        // GET: MemberWomen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberWoman = await _context.MemberWomenList.FindAsync(id);
            if (memberWoman == null)
            {
                return NotFound();
            }
            return View(memberWoman);
        }

        // POST: MemberWomen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,ProfilePicPath,SymbolPath,TotalVote")] MemberWoman memberWoman)
        {
            if (id != memberWoman.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberWoman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberWomanExists(memberWoman.Id))
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
            return View(memberWoman);
        }

        // GET: MemberWomen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberWoman = await _context.MemberWomenList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberWoman == null)
            {
                return NotFound();
            }

            return View(memberWoman);
        }

        // POST: MemberWomen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberWoman = await _context.MemberWomenList.FindAsync(id);
            _context.MemberWomenList.Remove(memberWoman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberWomanExists(int id)
        {
            return _context.MemberWomenList.Any(e => e.Id == id);
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
