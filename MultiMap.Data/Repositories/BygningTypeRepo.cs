using MultiMap.Data.IRepositories;
using MultiMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMap.Data.Repositories
{
    public class BygningTypeRepo : IBygningTypeRepo
    {
        private readonly ApplicationDbContext _db;

        public BygningTypeRepo(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IQueryable<BygningType> GetAll()
        {
            return _db.BygningTypes;
        }
        public async Task<BygningType> Get(int? id)
        {
            return await _db.BygningTypes.FindAsync(id);
        }       
        public async Task<BygningType> AddNew(BygningType newBygningType)
        {
            try
            {
                _db.BygningTypes.Add(newBygningType);
                await _db.SaveChangesAsync();
                return newBygningType;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<bool> Remove(int id)
        {
            var etasje = _db.BygningTypes.FirstOrDefault(x => x.Id == id);
            if (etasje == null)
            {
                return false;
            }
            try
            {
                _db.BygningTypes.Remove(etasje);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<BygningType> Update(int id, BygningType updateBygningType)
        {
            var btype = _db.BygningTypes.FirstOrDefault(x => x.Id == id);
            if (btype == null)
            {
                return null;
            }
            btype.Navn = updateBygningType.Navn;           
            btype.Updated = DateTime.Now;
            try
            {
                await _db.SaveChangesAsync();
                return btype;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
