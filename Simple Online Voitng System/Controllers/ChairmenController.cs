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
    public class ChairmenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment environment;
        private IUserService userService;
        private ICharimenVoteManager chairmanManager;

        public ChairmenController
            (ApplicationDbContext context,
            IWebHostEnvironment environment,
            IUserService userService,
            ICharimenVoteManager charimen
            )
        {
            _context = context;
            this.environment = environment;
            this.userService = userService;
            this.chairmanManager = charimen;
        }

        // GET: Chairmen
        public IActionResult Index()
        {
            //return View();
            return View(
                chairmanManager.GetAll()
                );
        }

        // GET: Chairmen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chairman = await _context.ChairmanList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chairman == null)
            {
                return NotFound();
            }

            return View(chairman);
        }

        // GET: Chairmen/Create
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
                    Chairman chairman = new Chairman();
                    var uploadFolder = Path.Combine(environment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + candidate.ProfilePicPath.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    candidate.ProfilePicPath.CopyTo(new FileStream(filePath, FileMode.Create));

                    chairman.ProfilePicPath = uniqueFileName;

                    uploadFolder = Path.Combine(environment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + candidate.SymbolPath.FileName;
                    filePath = Path.Combine(uploadFolder, uniqueFileName);
                    candidate.SymbolPath.CopyTo(new FileStream(filePath, FileMode.Create));

                    chairman.SymbolPath = uniqueFileName;
                    chairman.Name = candidate.Name;
                    chairman.Email = candidate.Email;

                    if (chairmanManager.Add(chairman))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(candidate);
        }

        // GET: Chairmen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chairman = await _context.ChairmanList.FindAsync(id);
            if (chairman == null)
            {
                return NotFound();
            }
            return View(chairman);
        }

        // POST: Chairmen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,ProfilePicPath,SymbolPath,TotalVote")] Chairman chairman)
        {
            if (id != chairman.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chairman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChairmanExists(chairman.Id))
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
            return View(chairman);
        }

        // GET: Chairmen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chairman = await _context.ChairmanList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chairman == null)
            {
                return NotFound();
            }

            return View(chairman);
        }

        // POST: Chairmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chairman = await _context.ChairmanList.FindAsync(id);
            _context.ChairmanList.Remove(chairman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChairmanExists(int id)
        {
            return _context.ChairmanList.Any(e => e.Id == id);
        }

        public IActionResult AddVote(int? data)
        {
            if (data == null)
            {
                return NotFound();
            }

            bool isVoted= chairmanManager.AddVote((int)data, userService.Email());
            if (isVoted == true)
            {
                return View();
            }

            return Forbid();
        }
    }
}
