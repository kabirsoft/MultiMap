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
    public class EtasjeRepo : IEtasjeRepo
    {
        private readonly ApplicationDbContext _db;

        public EtasjeRepo(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IQueryable<Etasje> GetAll()
        {
            return _db.Etasjes.Include(e => e.Bygg);
        }
        public async Task<Etasje> Get(int? id)
        {
            return await _db.Etasjes.Include(e => e.Bygg).FirstOrDefaultAsync(m => m.Id == id);            
        }     
        public async Task<Etasje> AddNew(Etasje newEtasje)
        {
            try
            {
                _db.Etasjes.Add(newEtasje);
                await _db.SaveChangesAsync();
                return newEtasje;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public async Task<bool> Remove(int id)
        {
            var etasje = _db.Etasjes.FirstOrDefault(x => x.Id == id);
            if (etasje == null)
            {
                return false;
            }
            try
            {
                _db.Etasjes.Remove(etasje);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<Etasje> Update(int id, Etasje updateEtasje)
        {
            var etasje = _db.Etasjes.FirstOrDefault(x => x.Id == id);
            if (etasje == null)
            {
                return null;
            }
            etasje.Navn = updateEtasje.Navn;
            etasje.Beskrivelse = updateEtasje.Beskrivelse;
            etasje.ByggId = updateEtasje.ByggId;
            etasje.Updated = DateTime.Now;
            try
            {
                await _db.SaveChangesAsync();
                return etasje;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
