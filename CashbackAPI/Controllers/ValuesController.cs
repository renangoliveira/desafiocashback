using CashbackRepository;
using Microsoft.AspNetCore.Mvc;

namespace CashbackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        public readonly CashbackContext _context;
        public ValuesController(CashbackContext context)
        {
            _context = context;
        }

    }
}
