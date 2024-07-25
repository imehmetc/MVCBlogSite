using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AbstractRepositories
{
    public interface IComplainRepository
    {
        Task<int> CountComplains(int id);
    }
}
