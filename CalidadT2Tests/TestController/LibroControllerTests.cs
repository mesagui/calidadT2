using CalidadT2.Controllers;
using CalidadT2.Models;
using CalidadT2.Repository;
using CalidadT2.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalidadT2Tests.Controllers
{
    class LibroControllerTests
    {

        [Test]
        public void Index()
        {
            List<Biblioteca> Bibliotecas = new List<Biblioteca>();
            var usuarioMock = new Mock<IUsuarioRepository>();
            var libreriaMock = new Mock<IBibliotecaRepository>();
            var libroMock = new Mock<ILibroRepository>();
            var cookieMock = new Mock<ICookieAuthService>();
            var comentarioMock = new Mock<IComentarioRepository>();

            libroMock.Setup(o => o.DetalleLibro(4)).Returns(new Libro { Id = 1 });

            var libroController = new LibroController(usuarioMock.Object, comentarioMock.Object, libroMock.Object, cookieMock.Object);
            var guardarCom = libroController.Details(5);

            Assert.IsInstanceOf<ViewResult>(guardarCom);
        }


        [Test]
        public void AddComentario()
        {
            List<Biblioteca> Bibliotecas = new List<Biblioteca>();
            var usuarioMock = new Mock<IUsuarioRepository>();
            var libreriaMock = new Mock<IBibliotecaRepository>();
            var libroMock = new Mock<ILibroRepository>();
            var cookieMock = new Mock<ICookieAuthService>();
            var comentarioMock = new Mock<IComentarioRepository>();

            usuarioMock.Setup(o => o.UserLogued(null)).Returns(new Usuario { Id = 50 });
            comentarioMock.Setup(o => o.SaveComentrario(null));
            libroMock.Setup(o => o.PuntuacionLibro(null));

            var libroController = new LibroController(usuarioMock.Object, comentarioMock.Object, libroMock.Object, cookieMock.Object);
            var guardarCom = libroController.AddComentario(new Comentario { Puntaje = 1, Texto = "", LibroId = 2 });

            Assert.IsInstanceOf<RedirectToActionResult>(guardarCom);
        }
    }
}