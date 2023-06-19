using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ofline_Health.Models;

namespace Ofline_Health.Data
{
    public class Ofline_HealthContext : DbContext
    {
        public Ofline_HealthContext (DbContextOptions<Ofline_HealthContext> options)
            : base(options)
        {
        }

        public DbSet<Ofline_Health.Models.Patient> Patient { get; set; } = default!;

        public DbSet<Ofline_Health.Models.Doctor> Doctor { get; set; } = default!;

        public DbSet<Ofline_Health.Models.Prescription> Prescriptions { get; set; } = default!;



    }
}
