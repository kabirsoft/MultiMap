using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMap.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Bygningsregister> Bygningsregisters { get; set; }
        public DbSet<Lokasjon> Lokasjons { get; set; }
        public DbSet<Bygg> Byggs { get; set; }
        public DbSet<Etasje> Etasjes { get; set; }
        public DbSet<BygningType> BygningTypes { get; set; }
        public DbSet<Arealtype> Arealtypes { get; set; }
    }
}
