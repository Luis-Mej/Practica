using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dtos;
using Dtos.ProductosDTOS;
using Dtos.UsuariosDTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica.Context;
using Practica.Models;

namespace Negocio.Servicios
{
    public class ProductosServicios
    {
        private readonly PracticaContext _context;

        public ProductosServicios(PracticaContext context)
        {
            _context = context;
        }

        public async Task<ResponseBase<List<ProductoDTO>>> GetProductosDTO()
        {
            var listaProductos = await _context.Productos.Select(x => new ProductoDTO()
            {
                Nombre = x.Nombre,
                Precio = x.Precio,
                Stock = x.Stock,
            }).ToListAsync();

            return new ResponseBase<List<ProductoDTO>>(200, listaProductos);
        }

        public async Task<ResponseBase<ProductoDTO>> PostProductosDTO(ProductoDTO productoDTO)
        {
            var productoExiste = _context.Productos.FirstOrDefaultAsync(x => x.Nombre.ToUpper().Trim() == productoDTO.Nombre.ToUpper().Trim());
            if (productoExiste != null)
            {
                return new ResponseBase<ProductoDTO>(400, "Producto ya existe");
            }

            Producto productoRegistro = new Producto(productoDTO.Nombre, productoDTO.Precio, productoDTO.Stock);
            _context.Productos.Add(productoRegistro);
            return new ResponseBase<ProductoDTO>(200, "Producto ingresado");
        }

        public async Task<ResponseBase<ProductoDTO>> PutProductosDTO(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return new ResponseBase<ProductoDTO>(400, "El producto no existe");
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new ResponseBase<ProductoDTO>(200, "Producto actualizado");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return new ResponseBase<ProductoDTO>(400, "El producto no existe");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
