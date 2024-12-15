using System;
using modelo_canonico.Types;

namespace productos.Application.Contracts;

public interface IUsuarioContract
{
    public Task<ClienteType> GetUsuarioData(string username, string password);
    public Task<bool> RegisterUser(ClienteType type);
}
