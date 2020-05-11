using CashbackBusiness;
using CashbackDomain.Model;
using CashbackRepository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CashbackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly ICashbackRepository _repo;
        VendasBusiness vendasBusiness = new VendasBusiness();
        public VendasController(ICashbackRepository repo)
        {
            _repo = repo;
        }

        // GET api/values 
        [HttpGet("getVendasByUser/{username}")]
        public async Task<IActionResult> Get(string username)
        {
            try
            {
                var results = await _repo.GetAllVendasAsyncByRevendedor(username);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha de conexão com o Banco de Dados");
            }
        }

        // Post venda 
        [HttpPost()]
        public async Task<IActionResult> Post(Venda venda)
        {
            try
            {
                var retornoUser = _repo.GetRevendedorAsync(venda.Username);
                venda.CPF = retornoUser.Result.CPF;
                var X = new List<string>();
                var retornoValidacao = vendasBusiness.ValidarDadosVenda(venda);

                if (X.Count == retornoValidacao.Count)
                {
                    Venda vendaCalculadoCash = vendasBusiness.CashBackCalculadoStatus(venda);
                    _repo.Add(vendaCalculadoCash);

                    if (await _repo.SaveChangesAsync())
                    {
                        return Created($"getVendasByCPF/{venda.CodigoId}", venda);
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

        // Put venda
        [HttpPut("{CodigoId}")]
        public async Task<IActionResult> Put(int CodigoId , Venda vendaModel)
        {
            try
            {
                var venda = await _repo.GetVendasAsyncByID(CodigoId, vendaModel.Username);
                if (venda == null)
                    return NotFound();

                venda.Valor = vendaModel.Valor;
                venda.Data = vendaModel.Data;
                venda.CodigoId = vendaModel.CodigoId;

                if (venda.Status.Equals("Em validação"))
                {
                    Venda vendaCalculadoCash = vendasBusiness.CashBackCalculadoStatus(venda);
                    _repo.Update(vendaCalculadoCash);

                    if (await _repo.SaveChangesAsync())
                    {
                        return Created($"/api/vendas/{vendaModel.CodigoId}", vendaModel);
                    }
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status403Forbidden, "Não é possível editar um pedido com status de 'Aprovado'");
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha de conexão com o Banco de Dados");
            }

            return BadRequest();
        }

        // Delete venda
        [HttpDelete("{CodigoId}/{Username}")]
        public async Task<IActionResult> Delete(int CodigoId, string Username)
        {
            try
            {
                var venda = await _repo.GetVendasAsyncByID(CodigoId, Username);

                if (venda.Status.Equals("Em validação"))
                {
                    if (venda == null)
                        return NotFound();

                    _repo.Delete(venda);

                    if (await _repo.SaveChangesAsync())
                    {
                        return Ok();
                    }
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status403Forbidden, "Não é possível excluir um pedido com status de 'Aprovado'");
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha de conexão com o Banco de Dados");
            }

            return BadRequest();
        }

        [HttpGet("getCashbackAcumulado/{username}")]
        public ActionResult<Cashback> GetCashback(string username)
        {
            try
            {
                var retornoUser = _repo.GetRevendedorAsync(username);
                var CPF = retornoUser.Result.CPF;
                CPF = CPF.Replace(".", "");
                CPF = CPF.Replace("-", "");

                WebClient client = new WebClient();
                client.Headers.Add("Authentication-Token", "ZXPURQOARHiMc6Y0flhRC1LVlZQVFRnm");
                string cashBackFromApi = client.DownloadString("https://mdaqk8ek5j.execute-api.us-east-1.amazonaws.com/v1/cashback?cpf=" + CPF);

                dynamic obj = JsonConvert.DeserializeObject<dynamic>(cashBackFromApi);
                string status = obj["statusCode"].ToString();
                string credito = obj["body"]["credit"].ToString();
                decimal dCredito = decimal.Parse(credito);

                return new Cashback[] {
                    new Cashback ()
                    {
                        Status = status,
                        Credito = dCredito
                    },
                }.FirstOrDefault();
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha ao recuperar o valor total do Cashback");
            }
        }
    }
}
