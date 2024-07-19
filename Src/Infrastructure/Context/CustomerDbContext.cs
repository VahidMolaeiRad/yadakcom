using Domain.Entity;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class CustomerDbContext :DbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public CustomerDbContext(DbContextOptions options):base(options) { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new CustomersConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
