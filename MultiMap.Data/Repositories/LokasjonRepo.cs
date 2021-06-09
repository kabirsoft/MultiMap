using MultiMap.Data.IRepositories;
using MultiMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMap.Data.Repositories
{
    public class LokasjonRepo : ILokasjonRepo
    {
        private readonly ApplicationDbContext _db;

        public LokasjonRepo(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IQueryable<Lokasjon> GetAll()
        {
            return _db.Lokasjons;
        }
        public async Task<Lokasjon> Get(int? id)
        {
            return await _db.Lokasjons.FindAsync(id);
        }      
        public async Task<Lokasjon> AddNew(Lokasjon newLokasjon)
        {
            try
            {
                _db.Lokasjons.Add(newLokasjon);
                await _db.SaveChangesAsync();
                return newLokasjon;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }     

        public async Task<bool> Remove(int id)
        {
            var byg = _db.Lokasjons.FirstOrDefault(x => x.Id == id);
            if (byg == null)
            {
                return false;
            }
            try
            {
                _db.Lokasjons.Remove(byg);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<Lokasjon> Update(int id, Lokasjon updateLokasjon)
        {
            var lok = _db.Lokasjons.FirstOrDefault(x => x.Id == id);
            if (lok == null)
            {
                return null;
            }
            lok.Navn = updateLokasjon.Navn;
            lok.Beskrivelse = updateLokasjon.Beskrivelse;
            lok.Selskap = updateLokasjon.Selskap;
            lok.AntallBygg = updateLokasjon.AntallBygg;
            lok.Updated = DateTime.Now;
            try
            {
                await _db.SaveChangesAsync();
                return lok;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
