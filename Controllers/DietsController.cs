﻿    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using WebApplication1.Data;
    using WebApplication1.Models;

    namespace WebApplication1.Controllers
    {
        public class DietsController : Controller
        {
            private readonly FitnessDbContext _context;

            public DietsController(FitnessDbContext context)
            {
                _context = context;
            }

            // GET: Diets
            public async Task<IActionResult> Index()
            {
                return View(await _context.Diet.ToListAsync());
            }

            // GET: Diets/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var diet = await _context.Diet
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (diet == null)
                {
                    return NotFound();
                }

                return View(diet);
            }

            // GET: Diets/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Diets/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,FoodItem,Calories,Date")] Diet diet)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(diet);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(diet);
            }

            // GET: Diets/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var diet = await _context.Diet.FindAsync(id);
                if (diet == null)
                {
                    return NotFound();
                }
                return View(diet);
            }

            // POST: Diets/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,FoodItem,Calories,Date")] Diet diet)
            {
                if (id != diet.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(diet);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!DietExists(diet.Id))
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
                return View(diet);
            }

            // GET: Diets/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var diet = await _context.Diet
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (diet == null)
                {
                    return NotFound();
                }

                return View(diet);
            }

            // POST: Diets/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var diet = await _context.Diet.FindAsync(id);
                if (diet != null)
                {
                    _context.Diet.Remove(diet);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool DietExists(int id)
            {
                return _context.Diet.Any(e => e.Id == id);
            }
        }
    }
