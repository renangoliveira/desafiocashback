using CashbackBusiness;
using CashbackDomain.Model;
using CashbackRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashbackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevendedorController : ControllerBase
    {
        private readonly ICashbackRepository _repo;
        RevendedorBusiness revendedorBusiness = new RevendedorBusiness();

        public RevendedorController(ICashbackRepository repo)
        {            
            _repo = repo;
        }

        // GET api/values 
        [HttpGet("getRevendedor/{cpf}")]
        public async Task<IActionResult> Get(string cpf)
        {
            try
            {
                var results = await _repo.GetRevendedorAsync(cpf);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha de conexão com o Banco de Dados");
            }
        }

        // GET api/values
        [HttpGet("validaUser/{cpf}")]
        public async Task<IActionResult> ValidaUser(string cpf)
        {
            try
            {
                var results = await _repo.GetRevendedorAsync(cpf);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha de conexão com o Banco de Dados");
            }
        }

        // Post revendedor
        [HttpPost()]
        public async Task<IActionResult> Post(Revendedor revendedor)
        {
            try
            {
                var X = new List<string>();
                var retornoValidacao = revendedorBusiness.ValidarDadosRevendedor(revendedor);

                if (X.Count == retornoValidacao.Count)
                {
                    _repo.Add(revendedor);

                    if (await _repo.SaveChangesAsync())
                    {
                        return Created($"/api/revendedor/getRevendedor/{revendedor.CPF}", revendedor);
                    }
                }
                else
                {
                    throw new System.Exception("Falha na validação de Dados");
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha de conexão com o Banco de Dados");
            }
            return BadRequest();
        }

        // Put revendor
        [HttpPut()]
        public async Task<IActionResult> Put(Revendedor revendedorModel)
        {
            try
            {
                var revendedor = await _repo.GetRevendedorAsyncByCPF(revendedorModel.CPF);

                if (revendedor == null)
                    return NotFound();

                var X = new List<string>();
                var retornoValidacao = revendedorBusiness.ValidarDadosRevendedor(revendedorModel);

                if (X.Count == retornoValidacao.Count)
                {
                    _repo.Update(revendedorModel);
                    
                    if (await _repo.SaveChangesAsync())
                    {
                        return Created($"/api/revendedor/getRevendedor/{revendedor.CPF}", revendedor);
                    }
                }                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha de conexão com o Banco de Dados");
            }

            return BadRequest();
        }

        // Delete Revendedor
        [HttpDelete("{cpf}")]
        public async Task<IActionResult> Delete(string cpf)
        {
            try
            {                
                var revendedor = await _repo.GetRevendedorAsyncByCPF(cpf);

                if (revendedor == null)
                    return NotFound();

                _repo.Delete(revendedor);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha de conexão com o Banco de Dados");
            }

            return BadRequest();
        }
    }
}
