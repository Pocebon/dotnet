using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using ApiClientes.Database.Models;
using ApiClientes.Services.DTOs;
using ApiClientes.Services.Parsers;
using ApiClientes.Services.Validations;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ApiClientes.Services
{
    public class ClienteService
    {

        private readonly ClientesContext _dbcontext;


        public ClienteService(ClientesContext dbcontext)
        { 
            _dbcontext = dbcontext;

        }

        public ClienteDTO Criar(CriarClienteDTO dto)
        {
            ClienteValidation.ValidouCriarCliente(dto);

            TbCliente novoCliente =
                ClienteParser.ToTbCliente(dto);
         
            _dbcontext.TbClientes.Add(novoCliente);
            _dbcontext.SaveChanges();

            return ClienteParser.ToClienteDTO(novoCliente);

        }

        public List <ClienteDTO>GetAll()
        {
          return _dbcontext.TbClientes.Select(c => ClienteParser.ToClienteDTO(c)).ToList();
        }

        public ClienteDTO GetById(int id)
        {
           TbCliente cliente = _dbcontext.TbClientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
                throw new Exception ("Cliente não encontrado.");

            ClienteDTO clienteDTO = ClienteParser.ToClienteDTO(cliente);

            return (clienteDTO);
        }
        public void Delete(int id)
        {
            TbCliente cliente = _dbcontext.TbClientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            _dbcontext.TbClientes.Remove(cliente);
            _dbcontext.SaveChanges();
        }


        public void Update(int id, ClienteDTO clienteDto)
        {
            TbCliente clienteExistente = _dbcontext.TbClientes.FirstOrDefault(c => c.Id == id);

            if (clienteExistente == null)
                throw new Exception("Cliente não encontrado.");

            // Atualize os campos conforme seu DTO
            clienteExistente.Nome = clienteDto.Nome;
            clienteExistente.Documento = clienteDto.Documento;
            clienteExistente.Telefone = clienteDto.Telefone;
            clienteExistente.Tipodoc = clienteDto.Tipodoc;
            // Adicione os demais campos aqui conforme necessário

            _dbcontext.SaveChanges();
        }

        //automapper
        //fluent validator


    }
}
