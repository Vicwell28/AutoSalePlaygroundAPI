using AutoSalePlaygroundAPI.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutoSalePlaygroundAPI.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("Controlador para gestionar los Autos")]
    public class VehicleController : ControllerBase
    {
        // Simulación de una base de datos en memoria
        private static readonly List<VehicleDto> Autos = new List<VehicleDto>
        {
            new VehicleDto { Id = 1, Marca = "Toyota", Modelo = "Corolla", Año = 2020, Precio = 20000 },
            new VehicleDto { Id = 2, Marca = "Honda", Modelo = "Civic", Año = 2019, Precio = 18000 }
        };

        /// <summary>
        /// Obtiene la lista completa de autos.
        /// </summary>
        /// <returns>Lista de autos.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los autos", Description = "Devuelve una lista completa de autos disponibles.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista de autos obtenida exitosamente", typeof(IEnumerable<VehicleDto>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public ActionResult<IEnumerable<VehicleDto>> GetAllAutos()
        {
            try
            {
                return Ok(Autos);
            }
            catch (Exception ex)
            {
                // Log error (no implementado aquí)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los autos.");
            }
        }

        /// <summary>
        /// Obtiene un auto por su ID.
        /// </summary>
        /// <param name="id">ID del auto.</param>
        /// <returns>VehicleDto específico.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un auto por ID", Description = "Devuelve los detalles de un auto específico según su ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "VehicleDto obtenido exitosamente", typeof(VehicleDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "VehicleDto no encontrado")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public ActionResult<VehicleDto> GetAutoById(int id)
        {
            try
            {
                var auto = Autos.FirstOrDefault(a => a.Id == id);

                if (auto == null)
                {
                    return NotFound($"VehicleDto con ID {id} no encontrado.");
                }

                return Ok(auto);
            }
            catch (Exception ex)
            {
                // Log error (no implementado aquí)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el auto.");
            }
        }

        /// <summary>
        /// Crea un nuevo auto.
        /// </summary>
        /// <param name="autoDto">Datos del nuevo auto.</param>
        /// <returns>VehicleDto creado.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo auto", Description = "Añade un nuevo auto a la base de datos.")]
        [SwaggerResponse(StatusCodes.Status201Created, "VehicleDto creado exitosamente", typeof(VehicleDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Datos de entrada inválidos")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public ActionResult<VehicleDto> CreateAuto([FromBody] VehicleDto autoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newAuto = new VehicleDto
                {
                    Id = Autos.Max(a => a.Id) + 1,
                    Marca = autoDto.Marca,
                    Modelo = autoDto.Modelo,
                    Año = autoDto.Año,
                    Precio = autoDto.Precio
                };

                Autos.Add(newAuto);

                return CreatedAtAction(nameof(GetAutoById), new { id = newAuto.Id }, newAuto);
            }
            catch (Exception ex)
            {
                // Log error (no implementado aquí)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el auto.");
            }
        }

        /// <summary>
        /// Actualiza un auto existente.
        /// </summary>
        /// <param name="id">ID del auto a actualizar.</param>
        /// <param name="autoDto">Nuevos datos del auto.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un auto", Description = "Modifica los detalles de un auto existente.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "VehicleDto actualizado exitosamente")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Datos de entrada inválidos")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "VehicleDto no encontrado")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public IActionResult UpdateAuto(int id, [FromBody] VehicleDto autoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingAuto = Autos.FirstOrDefault(a => a.Id == id);
                if (existingAuto == null)
                {
                    return NotFound($"VehicleDto con ID {id} no encontrado.");
                }

                existingAuto.Marca = autoDto.Marca;
                existingAuto.Modelo = autoDto.Modelo;
                existingAuto.Año = autoDto.Año;
                existingAuto.Precio = autoDto.Precio;

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log error (no implementado aquí)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el auto.");
            }
        }

        /// <summary>
        /// Elimina un auto por su ID.
        /// </summary>
        /// <param name="id">ID del auto a eliminar.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina un auto", Description = "Elimina un auto específico según su ID.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "VehicleDto eliminado exitosamente")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "VehicleDto no encontrado")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error interno del servidor")]
        public IActionResult DeleteAuto(int id)
        {
            try
            {
                var auto = Autos.FirstOrDefault(a => a.Id == id);
                if (auto == null)
                {
                    return NotFound($"VehicleDto con ID {id} no encontrado.");
                }

                Autos.Remove(auto);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log error (no implementado aquí)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el auto.");
            }
        }
    }
}
