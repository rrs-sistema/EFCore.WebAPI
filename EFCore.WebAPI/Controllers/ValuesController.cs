﻿using System.Linq;
using EFCore.Dominio;
using EFCore.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly HeroiContext _context;
        public ValuesController(HeroiContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet("filtro/{nome}")]
        public ActionResult GetFiltro(string nome)
        {
            var listHeroi = _context.Herois
                            .Where(h => h.Nome.Contains(nome))
                            .ToList();
            return Ok(listHeroi);
        }

        // GET api/values/5
        [HttpGet("Atualizar/{nameHero}")]
        public ActionResult Get(string nameHero)
        {
            var heroi = _context.Herois
                            .Where(h => h.Id == 3)
                            .FirstOrDefault();
            heroi.Nome = nameHero;
            _context.SaveChanges();
            return Ok();
        }

        // GET api/values/5
        [HttpGet("AddRange")]
        public ActionResult GetAddRange()
        {
            _context.AddRange(
                new Heroi { Nome = "Capitão América" },
                new Heroi { Nome = "Doutor Estranho" },
                new Heroi { Nome = "Pantera Negra" },
                new Heroi { Nome = "Viúva Negra" },
                new Heroi { Nome = "Hulk" },
                new Heroi { Nome = "Gavião Arqueiro" },
                new Heroi { Nome = "Capitã Marvel" }
            );
            _context.SaveChanges();
            return Ok();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpGet("Delete/{id}")]
        public void Delete(int id)
        {
            var heroi = _context.Herois.Where(x => x.Id == id).Single();
            _context.Herois.Remove(heroi);
            _context.SaveChanges();
        }
    }
}
