using MultiMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMap.Data.IRepositories
{
    public interface IEtasjeRepo
    {
        IQueryable<Etasje> GetAll();
        Task<Etasje> Get(int? id);

        Task<Etasje> AddNew(Etasje newEtasje);
        Task<Etasje> Update(int id, Etasje updateEtasje);
        Task<bool> Remove(int id);
    }
}
