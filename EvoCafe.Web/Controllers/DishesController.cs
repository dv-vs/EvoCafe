using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using EvoCafe.DAL.Models;
using EvoCafe.DAL.Interfaces;

namespace EvoCafe.Controllers
{
    public class DishesController : Controller
    {
        IUnitOfWork _unitOfWork;
        IRepository<Dish> _dishRepo;

        public DishesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dishRepo = _unitOfWork.Repository<Dish>();
        }
            // GET: Dishes
        public async Task<ActionResult> Index()
        {
            return View(await _dishRepo.GetAll().OrderBy(x => x.Category.Name).ThenBy(x => x.Name).ToListAsync());
        }

        // GET: Dishes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = await _dishRepo.GetSingleAsync(id.Value);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // GET: Dishes/Create
        public ActionResult Create()
        {
            var categoriesRepo = _unitOfWork.Repository<Category>();
            ViewBag.CategoryId = new SelectList(categoriesRepo.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,Image,Price,CategoryId")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                _dishRepo.Create(dish);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var categoriesRepo = _unitOfWork.Repository<Category>();
            ViewBag.CategoryId = new SelectList(categoriesRepo.GetAll(), "Id", "Name", dish.CategoryId);
            return View(dish);
        }

        // GET: Dishes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = await _dishRepo.GetSingleAsync(id.Value);
            if (dish == null)
            {
                return HttpNotFound();
            }

            var categoriesRepo = _unitOfWork.Repository<Category>();
            ViewBag.CategoryId = new SelectList(categoriesRepo.GetAll(), "Id", "Name", dish.CategoryId);
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Image,Price,CategoryId")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(dish).State = EntityState.Modified;
                _dishRepo.Update(dish);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            var categoriesRepo = _unitOfWork.Repository<Category>();
            ViewBag.CategoryId = new SelectList(categoriesRepo.GetAll(), "Id", "Name", dish.CategoryId);
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = await _dishRepo.GetSingleAsync(id.Value);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _dishRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
