using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using modelo_canonico.Types;
using productos.Application.Contracts;
using productos.Domain.Responses;
using productos.Infraestructure.Configuration;

namespace productos.Api.Controllers
{
    [Route("api/" + General.NombreApi + "/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductosContract _contract;
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(IProductosContract contract, ILogger<ProductoController> logger)
        {
            _contract = contract;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult> GetAllProductos()
        {
            try
            {
                _logger.LogInformation("Inicia metodo controller");
                List<ProductoType> ListaProd = await _contract.GetAllProductos();
                if (ListaProd.Count is 0) return StatusCode(StatusCodes.Status404NotFound, "Sin datos registrados");
                ProductoResponse res = new ProductoResponse { TotalRegistros = ListaProd.Count, Producto = ListaProd };
                return StatusCode(StatusCodes.Status200OK, res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el metodo controller");
                throw;
            }
            finally
            {
                _logger.LogInformation("Finaliza metodo controller");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductoType), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult> GetProducto(int id)
        {
            try
            {
                _logger.LogInformation("Inicia metodo controller");
                ProductoType prod = await _contract.GetProducto(id);
                if (prod is null) return StatusCode(StatusCodes.Status404NotFound, "Sin datos registrados");
                return StatusCode(StatusCodes.Status200OK, prod);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el metodo controller");
                throw;
            }
            finally
            {
                _logger.LogInformation("Finaliza metodo controller");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult> CreateProducto(ProductoType productoType)
        {
            try
            {
                _logger.LogInformation("Inicia metodo controller");
                bool res = await _contract.CreateProducto(productoType);
                if (res is false) return StatusCode(StatusCodes.Status400BadRequest, "No se pudo procesar la solicitud");
                return StatusCode(StatusCodes.Status200OK, res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el metodo controller");
                throw;
            }
            finally
            {
                _logger.LogInformation("Finaliza metodo controller");
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult> UpdateProducto(ProductoType productoType)
        {
            try
            {
                _logger.LogInformation("Inicia metodo controller");
                bool res = await _contract.UpdateProducto(productoType);
                if (res is false) return StatusCode(StatusCodes.Status400BadRequest, "No se pudo procesar la solicitud");
                return StatusCode(StatusCodes.Status200OK, res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el metodo controller");
                throw;
            }
            finally
            {
                _logger.LogInformation("Finaliza metodo controller");
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<ActionResult> DeleteProducto(int id)
        {
            try
            {
                _logger.LogInformation("Inicia metodo controller");
                bool res = await _contract.DeleteProducto(id);
                if (res is false) return StatusCode(StatusCodes.Status400BadRequest, "No se pudo procesar la solicitud");
                return StatusCode(StatusCodes.Status200OK, res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el metodo controller");
                throw;
            }
            finally
            {
                _logger.LogInformation("Finaliza metodo controller");
            }
        }
    }
}
