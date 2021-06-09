using MultiMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMap.Data.IRepositories
{
    public interface IByggRepo
    {
        IQueryable<Bygg> GetAll();
        Task<Bygg> Get(int? id);

        Task<Bygg> AddNew(Bygg newBygg);
        Task<Bygg> Update(int id, Bygg updateBygg);
        Task<bool> Remove(int id);
    }
}
