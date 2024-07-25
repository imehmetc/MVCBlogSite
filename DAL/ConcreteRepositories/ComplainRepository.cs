using DAL.AbstractRepositories;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ConcreteRepositories
{
    public class ComplainRepository : IComplainRepository
    {
        private readonly AppDbContext _context;

        public ComplainRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> CountComplains(int id)
        {
           return await _context.Complains.CountAsync(x => x.PostId == id);
        }
    }
}
