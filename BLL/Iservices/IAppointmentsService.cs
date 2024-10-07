using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Iservices
{
    public interface IAppointmentsService
    {
        public Task<List<DTO.AppointmentsDto>> SelectAllAsync();
        public Task AddAsync(DTO.AppointmentsDto p);
        public Task DeleteAsync(short id);
        public Task UpdateAsync(DTO.AppointmentsDto p, short id);
    }
}
