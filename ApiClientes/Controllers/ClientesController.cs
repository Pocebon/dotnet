using System;
using System.Collections.Generic;
using ApiClientes.Database.Models;
using ApiClientes.Services;
using ApiClientes.Services.DTOs;
using ApiClientes.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly ClienteService _service;
        public ClientesController(ClienteService service)
        {
            _service = service;
        }


        [HttpPost]
        public ActionResult<ClienteDTO> Adicionar([FromBody] CriarClienteDTO body)
        {
            try
            {
                var Response = _service.Criar(body);
                return Ok(Response);
            }
            catch (BadRequestException B)
            {
                return BadRequest(B.Message);
            }
            catch (System.Exception E)
            {
                return StatusCode(500, E.Message);

            }
        }
        [HttpGet]

        public ActionResult<List<ClienteDTO>> GetAll()
        {
            var lista = _service.GetAll();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public ActionResult<ClienteDTO> GetById(int id)
        {
            try
            {
                var cliente = _service.GetById(id);
                return Ok(cliente);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent(); // Retorna 204
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ClienteDTO clienteDto)
        {
            try
            {
                _service.Update(id, clienteDto);
                return NoContent(); // 204 No Content
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


    }
}
