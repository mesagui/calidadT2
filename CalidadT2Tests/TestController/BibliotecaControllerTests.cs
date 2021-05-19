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
    class BibliotecaControllerTests
    {

        [Test]
        public void Index()
        {
            List<Biblioteca> Bibliotecas = new List<Biblioteca>();
           
            var usuarioMock = new Mock<IUsuarioRepository>();
            var libreriMock = new Mock<IBibliotecaRepository>();
            var cookieMock = new Mock<ICookieAuthService>();

            libreriMock.Setup(o => o.ListarLibro(null)).Returns(Bibliotecas);

            var bibliotecaController = new BibliotecaController(libreriMock.Object, usuarioMock.Object, cookieMock.Object);
            var guardarCom = bibliotecaController.Index();

            Assert.IsInstanceOf<ViewResult>(guardarCom);
        }

        [Test]
        public void Add()
        {
            List<Biblioteca> Bibliotecas = new List<Biblioteca>();
            
            var usuarioMock = new Mock<IUsuarioRepository>();
            var libreriaMock = new Mock<IBibliotecaRepository>();
            var cookieMock = new Mock<ICookieAuthService>();

            libreriaMock.Setup(o => o.GuardarLibro(null));
            usuarioMock.Setup(o => o.UserLogued(null)).Returns(new Usuario { Id = 100 });

            var bibliotecaController = new BibliotecaController(libreriaMock.Object, usuarioMock.Object, cookieMock.Object);
            var guardarCom = bibliotecaController.Add(5);

            Assert.IsInstanceOf<RedirectToActionResult>(guardarCom);
        }

        [Test]
        public void MarcarComoLeyendo()
        {
            List<Biblioteca> Bibliotecas = new List<Biblioteca>();
            
            var userMock = new Mock<IUsuarioRepository>();
            var libraryMock = new Mock<IBibliotecaRepository>();
            var cookMock = new Mock<ICookieAuthService>();

            userMock.Setup(o => o.UserLogued(null)).Returns(new Usuario { Id = 20 });
            libraryMock.Setup(o => o.DetalleLibro(null, 1)).Returns(new Biblioteca { Id = 1 });
            libraryMock.Setup(o => o.CambiarEstadoLibro(null, 1));

            var bibliotecaController = new BibliotecaController(libraryMock.Object, userMock.Object, cookMock.Object);
            var guardarCom = bibliotecaController.MarcarComoLeyendo(2);

            Assert.IsInstanceOf<RedirectToActionResult>(guardarCom);
        }

        

    }
}