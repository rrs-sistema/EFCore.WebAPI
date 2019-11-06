using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IEFCoreRepository _repository;

        public BatalhaController(IEFCoreRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Batalha
        [HttpGet]
        public async Task<IActionResult> Get(bool incluirHeroi = true)
        {
            try
            {
                var listBatalha = await _repository.getAllBatalhas(incluirHeroi);
                return Ok(listBatalha);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: { ex } ");
            }
        }

        // GET: api/Batalha/5
        [HttpGet("{id}", Name = "GetBatalha")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var heroi = await _repository.getBatalhaById(id, true);
                return Ok(heroi);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST: api/Batalha
        [HttpPost]
        public async Task<IActionResult> Post(Batalha model)
        {
            try
            {
                _repository.Add(model);
                if(await _repository.SavesChangeAsync())
                    return Ok("Dados salvo com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex} ");
            }

            return Ok("Erro ao salvar os dados.");
        }

        // PUT: api/Batalha/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Batalha model)
        {
            try
            {
                var batalha = await _repository.getBatalhaById(id);
                if (batalha != null)
                {
                    _repository.Update(model);
                    if (await _repository.SavesChangeAsync())
                        return Ok("Dados atualizado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return Ok("Erro ao atualizar os dados.");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await _repository.getBatalhaById(id);
                if (model != null)
                {
                    _repository.Delete(model);
                    if (await _repository.SavesChangeAsync())
                        return Ok("Dados deletado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return Ok("Erro ao deletar os dados.");
        }
    }
}
