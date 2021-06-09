using MultiMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMap.Data.IRepositories
{
    public interface ILokasjonRepo
    {
        IQueryable<Lokasjon> GetAll();
        Task<Lokasjon> Get(int? id);

        Task<Lokasjon> AddNew(Lokasjon newLokasjon);
        Task<Lokasjon> Update(int id, Lokasjon updateByg);
        Task<bool> Remove(int id);
    }
}
