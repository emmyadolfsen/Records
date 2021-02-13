using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb3.Data;
using Labb3.Models;

namespace Labb3.Controllers
{
    public class OnloanController : Controller
    {
        private readonly MusicContext _context;

        public OnloanController(MusicContext context)
        {
            _context = context;
        }

        // GET: Onloan
        public async Task<IActionResult> Index()
        {
            // Get Onloan table - include Record object
            var musicContext = _context.Onloan.Include(o => o.Record);
            return View(await musicContext.ToListAsync());
        }

        /*
        // GET: Onloan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Where id match - get Onloan - include Record
            var onloan = await _context.Onloan
                .Include(o => o.Record)
                .FirstOrDefaultAsync(m => m.OnloanId == id);

            if (onloan == null)
            {
                return NotFound();
            }

            return View(onloan); // Return 
        }
        */

        // GET: Onloan/Create
        public IActionResult Create()
        {
            ViewData["RecordId"] = new SelectList(_context.Record // Instantiate new selectlist with recordNames..
                .Where(p => p.Onloan == false), "RecordId", "RecordName"); // ..where Onloan bool is false(album is free to loan)
            return View();
        }

        // POST: Onloan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OnloanId,FriendName,DateRegistered,RecordId")] Onloan onloan)
        {
 
            if (ModelState.IsValid)
            {
                //Change bool to know if album is free to loan or not
                var albumid = onloan.RecordId; // Id of the borrowed album
                var changebool = _context.Record    // Get column with matching id in database
                    .Where(p => p.RecordId == albumid)
                    .FirstOrDefault();

                // Change bool and save
                changebool.Onloan = true;
                _context.SaveChanges();

                // Add new Onloan to database - name + album
                _context.Add(onloan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["RecordId"] = new SelectList(_context.Record, "RecordId", "RecordName", onloan.RecordId);
            return View(onloan);
        }

        /*
        // GET: Onloan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var onloan = await _context.Onloan.FindAsync(id);
            if (onloan == null)
            {
                return NotFound();
            }
            ViewData["RecordId"] = new SelectList(_context.Record, "RecordId", "RecordName", onloan.RecordId);
            return View(onloan);
        }
        

        // POST: Onloan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OnloanId,FriendName,DateRegistered,RecordId")] Onloan onloan)
        {
            if (id != onloan.OnloanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(onloan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OnloanExists(onloan.OnloanId))
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
            ViewData["RecordId"] = new SelectList(_context.Record, "RecordId", "RecordId", onloan.RecordId);
            return View(onloan);
        }
        */

        // GET: Onloan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get onloan column with mathing id from database - include record
            var onloan = await _context.Onloan
                .Include(o => o.Record)
                .FirstOrDefaultAsync(m => m.OnloanId == id);


            if (onloan == null)
            {
                return NotFound();
            }

            return View(onloan);
        }

        // POST: Onloan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Get onloan column with mathing id from database - delete column
            var onloan = await _context.Onloan.FindAsync(id);
            _context.Onloan.Remove(onloan);
            await _context.SaveChangesAsync();

            //Change bool to know if album is free to loan or not
            var albumid = onloan.RecordId; // Id of the borrowed album
            var changebool = _context.Record    // Get column with matching id in database
                .Where(p => p.RecordId == albumid)
                .FirstOrDefault();

            // Change bool and save
            changebool.Onloan = false;
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        private bool OnloanExists(int id)
        {
            return _context.Onloan.Any(e => e.OnloanId == id);
        }
    }
}
