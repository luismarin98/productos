using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using modelo_canonico.Models;
using modelo_canonico.Parsing;
using modelo_canonico.Types;
using productos.Application.Contracts;
using productos.Infraestructure.Context;

namespace productos.Infraestructure.Repository;

public class UserRepository : IUsuarioContract
{
    private readonly DB _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(DB context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ClienteType> GetUsuarioData(string username, string password)
    {
        try
        {
            _logger.LogInformation("Finaliza metodo repository");
            ClienteModel? model = await _context.cliente.Where(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();
            if (model is null) _logger.LogInformation("Sin datos encontrados");
            return ParsingCliente.ModelToType(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en metodo repository");
            throw;
        }
        finally
        {
            _logger.LogInformation("Finaliza metodo repository");
        }
    }

    public async Task<bool> RegisterUser(ClienteType type)
    {
        try
        {
            _logger.LogInformation("Finaliza metodo repository");
            if (string.IsNullOrEmpty(type.Username) || string.IsNullOrEmpty(type.Password)) return false;
            ClienteModel? model = await _context.cliente.Where(x => x.Username!.Contains(type.Username) && x.Password!.Contains(type.Password)).FirstOrDefaultAsync();
            if (model is not null) return false;

            _context.cliente.Add(ParsingCliente.ModelToType(type));
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en metodo repository");
            throw;
        }
        finally
        {
            _logger.LogInformation("Finaliza metodo repository");
        }
    }
}
