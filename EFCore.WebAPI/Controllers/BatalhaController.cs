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
    public class BatalhaController : ControllerBase
    {
        public readonly HeroiContext _context;

        public BatalhaController(HeroiContext context)
        {
            _context = context;
        }

        // GET: api/Batalha
        [HttpGet]
        public ActionResult GetList()
        {
            try
            {
                var listBatalha = _context.Batalhas.ToList();
                return Ok(listBatalha);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: { ex } ");
            }
        }

        // GET: api/Batalha/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public ActionResult Get(int id)
        {
            try
            {
                var listBatalha = _context.Batalhas.Find(id);
                return Ok(listBatalha);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: { ex } ");
            }
        }

        // POST: api/Batalha
        [HttpPost]
        public ActionResult Post(Batalha model)
        {
            try
            {
                _context.Batalhas.Add(model);
                _context.SaveChanges();
                return Ok("Dados salvo com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // PUT: api/Batalha/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Batalha model)
        {
            try
            {
                if (_context.Batalhas.AsNoTracking().FirstOrDefault(h => h.Id == id) != null)
                {
                    _context.Update(model);
                    _context.SaveChanges();
                    return Ok("Dados atualizado com sucesso.");
                }
                else
                {
                    return Ok("Batalha não localizado...");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var batalha = _context.Batalhas
                .Where(x => x.Id == id)
                .Single();

                if (batalha != null)
                {
                    _context.Batalhas.Remove(batalha);
                    _context.SaveChanges();
                    return Ok("Dados deletado com sucesso.");
                }
                else
                {
                    return Ok("Batalha não localizado...");
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar os dados: {ex}");
            }
        }
    }
}
