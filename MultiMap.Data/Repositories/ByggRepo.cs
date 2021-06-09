using Microsoft.EntityFrameworkCore;
using MultiMap.Data.IRepositories;
using MultiMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMap.Data.Repositories
{
    public class ByggRepo : IByggRepo
    {
        private readonly ApplicationDbContext _db;

        public ByggRepo(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IQueryable<Bygg> GetAll()
        {
            return _db.Byggs.Include(b => b.Lokasjon);
        }
        public async Task<Bygg> Get(int? id)
        {
            //return await _db.Byggs.FindAsync(id);
            return await _db.Byggs.Include(b => b.Lokasjon).FirstOrDefaultAsync(m => m.Id == id);

            //var bygg = await _context.Byggs.Include(b => b.Lokasjon).FirstOrDefaultAsync(m => m.Id == id);
        }       
        public async Task<Bygg> AddNew(Bygg newBygg)
        {
            try
            {
                _db.Byggs.Add(newBygg);
                await _db.SaveChangesAsync();
                return newBygg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<bool> Remove(int id)
        {
            var bygg = _db.Byggs.FirstOrDefault(x => x.Id == id);
            if (bygg == null)
            {
                return false;
            }
            try
            {
                _db.Byggs.Remove(bygg);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public async Task<Bygg> Update(int id, Bygg updateByg)
        {
            var bygg = _db.Byggs.FirstOrDefault(x => x.Id == id);
            if (bygg == null)
            {
                return null;
            }
            bygg.Navn = updateByg.Navn;
            bygg.Beskrivelse = updateByg.Beskrivelse;
            bygg.AntallEtasje = updateByg.AntallEtasje;
            bygg.Byggeår = updateByg.Byggeår;
            bygg.LokasjonId = updateByg.LokasjonId;
            bygg.Updated = DateTime.Now;
            try
            {
                await _db.SaveChangesAsync();
                return bygg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
