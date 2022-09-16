using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaEpik.Api.Data;
using PruebaTecnicaEpik.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaTecnicaEpik.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneroController : Controller
    {
        private readonly DataContext _context;

        public GeneroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
        {
            return Ok(await _context.Generos.ToListAsync());
        }
    }
}
