using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OurSports1.Models;
using OurSports1.Data;

namespace OurSports1.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var webSportContext = _context.Article.Include(a => a.Author).Include(a => a.Category).OrderByDescending<Article,DateTime>(a=>a.TimeCreate);
           
            return View(await webSportContext.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Author)
                .SingleOrDefaultAsync(m => m.ID == id);

            var ai = (from Comment in _context.Comment select Comment).Where(m => m.ArticleID == id);
            ICollection<Comment> Comments = ai.ToArray<Comment>();

            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            var stands =
   _context.Author
     .Select(s => new
     {
         ID = s.ID,
         Description = s.AuthorName + "  (" + s.ID + ")"
     }).ToList();


            ViewData["AuthorID"] = new SelectList(stands, "ID", "Description");
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Title");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Secondary_title,Content,AuthorID,CategoryID,TimeCreate,Image")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var stands =
  _context.Author
    .Select(s => new
    {
        ID = s.ID,
        Description = s.AuthorName + "  (" + s.ID + ")"
    }).ToList();

            ViewData["AuthorID"] = new SelectList(stands, "ID", "Description");
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Title");
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.SingleOrDefaultAsync(m => m.ID == id);
            if (article == null)
            {
                return NotFound();
            }
            var stands =
   _context.Author
     .Select(s => new
     {
         ID = s.ID,
         Description = s.AuthorName + "  (" + s.ID + ")"
     }).ToList();


            ViewData["AuthorID"] = new SelectList(stands, "ID", "Description");
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Title");
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Secondary_title,Content,AuthorID,CategoryID,TimeCreate,Image")] Article article)
        {
            if (id != article.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ID))
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
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "ID", article.AuthorID);
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Title");
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Author)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Article.SingleOrDefaultAsync(m => m.ID == id);
            _context.Article.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.ID == id);
        }
    }
}
