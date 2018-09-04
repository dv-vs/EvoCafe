using EvoCafe.DAL.Interfaces;
using Menu.BLL;
using Menu.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EvoCafe.Controllers
{
    public class MenuController : Controller
    {
        readonly MenuService _menuService;
        readonly IUnitOfWork _unitOfWork;
            
        public MenuController(MenuService menuService, IUnitOfWork unitOfWork)
        {
            _menuService = menuService;
            _unitOfWork  = unitOfWork;
        }
        // GET: Menu
        public ActionResult Index()
        {
            var model = _menuService.GetMenuTemplate();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int[] IsChosen)
        {
            var model = new MenuCreateModel();
            if (ModelState.IsValid)
            {
                try
                {
                    var chosenDishes = _unitOfWork.Dishes.Get(x => IsChosen.Contains(x.Id)).ToList();
                    if (chosenDishes.Count == IsChosen.Length)
                    {
                        await _menuService.SaveMenu(chosenDishes);
                        model = _menuService.GetMenuTemplate();
                        (model.Messages as List<string>).Add("Все гуд, сохранено");
                    }
                    else
                        model.Errors.Append("Какой-то кривой список блюд - не все нашлось(");
                }
                catch (Exception e)
                {
                    model.Errors.Append(e.ToString());
                }

            }
            else
                model.Errors.Append("шота невалидно с моделькой(");

            return View("Index", model);
            
        }

        
    }
}
