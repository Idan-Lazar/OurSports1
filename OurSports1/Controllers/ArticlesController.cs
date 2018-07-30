using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OurSports1.Models;
using OurSports1.Data;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;

namespace OurSports1.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public List<Category> CategoryE;

        public List<Author> AuthorE;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
            CategoryE = _context.Category.ToList();
            AuthorE = _context.Author.ToList();


        }


        // GET: Articles
        public async Task<IActionResult> Index()
        {
          

            var webSportContext = _context.Article.Include(a => a.Author).Include(a => a.Category).OrderByDescending<Article, DateTime>(a => a.TimeCreate);
            return View(await webSportContext.ToListAsync());
        }
        public IActionResult SoccerEvents()
        {
            return View();
        }
        public async Task<IActionResult> IndexAll(String Month,String MonthSelect, String Year, String YearSelect, String Writer, String WriterSelect, string Category , String CategorySelect)
        {
            ViewData["Status"] = "true";
            
            ViewData["Result"] = "f";
            IEnumerable<String> Monthes = new List<String>(DateTimeFormatInfo.CurrentInfo.MonthNames);
            for (int year = 2009; year <= DateTime.Now.Year; year++)
                ViewData["Months"] = new SelectList(Monthes);
            var yeras = new List<String>();
            for (int year = 2016; year <= DateTime.Now.Year; year++)
            {
                yeras.Add(year.ToString());
            }
            ViewData["Years"] = new SelectList(yeras);
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Title");
            var stands =
      _context.Author
        .Select(s => new
        {
            ID = s.ID,
            Description = s.AuthorName + "  (" + s.ID + ")"
        }).ToList();


            ViewData["AuthorID"] = new SelectList(stands, "ID", "Description");
            var Articles = _context.Article.Include(a => a.Author).Include(a => a.Category).Select(a=>a);
            
            
            if (Month == "true")
            {
               
                Articles = Articles.Where(a => a.TimeCreate.ToString("MMMM", CultureInfo.InvariantCulture) == MonthSelect);
                

            }
            if(Year == "true")
            {
                Articles = Articles.Where(a => a.TimeCreate.Year.ToString() == YearSelect);
            }
            if(Writer=="true")
            {
                Articles = Articles.Where(a => a.AuthorID.ToString() == WriterSelect);
            }
            if(Category == "true")
            {
                Articles = Articles.Where(a => a.CategoryID.ToString() == CategorySelect);
            }
            if(Articles == _context.Article.Include(a => a.Author).Include(a => a.Category).Select(a => a))
            {
                ViewData["Result"] = "empty";
            }
           
           
            if(Articles.Count()==0)
            {
                ViewData["Status"] = "false";
                ViewData["Result"] = "There is no such article...";
               Articles= _context.Article.Include(a => a.Author).Include(a => a.Category).Select(a => a);
            }
            else if (ViewData["Result"].ToString() != "empty")
            {
                ViewData["Result"] = "We Found "+Articles.Count()+" Articles For You!";
            }
          
            var webSportContext = Articles.OrderByDescending<Article, DateTime>(a => a.TimeCreate);
          
            return View(await webSportContext.ToListAsync());
        }
        public async Task<IActionResult> SuggestedArticles(int id)
        {

            int cat = 0, aut = 0;
            var viewmodel = new ViewModle();
            double[][] Input = new double[1][];

            viewmodel.article = _context.Article.Find(id);



            cat = CategoryE.IndexOf(viewmodel.article.Category);
            aut = AuthorE.IndexOf(viewmodel.article.Author);

           

            Input[0] = new double[] { cat, aut };
            int[] x = Learning(Input, id);

            int prodcutid = x[0];
            //var viewmodel = new ViewModle();
            //viewmodel.Products = db.Products.Find(2);
            var result = _context.Article.Where(p => p.ID == prodcutid);
            return View(await result.ToListAsync());
        }


        public int[] Learning(double[][] Inputs, int id)
        {
            var articles = (from p in _context.Article
                            select p).ToArray();

            int cat = 0, aut = 0;
            int length = articles.Length;
            int lastID = articles[length - 1].ID;
            double[][] Input = new double[lastID + 1][];
            int[] output = new int[lastID + 1];
            for (int i = 0; i <= lastID; i++)
            {
                for (int j = 0; j < articles.Length; j++)
                {
                    if (articles[j].ID == i)
                    {
                        if (articles[j].ID != id) //skip the current item
                        {
                            if (CategoryE.Contains(articles[j].Category))
                            {
                                //Console.WriteLine(products[j].Resolution);
                                cat = CategoryE.IndexOf(articles[j].Category);
                              

                            }
                            if (AuthorE.Contains(articles[j].Author))
                            {
                                aut = AuthorE.IndexOf(articles[j].Author);
                            }


                            Input[i] = new double[] { cat, aut };
                            output[i] = articles[j].ID;
                            break;
                        }
                        else
                        { //outlier
                            Input[i] = new double[] { 20, 20 };
                            output[i] = id;
                        }
                    }
                    else
                    {
                        Input[i] = new double[] { 20, 20 };
                        output[i] = i;
                    }
                }
            }
            // Create the Multi-label learning algorithm for the machine
            var teacher = new MulticlassSupportVectorLearning<Linear>()
            {
                Learner = (p) => new SequentialMinimalOptimization<Linear>()
                {
                    Complexity = 10000.0 // Create a hard SVM
                }
            };

            // Learn a multi-label SVM using the teacher
            var svm = teacher.Learn(Input, output);


            // Compute the classifier answers for the given inputs
            int[] answers = svm.Decide(Inputs);

            return answers;
        }


        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Author).Include(c => c.Category).Include(co => co.Comments)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (article == null)
            {
                return NotFound();
            }






            return View(article);
        }

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        public ActionResult Statistics()
        {
            var buys = _context.Article.Include(b => b.Category).Include(b => b.Author);
            return View(buys.ToList());
        }

        public JsonResult CategoryGraph()
        {
            List<Result> salesCount = new List<Result>();


            var myGroup = from a in _context.Article
                          group a by a.Category.Title into cat
                          select new
                          {
                              CategoryName = cat.Key,
                              sum = cat.Count()
                          };

            foreach (var item in myGroup)
            {
                Result numReviews = new Result();
                numReviews.State = item.CategoryName;
                numReviews.freq = item.sum;
                salesCount.Add(numReviews);
            }
            return Json(salesCount);
        }


        public JsonResult AuthorGraph()
        {
            List<Result> salesCount = new List<Result>();


            var myQuery2 = from a in _context.Article
                           group a by a.Author.AuthorName into brand
                           select new
                           {
                               AuthorName = brand.Key,
                               sum = brand.Count()
                           };



            foreach (var item in myQuery2)
            {
                Result numReviews = new Result();
                numReviews.State = item.AuthorName;
                numReviews.freq = Convert.ToInt32(item.sum);
                salesCount.Add(numReviews);
            }
            return Json(salesCount);
        }

    }

    internal class Result
    {
        public String State { get; set; }
        public int freq { get; set; }

    }
}
