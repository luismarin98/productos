using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pruebaTecnicaProductos.Modelos;
using pruebaTecnicaProductos.Modelos.Dtos;
using pruebaTecnicaProductos.Repositorio.IRepositorio;

namespace pruebaTecnicaProductos.Controllers
{
    [Authorize(Roles = "Admin")]
    [AllowAnonymous]
    [Route("api/productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepositorio _Irepo;
        private readonly IMapper _mapper;

        public ProductoController(IProductoRepositorio irepo, IMapper imapper)
        {
            _Irepo = irepo;
            _mapper = imapper;

        }

        [HttpGet]
        public IActionResult GetProductos()
        {
            try
            {
                var productos = _Irepo.GetProductos();
                var productosDto = new List<ProductoDto>();
                foreach (var item in productos) {
                    productosDto.Add(_mapper.Map<ProductoDto>(item));
                }

                return Ok(productosDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos");

            }
        }

        [HttpGet("{id:int}", Name = "GetPelicula")]
        [AllowAnonymous]    //permite que no necesite autenticacion
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPelicula(int id)
        {
            var itemPelicula = _Irepo.GetProducto(id);
            if (itemPelicula == null)
            {
                return NotFound();
            }

            var itemPeliculaDto = _mapper.Map<ProductoDto>(itemPelicula);
            return Ok(itemPeliculaDto);

        }


        [HttpPost]
        
        [ProducesResponseType(201, Type = typeof(ProductoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CrearProducto([FromBody] CrearProductoDto crearProductoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (crearProductoDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_Irepo.ExisteProducto(crearProductoDto.nombre!))
            {
                ModelState.AddModelError("", $"La pelicula ya existe");
                return StatusCode(404, ModelState);
            }

            var producto = _mapper.Map<Producto>(crearProductoDto);
            _Irepo.CrearProducto(producto);
            return CreatedAtRoute("GetPelicula", new { id = producto.Id }, producto);
        }


            [HttpPut]
            [Route("{productoId:int}")]
            public IActionResult ActualizarProducto([FromRoute] int productoId, [FromBody] ActualizarProductoDto actualizarProductoDto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (actualizarProductoDto == null)
                {
                    return BadRequest("El cuerpo de la solicitud no puede ser nulo.");
                }

                var existingProducto = _Irepo.Existeproducto(productoId);
                if (!existingProducto)
                {
                    return NotFound("Pelicula no encontrada.");
                }
                var producto = _mapper.Map<Producto>(actualizarProductoDto);




                _Irepo.ActualizarProducto(producto);
                return NoContent();
            }

            [HttpDelete]
            [Route("{id:int}")]
            public IActionResult BorrarPelicula([FromRoute] int id)
            {
                var existePelicula = _Irepo.GetProducto(id);
                if (existePelicula == null)
                {
                    return NotFound();
                }

                if (!_Irepo.Borrarproducto(existePelicula))
                {
                    ModelState.AddModelError("", $"No se pudo borrar la pelicula: {existePelicula.nombre}");
                    return StatusCode(500, ModelState);
                }
                return Ok();

            }



            [HttpGet]
            [AllowAnonymous]    //permite que no necesite autenticacion
            [Route("Buscar/{search}")]
            public IActionResult Buscar([FromRoute] string search)
            {
                if (string.IsNullOrEmpty(search))
                {
                    return BadRequest();
                }
                var productos = _Irepo.BuscarProducto(search);

                var productosDto = productos.Select(pelicula => _mapper.Map<ProductoDto>(productos)).ToList();

                return Ok(productosDto);
            }
        } 
}
