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
    public class EtasjesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEtasjeRepo _etasjeRepo;
        private readonly IByggRepo _byggRepo;
        private readonly UserManager<IdentityUser> _userManager;

        public EtasjesController(ApplicationDbContext context, IEtasjeRepo etasjeRepo, IByggRepo byggRepo, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _etasjeRepo = etasjeRepo;
            _byggRepo = byggRepo;
            _userManager = userManager;            
        }

        // GET: Etasjes
        public async Task<IActionResult> Index()
        {            
            var etasje = await _etasjeRepo.GetAll().Where(x => x.UserID.Equals(_userManager.GetUserId(HttpContext.User))).ToListAsync();
            return View(etasje);           
        }

        // GET: Etasjes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }            
            var etasje = await _etasjeRepo.Get(id);

            if (etasje == null)
            {
                return NotFound();
            }

            return View(etasje);
        }

        // GET: Etasjes/Create
        public IActionResult Create()
        {
            ViewData["ByggId"] = new SelectList(_byggRepo.GetAll().Where(x => x.UserID.Equals(_userManager.GetUserId(HttpContext.User))), "Id", "Navn");
            return View();
        }

        // POST: Etasjes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Navn,Beskrivelse,Created,Updated,ByggId,UserID")] Etasje etasje)
        {
            if (ModelState.IsValid)
            {
                etasje.UserID = _userManager.GetUserId(HttpContext.User);
                await _etasjeRepo.AddNew(etasje);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ByggId"] = new SelectList(_byggRepo.GetAll().Where(x => x.UserID.Equals(_userManager.GetUserId(HttpContext.User))), "Id", "Navn", etasje.ByggId);
            return View(etasje);
        }

        // GET: Etasjes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etasje = await _etasjeRepo.Get(id);
            
            if (etasje == null)
            {
                return NotFound();
            }
            ViewData["ByggId"] = new SelectList(_byggRepo.GetAll().Where(x => x.UserID.Equals(_userManager.GetUserId(HttpContext.User))), "Id", "Navn", etasje.ByggId);
            return View(etasje);
        }

        // POST: Etasjes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Navn,Beskrivelse,Created,Updated,ByggId,UserID")] Etasje etasje)
        {
            if (id != etasje.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _etasjeRepo.Update(id, etasje);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtasjeExists(etasje.Id))
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
            ViewData["ByggId"] = new SelectList(_byggRepo.GetAll().Where(x => x.UserID.Equals(_userManager.GetUserId(HttpContext.User))), "Id", "Navn", etasje.ByggId);
            return View(etasje);
        }

        // GET: Etasjes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etasje = await _etasjeRepo.Get(id);
            if (etasje == null)
            {
                return NotFound();
            }

            return View(etasje);
        }

        // POST: Etasjes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var etasje = _context.Etasjes.Where(x => x.Id == id && x.UserID.Equals(_userManager.GetUserId(HttpContext.User))).FirstOrDefault();
            await _etasjeRepo.Remove(etasje.Id);
            return RedirectToAction(nameof(Index));
        }

        private bool EtasjeExists(int id)
        {
            return _context.Etasjes.Any(e => e.Id == id);
        }
    }
}
