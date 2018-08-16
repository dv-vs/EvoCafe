using EvoCafe.DAL.Interfaces;
using EvoCafe.DAL.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EvoCafe.Controllers
{
    public class DishController : Controller
    {
        IUnitOfWork _unitOfWork;
        IRepository<Dish> _dishRepo;

        public DishController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dishRepo = _unitOfWork.Repository<Dish>();
        }
        // GET: Dish
        public ActionResult Index()
        {
            
            return View(_dishRepo.GetAll().OrderBy(x => x.Category.Name).ThenBy(x => x.Name).AsEnumerable());
        }

        // GET: Dish/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dish/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dish/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind(Include ="Id,Name,Description,Price,Category")]  Dish dish)
        {
            if (ModelState.IsValid)
            {
                _dishRepo.Create(dish);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            
            return View(dish);
            
        }

        // GET: Dish/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dish/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dish/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dish/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
