using Api_Restful.Application;
using Api_Restful.Context;
using Api_Restful.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api_Restful.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApiContext _context;
        public UsuarioController(ApiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return Ok("Index API");
        }

        [HttpPost]
        [Route("InsertUser")]
        public IActionResult InsertUser([FromBody] Usuarios usuarioEnviado)
        {
            try
            {
                if (!ModelState.IsValid || usuarioEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new UsuarioApplication(_context).InsertUser(usuarioEnviado);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult UpdateUser([FromBody] Usuarios usuarioEnviado)
        {
            try
            {
                if (!ModelState.IsValid || usuarioEnviado == null)
                {
                    return BadRequest("Dados inválidos! Tente novamente.");
                }
                else
                {
                    var resposta = new UsuarioApplication(_context).UpdateUser(usuarioEnviado);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }
        [HttpPost]
        [Route("GetUserByEmail")]
        public IActionResult GetClienteByEmail([FromBody] string email)
        {
            try
            {
                if (email == string.Empty)
                {
                    return BadRequest("Email inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new UsuarioApplication(_context).GetUserByEmail(email);

                    if (resposta != null)
                    {
                        var usuarioResposta = JsonConvert.SerializeObject(resposta);
                        return Ok(usuarioResposta);
                    }
                    else
                    {
                        return BadRequest("Usuário não cadastrado!");
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllClientes()
        {
            try
            {
                var listaDeUsuarios = new UsuarioApplication(_context).GetAllUsers();

                if (listaDeUsuarios != null)
                {
                    var resposta = JsonConvert.SerializeObject(listaDeUsuarios);
                    return Ok(resposta);
                }
                else
                {
                    return BadRequest("Nenhum usuário cadastrado!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }

        [HttpDelete]
        [Route("DeleteUserByEmail")]
        public IActionResult DeleteUserByEmail([FromBody] string email)
        {
            try
            {
                if (email == string.Empty)
                {
                    return BadRequest("Email inválido! Tente novamente.");
                }
                else
                {
                    var resposta = new UsuarioApplication(_context).DeleteUserByEmail(email);
                    return Ok(resposta);
                }
            }
            catch (Exception)
            {
                return BadRequest("Erro ao comunicar com a base de dados!");
            }
        }
    }
}
