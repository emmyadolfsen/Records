using System;
using System.ComponentModel;
using Labb3.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3.Data
{
    public class MusicContext : DbContext
    {
        // Constructor with options
        public MusicContext(DbContextOptions<MusicContext> options) : base(options)
        {
        }

        // Prop for tables
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Record> Record { get; set; }
        public DbSet<Onloan> Onloan { get; set; }

    }
}

