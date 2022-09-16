using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PruebaTecnicaEpik.Api.Data;
using PruebaTecnicaEpik.Models.Entities;
using PruebaTecnicaEpik.Models.Models.Request;
using PruebaTecnicaEpik.Web.Models;
using PruebaTecnicaEpik.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace PruebaTecnicaEpik.Web.Controllers
{
    public class PersonasController : Controller
    {
        private readonly IApiService _apiService;

        public PersonasController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ActionResult> Index()
        {
            Response response = await _apiService.GetListAsync<PersonaViewModel>("https://localhost:44339", "/api", $"/Persona/");
            if (!response.IsSuccess)
                return View(new List<PersonaViewModel>());

            List<PersonaViewModel> personas = (List<PersonaViewModel>)response.Result;
            Response generoResponse = await _apiService.GetListAsync<Genero>("https://localhost:44339", "/api", $"/Genero/");
            List<Genero> generos = (List<Genero>)generoResponse.Result;
            ViewBag.Generos = generos;

            List<PersonaViewModel> data = new List<PersonaViewModel>();
            data.AddRange(
                (from c in personas
                 select new PersonaViewModel
                 {
                     Id = c.Id,
                     Identificacion = c.Identificacion,
                     Nombres = c.Nombres,
                     Apellidos = c.Apellidos,
                     Edad = c.Edad,
                     Genero = generos.Where(x => x.Id == c.GeneroId).FirstOrDefault()
                 }).ToList());

            return View(data);
        }

        public async Task<ActionResult> Create()
        {
            Response generoResponse = await _apiService.GetListAsync<Genero>("https://localhost:44339", "/api", $"/Genero/");
            ViewBag.Generos = (List<Genero>)generoResponse.Result;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonaViewModel model)
        {
            try
            {
                PersonaRequest modelRequest = new PersonaRequest
                {
                    Identificacion = model.Identificacion,
                    Nombres = model.Nombres,
                    Apellidos = model.Apellidos,
                    Edad = model.Edad,
                    GeneroId = model.Genero.Id
                };
                Response response = await _apiService.CreatePersonaAsync("https://localhost:44339", "/api", "/Persona", modelRequest);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Response response = await _apiService.GetAsync<Persona>("https://localhost:44339", "/api", $"/Persona/Get/{id}");
            if (response == null)
                return NotFound();

            Persona persona = JsonConvert.DeserializeObject<Persona>(response.Result.ToString());
            return View(new PersonaViewModel
            {
                Identificacion = (long)id,
                Edad = persona.Edad
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PersonaViewModel model)
        {
            try
            {
                PersonaRequest modelRequest = new PersonaRequest
                {
                    Id = model.Id,
                    Edad = model.Edad,
                };
                Response response = await _apiService.PutAsync("https://localhost:44339", "/api", $"/Persona/{model.Identificacion}/{model.Edad}", modelRequest);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<JsonResult> FiltrarGenero(int? id)
        {
            Response response = await _apiService.GetListAsync<PersonaViewModel>("https://localhost:44339", "/api", $"/Persona/GetGenero/{id}");
            List<PersonaViewModel> personas = (List<PersonaViewModel>)response.Result;

            Response generoResponse = await _apiService.GetListAsync<Genero>("https://localhost:44339", "/api", $"/Genero/");
            List<Genero> generos = (List<Genero>)generoResponse.Result;

            List<PersonaViewModel> data = new List<PersonaViewModel>();
            data.AddRange(
                (from c in personas
                 select new PersonaViewModel
                 {
                     Id =c.Id,
                     Identificacion = c.Identificacion,
                     Nombres= c.Nombres,
                     Apellidos = c.Apellidos,
                     Edad = c.Edad,
                     Genero = generos.Where(x => x.Id == id.Value).FirstOrDefault()
                 }).ToList());

            return Json(data);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Response response = await _apiService.DeleteAsync("https://localhost:44339", "/api", $"/Persona/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
