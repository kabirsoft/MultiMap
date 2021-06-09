using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MultiMap.Data;
using MultiMap.Data.IRepositories;
using MultiMap.Data.Models;

namespace MultiMap.Controllers
{
    [Authorize]
    public class ByggsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IByggRepo _byggRepo;
        private readonly ILokasjonRepo _lokasjonRepo;
        private readonly UserManager<IdentityUser> _userManager;

        public ByggsController(ApplicationDbContext context, IByggRepo byggRepo, UserManager<IdentityUser> userManager, ILokasjonRepo lokasjonRepo)
        {
            _context = context;
            this._byggRepo = byggRepo;
            this._userManager = userManager;
            _lokasjonRepo = lokasjonRepo;
        }

        // GET: Byggs
        public async Task<IActionResult> Index()
        {
            var byggs = await _byggRepo.GetAll().Where(x => x.UserID.Equals(_userManager.GetUserId(HttpContext.User))).ToListAsync();
            return View(byggs);
            //var applicationDbContext = _context.Byggs.Include(b => b.Lokasjon);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Byggs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bygg = await _byggRepo.Get(id);
            if (bygg == null)
            {
                return NotFound();
            }

            return View(bygg);
        }

        // GET: Byggs/Create
        public IActionResult Create()
        {           
            ViewData["LokasjonId"] = new SelectList(_lokasjonRepo.GetAll().Where(x => x.UserID.Equals(_userManager.GetUserId(HttpContext.User))), "Id", "Navn");
            return View();
        }

        // POST: Byggs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Navn,Beskrivelse,AntallEtasje,Byggeår,Created,Updated,LokasjonId,UserID")] Bygg bygg)
        {
            if (ModelState.IsValid)
            {
                bygg.UserID = _userManager.GetUserId(HttpContext.User);
                await _byggRepo.AddNew(bygg);               
                return RedirectToAction(nameof(Index));
            }
            ViewData["LokasjonId"] = new SelectList(_lokasjonRepo.GetAll().Where(x => x.UserID.Equals(_userManager.GetUserId(HttpContext.User))), "Id", "Navn", bygg.LokasjonId);
            return View(bygg);
        }

        // GET: Byggs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bygg = await _byggRepo.Get(id);
            if (bygg == null)
            {
                return NotFound();
            }
            ViewData["LokasjonId"] = new SelectList(_lokasjonRepo.GetAll().Where(x => x.UserID.Equals(_userManager.GetUserId(HttpContext.User))), "Id", "Navn", bygg.LokasjonId);
            return View(bygg);
        }

        // POST: Byggs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Navn,Beskrivelse,AntallEtasje,Byggeår,Created,Updated,LokasjonId,UserID")] Bygg bygg)
        {
            if (id != bygg.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _byggRepo.Update(id, bygg);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ByggExists(bygg.Id))
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
            ViewData["LokasjonId"] = new SelectList(_context.Lokasjons, "Id", "Navn", bygg.LokasjonId);
            return View(bygg);
        }

        // GET: Byggs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bygg = await _byggRepo.Get(id);
            if (bygg == null)
            {
                return NotFound();
            }

            return View(bygg);
        }

        // POST: Byggs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bygg = _context.Byggs.Where(x => x.Id == id && x.UserID.Equals(_userManager.GetUserId(HttpContext.User))).FirstOrDefault();
            await _byggRepo.Remove(bygg.Id);            
            return RedirectToAction(nameof(Index));
        }

        private bool ByggExists(int id)
        {
            return _context.Byggs.Any(e => e.Id == id);
        }
    }
}
