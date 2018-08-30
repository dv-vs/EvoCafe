using EvoCafe.DAL.Interfaces;
using Menue.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Menue.BLL
{
    public class MenuService
    {
        readonly IUnitOfWork _unitOfWork;

        public MenuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MenuCreateModel> GetMenuTemplate()
        {
            var currentDate = DateTime.Now.Date;
            var currentMenu = _unitOfWork.Menues.Get(x => x.CreatedAt == currentDate).SingleOrDefault();
            if (currentMenu == null)
                return _unitOfWork.Dishes.GetAll().Select(x => new MenuCreateModel { IsChosen = false, Dish = x});

            return from allDish in _unitOfWork.Dishes.GetAll().AsEnumerable()
                      join chosenDish in currentMenu.ActualDishes on allDish equals chosenDish into gj
                      from x in gj.DefaultIfEmpty()
                      select new MenuCreateModel { IsChosen = x != null, Dish = allDish };
             
        }
    }
}
