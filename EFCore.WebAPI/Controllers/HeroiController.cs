using System;
using System.Linq;
using EFCore.Dominio;
using EFCore.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroiController : ControllerBase
    {
        public readonly HeroiContext _context;

        public HeroiController(HeroiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetList()
        {
            try
            {
                var listHeroi = _context.Herois.ToList();
                return Ok(listHeroi);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: { ex } ");
            }
        }

        [HttpGet("{id}", Name = "GetHeroi")]
        public ActionResult Get(int id)
        {
            try
            {
                var listHeroi = _context.Herois.Find(id);
                return Ok(listHeroi);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: { ex } ");
            }
        }

        [HttpPost]
        public ActionResult Post(Heroi model)
        {
            try
            {
                _context.Herois.Add(model);
                _context.SaveChanges();
                return Ok("Dados salvo com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Heroi model)
        {
            try
            { 
                if(_context.Herois.AsNoTracking().FirstOrDefault(h => h.Id == id) != null)
                {
                    _context.Update(model);
                    _context.SaveChanges();
                    return Ok("Dados atualizado com sucesso.");
                }
                else
                {
                    return Ok("Heroi não localizado...");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar os dados: {ex}");
            }
        }

        [HttpGet("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var heroi = _context.Herois
                .Where(x => x.Id == id)
                .Single();

                if (heroi != null)
                {
                    _context.Herois.Remove(heroi);
                    _context.SaveChanges();
                    return Ok("Dados deletado com sucesso.");
                }
                else
                {
                    return Ok("Heroi não localizado...");
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar os dados: {ex}");
            }
        }
    }
}
