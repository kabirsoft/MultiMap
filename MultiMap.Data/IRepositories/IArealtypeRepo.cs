using MultiMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMap.Data.IRepositories
{
    public interface IArealtypeRepo
    {
        IQueryable<Arealtype> GetAll();
        Task<Arealtype> Get(int? id);

        Task<Arealtype> AddNew(Arealtype newArealtype);
        Task<Arealtype> Update(int id, Arealtype updateArealtype);
        Task<bool> Remove(int id);
    }
}
