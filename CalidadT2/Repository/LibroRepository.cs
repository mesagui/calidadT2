using CalidadT2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalidadT2.Repository
{

    public interface ILibroRepository
    {
        public List<Libro> ListarLibro();
        public Libro DetalleLibro(int id);
        public void PuntuacionLibro(Comentario comentario);
    }

    public class LibroRepository : ILibroRepository
    {
        private AppBibliotecaContext context;

        public LibroRepository(AppBibliotecaContext context)
        {
            this.context = context;
        }

        public List<Libro> ListarLibro()
        {
            
            var lst = context.Libros.Include(o => o.Autor).ToList();

            return lst;
        }

        public Libro DetalleLibro(int id)
        {
            var detLibro = context.Libros.Include("Autor")
                                         .Include("Comentarios.Usuario")
                                         .Where(o => o.Id == id)
                                         .FirstOrDefault();

            return detLibro;
        }

        public void PuntuacionLibro(Comentario comentario)
        {
            var libro = DetalleLibro(comentario.LibroId);
            
            libro.Puntaje = (libro.Puntaje + comentario.Puntaje) / 2;
            
            context.SaveChanges();
        }
    }
}
