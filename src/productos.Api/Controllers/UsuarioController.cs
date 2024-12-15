using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using modelo_canonico.Types;
using productos.Api.Utils;
using productos.Application.Contracts;
using productos.Domain.DTO;
using productos.Domain.Responses;
using productos.Infraestructure.Configuration;

namespace productos.Api.Controllers
{
    [Route("api/" + General.NombreApi + "/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioContract _contract;
        private readonly TokenGenerate _token;

        public UsuarioController(ILogger<UsuarioController> logger, TokenGenerate token, IUsuarioContract contract)
        {
            _logger = logger;
            _token = token;
            _contract = contract;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(ClienteType), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult> Register([FromBody] ClienteType cliente)
        {
            try
            {
                _logger.LogInformation("Inicia metodo controller");
                if (string.IsNullOrEmpty(cliente.Username) || string.IsNullOrEmpty(cliente.Password)) return StatusCode(StatusCodes.Status400BadRequest, "Asegurese de ingresar los usuarios correctamente");
                bool res = await _contract.RegisterUser(cliente);
                return StatusCode(StatusCodes.Status200OK, res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en metodo controller");
                throw;
            }
            finally
            {
                _logger.LogInformation("Finaliza metodo controller");
            }
        }

        [HttpPost("auth")]
        [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult> Login([FromBody] AuthDTO auth)
        {
            try
            {
                _logger.LogInformation("Inicia metodo controller");
                if (string.IsNullOrEmpty(auth.Username) || string.IsNullOrEmpty(auth.Password)) return StatusCode(StatusCodes.Status400BadRequest, "Asegurese de ingresar los usuarios correctamente");
                ClienteType? type = await _contract.GetUsuarioData(auth.Username, auth.Password);
                string token = _token.Generate(type);
                return StatusCode(StatusCodes.Status200OK, token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en metodo controller");
                throw;
            }
            finally
            {
                _logger.LogInformation("Finaliza metodo controller");
            }
        }
    }
}
