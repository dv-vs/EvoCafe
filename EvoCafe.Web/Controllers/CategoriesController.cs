using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EvoCafe.DAL;
using EvoCafe.DAL.Models;
using EvoCafe.DAL.Interfaces;

namespace EvoCafe.Controllers
{
    public class CategoriesController : Controller
    {
        IUnitOfWork _unitOfWork;
        IRepository<Category> _categoryRepo;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepo = _unitOfWork.Repository<Category>();
        }
        
        public async Task<ActionResult> Index()
        {
            return View(await _categoryRepo.GetAll().ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await _categoryRepo.GetSingleAsync(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Create(category);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await _categoryRepo.GetSingleAsync(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(category);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await _categoryRepo.GetSingleAsync(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Category category = await _categoryRepo.GetSingleAsync(id);
            var dishRepo = _unitOfWork.Dishes;

            dishRepo.DeleteRange(category.Dishes);
            _categoryRepo.Delete(category);

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
