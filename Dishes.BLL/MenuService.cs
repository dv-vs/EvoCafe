using EvoCafe.DAL.Interfaces;
using EvoCafe.DAL.Models;
using Menu.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Menu.BLL
{
    public class MenuService
    {
        readonly IUnitOfWork _unitOfWork;

        public MenuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private EvoCafe.DAL.Models.Menu GetCurrentMenu()
        {
            var currentDate = DateTime.Now.Date;
            return _unitOfWork.Menues.Get(x => x.CreatedAt == currentDate).SingleOrDefault();
        }

        public MenuCreateModel GetMenuTemplate()
        {
            var currentMenu = GetCurrentMenu();
            if (currentMenu == null)
                return new MenuCreateModel(_unitOfWork.Dishes.GetAll("Category").Select(x => new MenuDishes { IsChosen = false, Dish = x}).ToList());

            return new MenuCreateModel((from allDish in _unitOfWork.Dishes.GetAll("Category")
                      join chosenDish in currentMenu.ActualDishes on allDish equals chosenDish into gj
                      from x in gj.DefaultIfEmpty()
                      select new MenuDishes { IsChosen = x != null, Dish = allDish }).ToList());
             
        }

        public async Task SaveMenu(IEnumerable<Dish> dishes)
        {
            var currentMenu = GetCurrentMenu();
            if (currentMenu == null)
                currentMenu = new EvoCafe.DAL.Models.Menu();

            currentMenu.ActualDishes = dishes.ToList();
            _unitOfWork.Menues.Update(currentMenu);
            await _unitOfWork.SaveChangesAsync();
        }
        
    }
}
