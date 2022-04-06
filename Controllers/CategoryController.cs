using cosmetic_web.Data;
using cosmetic_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace cosmetic_web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        private IWebHostEnvironment _hostEnvironment;

       

        public CategoryController(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _hostEnvironment = environment;
        }
        public IActionResult Index([FromQuery]string? search)
        {
            IEnumerable<Category> productList;
            if (!string.IsNullOrWhiteSpace(search))
            {
                productList = _db.Categories.Where(x => x.Name.Contains(search)).ToList();
            }
            else
            {
                productList = _db.Categories.ToList();
            }
                            
          
            return View(productList);
        }
        //GET
        public IActionResult Create()
        {
           
            return View();
        }
        //Post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel obj)
        {
            if (obj.Name == obj.Price.ToString())
            {
                ModelState.AddModelError("CustomError", "The Price cannot exactly match the Name.");
            }
            
            if (!ModelState.IsValid)
            {
                this.HttpContext.Response.StatusCode = 400;

                return View(obj);
            }
            else
            {
                if(obj.Image != null)
                {
                    var currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images");
                    var fileName = Guid.NewGuid().ToString() + ".jpg";
                    var destinationPath = Path.Combine(currentDirectory, fileName );
                    using (var stream = System.IO.File.Create(destinationPath))
                    {
                        await obj.Image.CopyToAsync(stream);
                    }
                    
                     obj.ImagePath =  @"/images/" + fileName;
                }
            }
            _db.Categories.Add(obj.CreateModel());
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        //GET
        public IActionResult Edit(int? id) 
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb= _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.id == id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //Post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)

        {
            if (obj.Name == obj.Price.ToString())
            {
                ModelState.AddModelError("CustomError", "The Price cannot exactly match the Name.");


            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //Post 
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if(obj == null)
            {
                NotFound();     
            }
          
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
           
           
        }

        //GET
        public IActionResult addToBasket(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //Post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult addToBasket(Category obj)

        {
            if (obj.Name == obj.Price.ToString())
            {
                ModelState.AddModelError("CustomError", "The Price cannot exactly match the Name.");


            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
