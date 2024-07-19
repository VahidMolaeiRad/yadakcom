using Domain.Interface;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomerDbContext _context;
        public UnitOfWork(CustomerDbContext context)
        {
             _context = context;
        }
        public void Dispose()
        {
            _context.Dispose(); 
        }

        public int saveChange()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
