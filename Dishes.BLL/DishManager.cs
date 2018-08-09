using EvoCafe.DAL;
using EvoCafe.DAL.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dishes.BLL
{
    public class DishManager
    {
        IUnitOfWork _unitOfWork;

        public DishManager()
        {
            IKernel kernel = new StandardKernel();
            _unitOfWork = kernel.Get<IUnitOfWork>(new Ninject.Parameters.ConstructorArgument("dbContext", kernel.Get<CafeContext>()));
        }
    }
}
