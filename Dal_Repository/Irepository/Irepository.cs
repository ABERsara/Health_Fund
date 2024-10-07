using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal_Repository.Irepository
{
    public interface Irepository<T>
    {
        public Task<List<T>> SelectAllAsync();

        public Task AddAsync(T ap);

        public Task DeleteAsync(short id);

        public Task UpdateAsync(T p, short id);
    }
}
