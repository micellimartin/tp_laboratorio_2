using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entidades;

namespace TestsUnitarios
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ListaPaquetesInstanciada()
        {
            //Arrange
            Correo auxCorreo;

            //Act
            auxCorreo = new Correo();

            //Assert
            Assert.IsNotNull(auxCorreo.Paquetes);
        }

        [TestMethod]
        [ExpectedException(typeof(TrackingIdRepetidoException))]
        public void ExcepcionPaquetesRepetidosLanzada()
        {
            //Arrange
            Correo auxCorreo = new Correo();

            Paquete p1 = new Paquete("Calle False 123", "1001001000");
            Paquete p2 = new Paquete("Cauce Boscoso 456", "1001001000");


            //Act
            auxCorreo += p1;
            auxCorreo += p2;

            //Assert
            //Lo maneja el ExpectedException
        }
    }
}
