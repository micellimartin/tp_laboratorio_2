using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Archivos;
using Clases_Instanciables;
using EntidadesAbstractas;
using Excepciones;

namespace TestsUnitarios
{
    [TestClass]
    public class Pruebas
    {
        /// <summary>
        /// Verifica que se lance la excepcion DniInvalidoException cuando se genera un alumno argentino con un dni con formato invalido
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DniInvalidoException))]
        public void TestMethodDniInvladioException()
        {
            //Arrage && Act
            //Dni excede el lenght permitido
            Alumno alumno = new Alumno(1, "Tanjiro", "Kamado", "850100100", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio, Alumno.EEstadoCuenta.AlDia);

            //Assert es manejado en el ExpectedException  
        }

        #region NacionalidadInvalidaException

        /// <summary>
        /// Verfica que se lance la excepcion NacionalidadInvalidaException cuando se crea un alumno argentino con un dni que no se corresponde con la nacionalidad
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NacionalidadInvalidaException))]
        public void TestMethodNacionalidadInvalidaException()
        {
            //Arrage && Act
            //Dni argentino no puede ser menor a 1
            Alumno alumno = new Alumno(1, "Tanjiro", "Kamado", "0", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio, Alumno.EEstadoCuenta.AlDia);

            //Assert es manejado en el ExpectedException  
        }

        /// <summary>
        /// Verfica que se lance la excepcion NacionalidadInvalidaException cuando se crea un alumno argentino con un dni que no se corresponde con la nacionalidad
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NacionalidadInvalidaException))]
        public void TestMethodNacionalidadInvalidaException2()
        {
            //Arrage && Act
            //Dni argentino no puede ser mayor a 89999999
            Alumno alumno = new Alumno(1, "Tanjiro", "Kamado", "90000000", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio, Alumno.EEstadoCuenta.AlDia);

            //Assert es manejado en el ExpectedException  
        }

        /// <summary>
        /// Verfica que se lance la excepcion NacionalidadInvalidaException cuando se crea un alumno extranjero con un dni que no se corresponde con la nacionalidad
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NacionalidadInvalidaException))]
        public void TestMethodNacionalidadInvalidaException3()
        {
            //Arrage && Act
            //Dni extranjero no puede ser menor a 90000000
            Alumno alumno = new Alumno(1, "Tanjiro", "Kamado", "89000000", Persona.ENacionalidad.Extranjero, Universidad.EClases.Laboratorio, Alumno.EEstadoCuenta.AlDia);

            //Assert es manejado en el ExpectedException  
        }

        #endregion

        /// <summary>
        /// Verifica que se lance la excepcion AlumnoRepetidoException cuando se intenta agregar 2 veces el mismo a alumno a una universidad
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AlumnoRepetidoException))]
        public void TestMethodAlumnoRepetidoException()
        {
            //Arrange
            Universidad universidad = new Universidad();
            Alumno alumno = new Alumno(1, "Sebastian", "Perez", "36123654", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio, Alumno.EEstadoCuenta.AlDia);

            //Act
            //Agrego el mismo alumno 2 veces
            universidad += alumno;
            universidad += alumno;

            //Assert es manejado en el ExpectedException
        }

        /// <summary>
        /// Verifica que se instancien las colecciones de la clase universidad
        /// </summary>
        [TestMethod]
        public void TestMethodColeccionInstanciadaCorrectamente()
        {
            //Arrage && Act
            Universidad universidad = new Universidad();

            //Assert
            Assert.IsNotNull(universidad.Alumnos);
            Assert.IsNotNull(universidad.Instructores);
            Assert.IsNotNull(universidad.Jornadas);
        }

    }
}
