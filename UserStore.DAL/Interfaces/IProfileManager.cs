using EvoCafe.DAL.Models;
using System;

namespace UserStore.DAL.Interfaces
{
    public interface IProfileManager: IDisposable
    {
        void Create(Profile profile);
    }
}
