using MultiMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMap.Data.IRepositories
{
    public interface IBygningsregisterRepo
    {
        IQueryable<Bygningsregister> GetAll();
        Task<Bygningsregister> Get(int? id);

        Task<Bygningsregister> AddNew(Bygningsregister newByg);
        Task<Bygningsregister> Update(int id, Bygningsregister updateByg);
        Task<bool> Remove(int id);
    }
}
