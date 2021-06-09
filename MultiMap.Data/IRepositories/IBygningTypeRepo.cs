using MultiMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMap.Data.IRepositories
{
    public interface IBygningTypeRepo
    {
        IQueryable<BygningType> GetAll();
        Task<BygningType> Get(int? id);

        Task<BygningType> AddNew(BygningType newBygningType);
        Task<BygningType> Update(int id, BygningType updateBygningType);
        Task<bool> Remove(int id);
    }
}
