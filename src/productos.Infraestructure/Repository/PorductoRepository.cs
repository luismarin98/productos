using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using modelo_canonico.Models;
using modelo_canonico.Parsing;
using modelo_canonico.Types;
using productos.Application.Contracts;
using productos.Infraestructure.Context;

namespace productos.Infraestructure.Repository;

public class PorductoRepository : IProductosContract
{
    private readonly DB _context;
    private readonly ILogger<PorductoRepository> _logger;

    public PorductoRepository(DB context, ILogger<PorductoRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> CreateProducto(ProductoType productoType)
    {
        try
        {
            _logger.LogInformation("Inicia Metodo Repository");
            if (productoType is null) _logger.LogInformation("Sin datos en la variable");
            ProductoModel? model = await _context.producto.Where(x => x.Nombre!.Contains(productoType!.Nombre!)).FirstOrDefaultAsync();
            if (model is not null) _logger.LogInformation("Ya existe un producto similar con esas caracteristicas");
            _context.producto.Add(ParsingProducto.ModelToType(productoType));
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
            _logger.LogInformation("Finaliza Metodo Repository");
        }
    }

    public async Task<bool> DeleteProducto(int id)
    {
        try
        {
            _logger.LogInformation("Inicia Metodo Repository");
            ProductoModel? model = await _context.producto.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (model is null) _logger.LogInformation("No existe un producto similar con esas caracteristicas");
            _context.producto.Remove(model!);
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
            _logger.LogInformation("Finaliza Metodo Repository");
        }
    }

    public async Task<List<ProductoType>> GetAllProductos()
    {
        try
        {
            _logger.LogInformation("Inicia Metodo Repository");
            List<ProductoModel> ListModel = await _context.producto.ToListAsync();
            if (ListModel.Count is 0) _logger.LogInformation("Lista vacia");
            return ParsingProducto.ModelToType(ListModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en metodo repository");
            throw;
        }
        finally
        {
            _logger.LogInformation("Finaliza Metodo Repository");
        }
    }

    public async Task<ProductoType> GetProducto(int id)
    {
        try
        {
            _logger.LogInformation("Inicia Metodo Repository");
            ProductoModel? model = await _context.producto.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (model is null) _logger.LogInformation("No existe un producto similar con esas caracteristicas");
            return ParsingProducto.ModelToType(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en metodo repository");
            throw;
        }
        finally
        {
            _logger.LogInformation("Finaliza Metodo Repository");
        }
    }

    public async Task<bool> UpdateProducto(ProductoType productoType)
    {
        try
        {
            _logger.LogInformation("Inicia Metodo Repository");
            ProductoModel? model = await _context.producto.Where(x => x.Id == productoType.Id).FirstOrDefaultAsync();
            if (model is null) _logger.LogInformation("No existe un producto similar con esas caracteristicas");
            model = ParsingProducto.ModelToType(productoType);
            _context.producto.Update(model);
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
            _logger.LogInformation("Finaliza Metodo Repository");
        }
    }
}
