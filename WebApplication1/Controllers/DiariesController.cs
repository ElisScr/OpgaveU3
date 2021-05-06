using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class DiariesController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public DiariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Diaries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Diaries.ToListAsync());
        }

        // GET: Diaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diary = await _context.Diaries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (diary == null)
            {
                return NotFound();
            }

            return View(diary);
        }

        // GET: Diaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DayNumber,Calories,GoodWeather,DailyThoughts")] Diary diary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diary);
        }

        // GET: Diaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diary = await _context.Diaries.FindAsync(id);
            if (diary == null)
            {
                return NotFound();
            }
            return View(diary);
        }

        // POST: Diaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DayNumber,Calories,GoodWeather,DailyThoughts")] Diary diary)
        {
            if (id != diary.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiaryExists(diary.ID))
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
            return View(diary);
        }

        // GET: Diaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diary = await _context.Diaries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (diary == null)
            {
                return NotFound();
            }

            return View(diary);
        }

        // POST: Diaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diary = await _context.Diaries.FindAsync(id);
            _context.Diaries.Remove(diary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiaryExists(int id)
        {
            return _context.Diaries.Any(e => e.ID == id);
        }
    }
}
