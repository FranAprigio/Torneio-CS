using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TorneioCS.Context;
using TorneioCS.Models;

namespace TorneioCS.Controllers
{
    public class CompetidorsController : Controller
    {
        private readonly DatabaseContext _context;

        public CompetidorsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Competidors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Competidores.ToListAsync());
        }

        // GET: Competidors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competidor = await _context.Competidores
                .FirstOrDefaultAsync(m => m.idCompetidor == id);
            if (competidor == null)
            {
                return NotFound();
            }

            return View(competidor);
        }

        // GET: Competidors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Competidors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idCompetidor,Nome,Idade,TotalKill,TotalPartidas,Vitorias")] Competidor competidor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competidor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(competidor);
        }

        // GET: Competidors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competidor = await _context.Competidores.FindAsync(id);
            if (competidor == null)
            {
                return NotFound();
            }
            return View(competidor);
        }

        // POST: Competidors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idCompetidor,Nome,Idade,TotalKill,TotalPartidas,Vitorias")] Competidor competidor)
        {
            if (id != competidor.idCompetidor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competidor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetidorExists(competidor.idCompetidor))
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
            return View(competidor);
        }

        // GET: Competidors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competidor = await _context.Competidores
                .FirstOrDefaultAsync(m => m.idCompetidor == id);
            if (competidor == null)
            {
                return NotFound();
            }

            return View(competidor);
        }

        // POST: Competidors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var competidor = await _context.Competidores.FindAsync(id);
            _context.Competidores.Remove(competidor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetidorExists(int id)
        {
            return _context.Competidores.Any(e => e.idCompetidor == id);
        }
    }
}
