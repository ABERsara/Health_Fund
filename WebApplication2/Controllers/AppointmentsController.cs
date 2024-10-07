using BLL.Iservices;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : Controller
    {
        IAppointmentsService ips;
        public AppointmentsController(IAppointmentsService ips)
        {
            this.ips = ips;
        }
        [HttpGet]
        public async Task<List<AppointmentsDto>> GetAsync()
        {
            try
            {

                return await ips.SelectAllAsync();
            }
            catch (Exception ex) { throw; }
        }

        [HttpPost]
        public async Task AddAsync(DTO.AppointmentsDto p)
        {
            try
            {

                await ips.AddAsync(p);
            }
            catch (Exception ex) { throw; }

        }
        [HttpDelete]
        public async Task DeleteAsync(short id)
        {
            try
            {
                await ips.DeleteAsync(id);
            }
            catch (Exception ex) { throw; }

        }
        [HttpPut]
        public async Task UpdateAsync(DTO.AppointmentsDto p, short id)
        {
            try
            {
                await ips.UpdateAsync(p, id);
            }
            catch (Exception ex) { throw; }
        }



    }
}

