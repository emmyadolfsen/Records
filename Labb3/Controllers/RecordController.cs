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
    public class RecordController : Controller
    {
        private readonly MusicContext _context;

        public RecordController(MusicContext context)
        {
            _context = context;
        }

        // GET: Record
        public async Task<IActionResult> Index(string searchString)
        {


            if (!String.IsNullOrEmpty(searchString))
            {
                var albums = from m in _context.Record
                             select m;

                albums = albums.Where(s => s.RecordName.ToLower().Contains(searchString.ToLower())).Include(o => o.Artist);
                return View(await albums.ToListAsync());
            }

            var musicContext = _context.Record.Include(o => o.Artist);
            return View(await musicContext.ToListAsync());


        }

        // GET: Record/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var @record = await _context.Record
                .Include(o => o.Artist)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (@record == null)
            {
                return NotFound();
            }

            // Check if album is loaned out
            if (@record.Onloan == true)
            {
                /*

                var albumid = onloan.RecordId; // Id of the borrowed album
                var changebool = _context.Record    // Match with id in database
                    .Where(p => p.RecordId == albumid)
                    .FirstOrDefault();
                */
                // Get name of friend
                var loan = _context.Onloan
                    .Where(m => m.RecordId == id)
                    .FirstOrDefault();
                ViewData["LoanedOut"] = loan.FriendName;
                ViewData["DateLoaned"] = loan.DateRegistered;
            }
            return View(@record);
        }

        // GET: Record/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistName");
            return View();
        }

        // POST: Record/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecordId,RecordName,Onloan,ArtistId")] Record record)
        {
            if (ModelState.IsValid)
            {
                // Check if album name in input and artist in input match existing album names in database
                // Send query to check Record table
                var albumname = _context.Record.Where(x => x.RecordName == record.RecordName && x.ArtistId == record.ArtistId).FirstOrDefault();
                
                if (albumname != null)
                {
                    ViewBag.Message = "Albumet finns redan inlagt";
                    ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistName");
                    return View();
                }

                // Add new record to database - name + artist
                _context.Add(record);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistName", record.ArtistId);
            return View(record);
        }

        // GET: Record/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @record = await _context.Record.FindAsync(id);
            if (@record == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistName", @record.ArtistId);
            return View(@record);
        }

        // POST: Record/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordId,RecordName,Onloan,ArtistId")] Record @record)
        {
            if (id != @record.RecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordExists(@record.RecordId))
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
            ViewData["ArtistId"] = new SelectList(_context.Artist, "ArtistId", "ArtistName", @record.ArtistId);
            return View(@record);
        }

        // GET: Record/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @record = await _context.Record
                .Include(o => o.Artist)
                .FirstOrDefaultAsync(m => m.RecordId == id);
            if (@record == null)
            {
                return NotFound();
            }

            return View(@record);
        }

        // POST: Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @record = await _context.Record.FindAsync(id);
            _context.Record.Remove(@record);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecordExists(int id)
        {
            return _context.Record.Any(e => e.RecordId == id);
        }

    }
}
