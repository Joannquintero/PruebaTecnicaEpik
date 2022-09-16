using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaEpik.Api.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PruebaTecnicaEpik.Models.Entities;
using PruebaTecnicaEpik.Models.Models.Request;
using PruebaTecnicaEpik.Api.Enumerations;

namespace PruebaTecnicaEpik.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : Controller
    {
        private readonly DataContext _context;

        public PersonaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonaRequest>>> GetPersonas()
        {
            List<Persona> personas = await _context.Personas
                .Include(x => x.Genero)
                .OrderBy(x => x.Id).ToListAsync();

            List<PersonaRequest> data = new List<PersonaRequest>();
            data.AddRange(
                (from c in personas
                 select new PersonaRequest
                 {
                     Id = c.Id,
                     Identificacion = c.Identificacion,
                     Nombres = c.Nombres,
                     Apellidos = c.Apellidos,
                     Edad = c.Edad,
                     GeneroId = c.Genero.Id

                 }).ToList());

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersona(PersonaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Models.Entities.Genero genero = await _context.Generos.FindAsync(request.GeneroId);
            if (genero == null)
                return BadRequest("La genero no existe.");

            Persona persona = new Persona
            {
                Identificacion = request.Identificacion,
                Nombres = request.Nombres,
                Apellidos = request.Apellidos,
                Edad = request.Edad,
                Genero = genero
            };

            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
            return Ok(persona);
        }

        [HttpPut("{identificacion}/{edad}")]
        public async Task<ActionResult<Persona>> UpdateEdadByIdentificacion(long identificacion, int edad)
        {
            if (identificacion == 0)
                return BadRequest();

            if (edad == 0)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Persona persona = await _context.Personas.FirstOrDefaultAsync(x => x.Identificacion == identificacion);
            if (persona == null)
                return BadRequest("La persona no existe.");

            persona.Edad = edad;

            try
            {
                _context.Personas.Update(persona);
                await _context.SaveChangesAsync();
                return persona;
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("Get/{Identificacion}")]
        public async Task<ActionResult<Persona>> GetPersonaByIdentificacion(long identificacion)
        {
            Persona persona = await _context.Personas
                .FirstOrDefaultAsync(x => x.Identificacion == identificacion);

            if (persona == null)
                return NotFound();

            return persona;
        }

        [HttpGet("GetGenero/{GeneroId}")]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonasByGenero(int generoId)
        {
            List<Persona> personas = await _context.Personas
             .Where(x => x.Genero.Id == generoId)
             .ToListAsync();
            return personas;
        }

        [HttpDelete("{identificacion}")]
        public async Task<ActionResult<Persona>> DeletePersona(long identificacion)
        {
            Persona persona = await _context.Personas.FirstOrDefaultAsync(x => x.Identificacion == identificacion);
            if (persona == null)
                return NotFound();

            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();

            return persona;
        }
    }
}
