using EmployeeLogix.Server.Reposatories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLogix.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DasboardController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DasboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData()
        {
            var result = await _dashboardRepository.GetDashboardData();
            return Ok(result);
        }
    }
}
