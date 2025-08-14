using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AOWebApp.Data;
using AOWebApp.Models;
using AOWebApp.ViewModel;

namespace AOWebApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AmazonOrders2025Context _context;

        public ItemsController(AmazonOrders2025Context context)
        {
            _context = context;
        }

        // GET: Items
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(ItemSearchViewModel isvm, string sortOrder)
        {
            #region  CategoriesQuery

            var Categories = _context.ItemCategories
                .Where(ic => ic.ParentCategoryId.HasValue)
                // OR .Where(ic => ic.ParentCategoryId == null)
                .OrderBy(ic => ic.CategoryName)
                .Select(ic => new
                {
                    ic.CategoryId,
                    ic.CategoryName,
                }).ToList();
            #endregion
            isvm.CategoryList = new SelectList(Categories, 
                nameof(ItemCategory.CategoryId),
                nameof(ItemCategory.CategoryName),
                isvm.CategoryId);

            #region ItemQuery
            //ViewBag.SearchText = isvm.SearchText;
            var amazonOrdersContext = _context.Items
                .Include(i => i.Category)
                .OrderBy(i => i.ItemName)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(isvm.SearchText))
            {
                amazonOrdersContext= amazonOrdersContext
                    .Where(i => i.ItemName.Contains(isvm.SearchText));
            }

            if(isvm.CategoryId != null)
            {
                amazonOrdersContext = amazonOrdersContext
                    .Where(i => i.Category.CategoryId == isvm.CategoryId);
                
            }
            #endregion

            switch (sortOrder)
            {
                case "name_desc":
                    amazonOrdersContext = amazonOrdersContext.OrderByDescending(i => i.ItemName);
                    break;
                case "price_asc":
                    amazonOrdersContext = amazonOrdersContext.OrderBy(i => i.ItemCost);
                    break;
                case "price_desc":
                    amazonOrdersContext = amazonOrdersContext.OrderByDescending(i => i.ItemCost);
                    break;
                default:
                    amazonOrdersContext = amazonOrdersContext.OrderBy(i => i.ItemName);
                    break;
            }

            isvm.ItemList = await amazonOrdersContext
                .Select(i => new ItemDetail
                {
                    TheItem = i,
                    ReviewCount = (i.Reviews != null ? i.Reviews.Count() : 0),
                    AverageRating = (i.Reviews != null && i.Reviews.Count() > 0 ? i.Reviews.Select(r => r.Rating).Average() : 0)
                }).ToListAsync();
            //var amazonOrdersContextVM = new ItemSearchViewModel
            //{
            //    SearchText = isvm.SearchText,
            //    CategoryId = isvm.CategoryId,
            //    CategoryList = new SelectList(Categories, nameof(ItemCategory.CategoryId), nameof(ItemCategory.CategoryName), isvm.CategoryId),
            //    ItemList = await amazonOrdersContext.ToListAsync()
            //};

            return View(isvm);

        }

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    var amazonOrders2025Context = _context.Items.Include(i => i.Category);
        //    return View(await amazonOrders2025Context.ToListAsync());
        //}

        //[HttpPost]
        //public async Task<IActionResult> Index(string? searchText)
        //{
        //    if (string.IsNullOrWhiteSpace(searchText))
        //    {
        //        return await Index();
        //    }

        //    var amazonDbContext = _context.Items
        //        .Include(i => i.Category)
        //        .Where(i => i.ItemName.Contains(searchText.ToLower()))
        //        .OrderBy(i => i.ItemName)
        //        .Select(i => i);
        //    return View(await amazonDbContext.ToListAsync());
        //}

        //public static string starStringCalc(IEnumerable<int> reviewScores)
        //{
        //    if (reviewScores.Count() == 0)
        //    {
        //        return "No data";
        //    }

        //    var reviewString = "";

        //    for (int i = 0; i < (int)Math.Round(reviewScores.Average()); i++)
        //    {
        //        reviewString += "☆";
        //    }
        //    return reviewString; 
        //}


        //public async Task<IActionResult> Index(string? searchText, int? categoryId)
        //{
        //    ViewData["formValue"] = searchText;

        //    var amazonDbContext = _context.Items.Include(i => i.Category).AsQueryable();

        //    var amazonDbCategories = _context.ItemCategories.Where(i => i.ParentCategoryId == null);

        //    ViewBag.Categories = new SelectList(amazonDbCategories.Select(i => i).ToList(),
        //        nameof(ItemCategory.CategoryId),
        //        nameof(ItemCategory.CategoryName),
        //        categoryId);

        //    if (!string.IsNullOrWhiteSpace(searchText))
        //    {
        //        amazonDbContext = amazonDbContext.Where(i => i.ItemName.Contains(searchText.ToLower()));
        //    }

        //    if (categoryId.HasValue)
        //    {
        //        amazonDbContext = amazonDbContext.Where(i => i.Category.CategoryId == categoryId || i.Category.ParentCategoryId == categoryId);

        //    }

        //    amazonDbContext = amazonDbContext.OrderBy(i => i.ItemName);

        //    var amazonDbContext2 = amazonDbContext
        //        .Select(i => new
        //        {
        //            itemName = i.ItemName,
        //            itemCost = i.ItemCost.ToString("C"),
        //            itemId = i.ItemId,
        //            itemDescription = i.ItemDescription.Length > 100 ? i.ItemDescription.Substring(0, 100).Trim() + "..." : i.ItemDescription,
        //            itemImage = i.ItemImage,
        //            itemReviews = i.Reviews.Count(),
        //            itemStars = starStringCalc(i.Reviews.Select(i => i.Rating)),
        //        });

        //    return View(await amazonDbContext2.ToListAsync());

        //}



        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
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
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
