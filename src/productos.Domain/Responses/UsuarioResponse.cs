using System;
using modelo_canonico.Types;

namespace productos.Domain.Responses;

public class UsuarioResponse
{
    public string? Token { get; set; }
    public ClienteType? Cliente { get; set; }
}
