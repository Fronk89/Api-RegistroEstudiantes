using ApiPrueba.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace ApiPrueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        
        public class AlumnosController : ControllerBase
        {
            private readonly BdRegistroEscolarContext _context;

            public AlumnosController (BdRegistroEscolarContext context)
            {
                _context = context;
            }

            [HttpPost]
            [Route("GuardarRegistro")]
            public async Task<IActionResult> GuardarRegistro([FromBody] Alumno alumno)
            {
                try
                {
                    var parameters = new[]
                    {
                    new SqlParameter("@Name", SqlDbType.NVarChar) { Value = alumno.Name },
                    new SqlParameter("@LastName", SqlDbType.NVarChar) { Value = alumno.LastName },
                    new SqlParameter("@Nacimiento", SqlDbType.Date) { Value = alumno.Nacimiento },
                    new SqlParameter("@Sexo", SqlDbType.Bit) { Value = alumno.Sexo },
                    new SqlParameter("@TelefonoPadres", SqlDbType.NVarChar) { Value = alumno.TelefonoPadres },
                    new SqlParameter("@IdCurso", SqlDbType.Int) { Value = alumno.CursoId }
                };

                    await _context.Database.ExecuteSqlRawAsync("EXEC GuardarRegistroAlumnos @Name, @LastName, @Nacimiento, @Sexo, @TelefonoPadres, @IdCurso", parameters);

                    return Ok(new { message = "Alumno registrado exitosamente." });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = $"Error al guardar el alumno: {ex.Message}" });
                }
            }

        [HttpGet("ObtenerDatosEstudiantes")]
        public async Task<IActionResult> ObtenerDatosEstudiantes()
        {
            try
            {
                var resultados = new List<object>();

                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "ObtenerDatosEstudiantes";
                        command.CommandType = CommandType.StoredProcedure;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var resultado = new
                                {
                                    AlumnoId = reader["AlumnoId"],
                                    NombreAlumno = reader["NombreAlumno"],
                                    ApellidoAlumno = reader["ApellidoAlumno"],
                                    Nacimiento = reader["Nacimiento"],
                                    Sexo = reader["Sexo"],
                                    TelefonoPadres = reader["TelefonoPadres"],
                                    CursoId = reader["CursoId"],
                                    NombreCurso = reader["NombreCurso"],
                                    MaestroId = reader["MaestroId"],
                                    NombreMaestro = reader["NombreMaestro"],
                                    ApellidoMaestro = reader["ApellidoMaestro"]
                                };
                                resultados.Add(resultado);
                            }
                        }
                    }
                }

                return Ok(resultados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener datos de los estudiantes.", error = ex.Message });
            }
        }
        [HttpPut("ActualizarEstudiante/{id}")]
        public async Task<IActionResult> ActualizarEstudiante(int id, [FromBody] Alumno alumno)
        {
            try
            {
                if (alumno == null)
                {
                    return BadRequest(new { message = "El cuerpo de la solicitud no puede estar vacío." });
                }

                var parameters = new[]
                {
            new SqlParameter("@AlumnoId", id),
            new SqlParameter("@Name", alumno.Name),
            new SqlParameter("@LastName", alumno.LastName),
            new SqlParameter("@Nacimiento", alumno.Nacimiento),
            new SqlParameter("@Sexo", alumno.Sexo),
            new SqlParameter("@TelefonoPadres", alumno.TelefonoPadres),
            new SqlParameter("@CursoId", alumno.CursoId)
        };

                var result = await _context.Database.ExecuteSqlRawAsync("EXEC ActualizarEstudiante @AlumnoId, @Name, @LastName, @Nacimiento, @Sexo, @TelefonoPadres, @CursoId", parameters);

                if (result == 0)
                {
                    return NotFound(new { message = "No se encontró el estudiante para actualizar." });
                }

                return Ok(new { message = "Estudiante actualizado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el estudiante.", error = ex.Message });
            }
        }

        [HttpDelete("EliminarEstudiante/{id}")]
        public async Task<IActionResult> EliminarEstudiante(int id)
        {
            try
            {
                var parameters = new[]
                {
            new SqlParameter("@AlumnoId", id)
        };

                // Ejecutar el procedimiento almacenado para eliminar al alumno
                var result = await _context.Database.ExecuteSqlRawAsync("EXEC EliminarAlumno @AlumnoId", parameters);

                if (result == 0)
                {
                    return NotFound(new { message = "No se encontró el estudiante para eliminar." });
                }

                return Ok(new { message = "Estudiante eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el estudiante.", error = ex.Message });
            }
        }

        [HttpGet("ObtenerEstudiante/{id}")]
        public async Task<IActionResult> ObtenerEstudiante(int id)
        {
            try
            {
                var resultados = new List<object>();

                using (var connection = _context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "ObtenerEstudiante"; // Procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Añadir el parámetro del ID del alumno
                        var parameter = new SqlParameter("@AlumnoId", SqlDbType.Int) { Value = id };
                        command.Parameters.Add(parameter);

                        // Ejecutar el procedimiento y leer los resultados
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var resultado = new
                                {
                                    AlumnoId = reader["AlumnoId"],
                                    NombreAlumno = reader["NombreAlumno"],
                                    ApellidoAlumno = reader["ApellidoAlumno"],
                                    Nacimiento = reader["Nacimiento"],
                                    Sexo = reader["Sexo"],
                                    TelefonoPadres = reader["TelefonoPadres"],
                                    CursoId = reader["CursoId"],
                                    NombreCurso = reader["NombreCurso"],
                                    MaestroId = reader["MaestroId"],
                                    NombreMaestro = reader["NombreMaestro"],
                                    ApellidoMaestro = reader["ApellidoMaestro"]
                                };
                                resultados.Add(resultado);
                            }
                        }
                    }
                }

                // Si no hay resultados, se responde con un mensaje
                if (resultados.Count == 0)
                {
                    return NotFound(new { message = "Estudiante no encontrado." });
                }

                // Devolver los resultados encontrados
                return Ok(resultados);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return StatusCode(500, new { message = "Error al obtener el estudiante.", error = ex.Message });
            }
        }

    }
}
    

