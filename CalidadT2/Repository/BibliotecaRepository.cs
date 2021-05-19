using CalidadT2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.Repository
{

    public interface IBibliotecaRepository
    {
        public List<Biblioteca> ListarLibro(Usuario user);
        public Biblioteca GuardarLibro(Biblioteca biblioteca);
        public Biblioteca DetalleLibro(Usuario usuario, int libroId);
        public void CambiarEstadoLibro(Biblioteca biblioteca, int nuevoEstado);
    }

    public class BibliotecaRepository : IBibliotecaRepository
    {
        private AppBibliotecaContext context;

        public BibliotecaRepository(AppBibliotecaContext context)
        {
            this.context = context;
        }

        public List<Biblioteca> ListarLibro(Usuario user) {
            
            var lst = context.Bibliotecas
                                    .Include(o => o.Libro.Autor)
                                    .Include(o => o.Usuario)
                                    .Where(o => o.UsuarioId == user.Id)
                                    .ToList();

            return lst;
        }

        public Biblioteca GuardarLibro(Biblioteca biblioteca) {
            
            context.Bibliotecas.Add(biblioteca);
            context.SaveChanges();
            
            return biblioteca;
        }

        public Biblioteca DetalleLibro(Usuario usuario, int libroId)
        {
            var detaLibro = context.Bibliotecas.Where(o => o.LibroId == libroId && o.UsuarioId == usuario.Id)
                .FirstOrDefault();

            return detaLibro;
        }

        public void CambiarEstadoLibro(Biblioteca biblioteca, int nuevoEstado)
        {
            biblioteca.Estado = nuevoEstado;
            
            context.SaveChanges();
        }
    }
}
