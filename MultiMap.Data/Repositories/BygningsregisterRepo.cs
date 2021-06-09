using MultiMap.Data.IRepositories;
using MultiMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMap.Data.Repositories
{
    public class BygningsregisterRepo : IBygningsregisterRepo
    {
        private readonly ApplicationDbContext _db;
        public BygningsregisterRepo(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IQueryable<Bygningsregister> GetAll()
        {
            return _db.Bygningsregisters;
        }
        public async Task<Bygningsregister> Get(int? id)
        {
            return await _db.Bygningsregisters.FindAsync(id);
        }       
        
        public async Task<Bygningsregister> AddNew(Bygningsregister newByg)
        {
            try
            {
                _db.Bygningsregisters.Add(newByg);
                await _db.SaveChangesAsync();
                return newByg;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool> Remove(int id)
        {
            var byg = _db.Bygningsregisters.FirstOrDefault(x => x.Id == id);
            if(byg == null)
            {
                return false;
            }
            try
            {
                _db.Bygningsregisters.Remove(byg);
                await _db.SaveChangesAsync();
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public async Task<Bygningsregister> Update(int id, Bygningsregister updateByg)
        {
            var byg = _db.Bygningsregisters.FirstOrDefault(x => x.Id == id);
            if(byg == null)
            {
                return null;
            }
            byg.Lokasjon = updateByg.Lokasjon;
            byg.AntallBygning = updateByg.AntallBygning;
            byg.Areal = updateByg.Areal;
            byg.Objektype = updateByg.Objektype;
            byg.Snittalder = updateByg.Snittalder;
            byg.Beskrivelse = updateByg.Beskrivelse;
            byg.Tomtareal = updateByg.Tomtareal;
            byg.Bilde = updateByg.Bilde;
            try
            {
                await _db.SaveChangesAsync();
                return byg;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
    }
}
