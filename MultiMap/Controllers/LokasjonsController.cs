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
    public class LokasjonsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILokasjonRepo _lokasjonRepo;
        private readonly UserManager<IdentityUser> _userManager;

        public LokasjonsController(ApplicationDbContext context, ILokasjonRepo lokasjonRepo, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _lokasjonRepo = lokasjonRepo;
            _userManager = userManager;
        }

        // GET: Lokasjons
        public async Task<IActionResult> Index()
        {
            var lokasjon = await _lokasjonRepo.GetAll().Where(x => x.UserID.Equals(_userManager.GetUserId(HttpContext.User))).ToListAsync();
            return View(lokasjon);
        }

        // GET: Lokasjons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }            
            var lokasjon = await _lokasjonRepo.Get(id);
            if (lokasjon == null)
            {
                return NotFound();
            }

            return View(lokasjon);
        }

        // GET: Lokasjons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lokasjons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Navn,Beskrivelse,Selskap,AntallBygg,Created,Updated,UserID")] Lokasjon lokasjon)
        {
            if (ModelState.IsValid)
            {
                lokasjon.UserID = _userManager.GetUserId(HttpContext.User);
                await _lokasjonRepo.AddNew(lokasjon);
                return RedirectToAction(nameof(Index));
            }
            return View(lokasjon);
        }

        // GET: Lokasjons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lokasjon = await _lokasjonRepo.Get(id);           
            if (lokasjon == null)
            {
                return NotFound();
            }
            return View(lokasjon);
        }

        // POST: Lokasjons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Navn,Beskrivelse,Selskap,AntallBygg,Created,Updated,UserID")] Lokasjon lokasjon)
        {
            if (id != lokasjon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _lokasjonRepo.Update(id,lokasjon);                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LokasjonExists(lokasjon.Id))
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
            return View(lokasjon);
        }

        // GET: Lokasjons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lokasjon = await _lokasjonRepo.Get(id);           
            if (lokasjon == null)
            {
                return NotFound();
            }

            return View(lokasjon);
        }

        // POST: Lokasjons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lokasjon = _context.Lokasjons.Where(x => x.Id == id && x.UserID.Equals(_userManager.GetUserId(HttpContext.User))).FirstOrDefault();
            await _lokasjonRepo.Remove(lokasjon.Id);
            
            return RedirectToAction(nameof(Index));
        }

        private bool LokasjonExists(int id)
        {
            return _context.Lokasjons.Any(e => e.Id == id);
        }
    }
}
