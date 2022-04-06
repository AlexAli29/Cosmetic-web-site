using cosmetic_web.Data;
using cosmetic_web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace cosmetic_web.Controllers
{
    public class BasketController : Controller
    {
        private readonly ILogger<BasketController> _logger;
        private readonly ApplicationDbContext _db;

        public BasketController(ILogger<BasketController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var basketItems = await _db.BasketItems
                .Include(x => x.Product)
                .OrderBy(x => x.Sold)
                .Select(x => new BasketItemViewModel
                {
                    ProductId = x.Product.Id,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    Count=x.Count,
                    ImagePath = x.Product.ImagePath,
                    Sold = x.Sold
                })
                .ToArrayAsync();

            return View(basketItems);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            var basketItem = await _db.BasketItems.Where(x => x.ProductId == id && !x.Sold).FirstOrDefaultAsync();

            if(basketItem != null)
            {
                basketItem.Count++;
            }
            else
            {
                var product = await _db.Categories.FindAsync(id);
                _db.BasketItems.Add(new BasketItem {Product = product, Count = 1, Sold = false });
            }
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Category");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ActionName("Delete")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult>  DeletePOST(int? id)
        {
            var obj = await _db.BasketItems.Where(x => x.ProductId == id).SingleAsync();
            if (obj == null)
            {
               return NotFound();
            }

            _db.BasketItems.Remove(obj);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Purchase(int? id)
        {
            var basketItem = await _db.BasketItems.Where(x => x.ProductId == id && !x.Sold).SingleAsync();

            if (basketItem == null)
            {
                return NotFound();
            }
            else
            {
                basketItem.Sold = true;
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Clear()
        {
            await _db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [BasketItems]");

            return RedirectToAction("Index");
        }

    }
}