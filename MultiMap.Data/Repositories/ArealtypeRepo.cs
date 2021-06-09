using MultiMap.Data.IRepositories;
using MultiMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMap.Data.Repositories
{
    public class ArealtypeRepo : IArealtypeRepo
    {
        private readonly ApplicationDbContext _db;
        public ArealtypeRepo(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IQueryable<Arealtype> GetAll()
        {
            return _db.Arealtypes;
        }
        public async Task<Arealtype> Get(int? id)
        {
            return await _db.Arealtypes.FindAsync(id);
        }       
        public async Task<Arealtype> AddNew(Arealtype newArealtype)
        {
            try
            {
                _db.Arealtypes.Add(newArealtype);
                await _db.SaveChangesAsync();
                return newArealtype;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }    

        public async Task<bool> Remove(int id)
        {
            var etasje = _db.Arealtypes.FirstOrDefault(x => x.Id == id);
            if (etasje == null)
            {
                return false;
            }
            try
            {
                _db.Arealtypes.Remove(etasje);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<Arealtype> Update(int id, Arealtype updateArealtype)
        {
            var atype = _db.Arealtypes.FirstOrDefault(x => x.Id == id);
            if (atype == null)
            {
                return null;
            }
            atype.Name = updateArealtype.Name;
            atype.Updated = DateTime.Now;
            try
            {
                await _db.SaveChangesAsync();
                return atype;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
