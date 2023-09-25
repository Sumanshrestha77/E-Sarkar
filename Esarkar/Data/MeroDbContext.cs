using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Esarkar.Models;

    public class MeroDbContext : DbContext
    {
        public MeroDbContext (DbContextOptions<MeroDbContext> options)
            : base(options)
        {
        }

        public DbSet<Esarkar.Models.RequestModel> RequestModel { get; set; } = default!;
    }
