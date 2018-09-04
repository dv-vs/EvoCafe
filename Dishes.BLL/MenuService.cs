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

            var chosenDishes = currentMenu.ActualDishes.ToList();

            var allAndSelectedDishes = (from allDish in _unitOfWork.Dishes.GetAll("Category").ToList()
                                        join chosenDish in chosenDishes on allDish.Id equals chosenDish.Id into gj
                                        from x in gj.DefaultIfEmpty()
                                        select new MenuDishes { IsChosen = x != null, Dish = allDish }).ToList();

            return new MenuCreateModel(allAndSelectedDishes);
             
        }

        public async Task SaveMenu(IEnumerable<Dish> dishes)
        {
            var currentMenu = GetCurrentMenu();
            if (currentMenu != null)
            {
                currentMenu.ActualDishes.Clear();
                _unitOfWork.Menues.Delete(currentMenu);

                await _unitOfWork.SaveChangesAsync();
            }

            currentMenu = new EvoCafe.DAL.Models.Menu();
            currentMenu.CreatedAt = DateTime.Now.Date;
            foreach (var dish in dishes)
                currentMenu.ActualDishes.Add(dish);
            //currentMenu.ActualDishes.ToList().AddRange(dishes);
            _unitOfWork.Menues.Create(currentMenu);

            await _unitOfWork.SaveChangesAsync();
        }
        
    }
}
