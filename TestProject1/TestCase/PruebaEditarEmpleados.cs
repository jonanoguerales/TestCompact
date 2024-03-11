using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestCompact.PageObjectModels;
using TestCompact.Utilities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestCompact.TestCase
{
    //Clase que contiene los casos de pruebas de Login

    [TestFixture]   //Anotación de Nunit para marcar una clase que contenga casos de prueba
    public class PruebaEditarEmpleados
    {
        //Selenium driver
        public IWebDriver Driver;

        public PaginaLogin PaginaLogin;
        public PaginaEmpleados PaginaEmpleados;
        public Inicio Inicio;
        public ValidaciónAlerts ValidaciónAlerts;

        //SetUp: Anotación de Nunit para ejecutar un método antes de cada test
        //Método para iniciar el navegador Chrome y navegar a una url
        [SetUp]
        public void SetUp()
        {
            // Inicializar instancias
            Driver = Inicio.CreateWebDriver();
            Inicio = new Inicio(Driver); // Aquí estoy asumiendo que el constructor de Inicio espera un IWebDriver

            //Maximizar la ventana browser
            Driver.Manage().Window.FullScreen();

            // Crear instancias de otras clases necesarias
            PaginaLogin = new PaginaLogin(Driver);
            PaginaEmpleados = new PaginaEmpleados(Driver);
            ValidaciónAlerts  = new ValidaciónAlerts(Driver);

            // Cargar la página web
            Inicio.NavigateUrlProyecto();

            //Obtener datos de login
            string usuarioLogin = Inicio.ObtenerUsuarioLogin();
            string contraseñaLogin = Inicio.ObtenerContraseñaLogin();

            //Realizar el inicio de sesión
            try
            {
                PaginaEmpleados paginaEmpleados = PaginaLogin.LoginAs(usuarioLogin, contraseñaLogin);

                Assert.IsFalse(PaginaLogin.IsErrorAlertPresent(), $"Error al iniciar sesión con usuario: {usuarioLogin} y contraseña: {contraseñaLogin}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Error al iniciar sesión con usuario: {usuarioLogin} y contraseña: {contraseñaLogin}. Detalles: {ex.Message}");
            }

            //Hacer click en el menu
            PaginaEmpleados.BtnMenu();
            //Hacer click en personas
            PaginaEmpleados.BtnPersonas();
            //Hacer click en listado de personas
            PaginaEmpleados.BtnListadoPersonas();
            //Buscar persona
            PaginaEmpleados.InputBuscarPersona("Nombre");
            PaginaEmpleados.BtnBuscar();
            PaginaEmpleados.BtnEditarPersona();

        }

        //Test: Anotación de Nunit para marcar a un método como un caso de prueba automatizado
        //Método que implementa el caso de alta de nueva persona,con los datos proporcionados desde el archivo Excel, comprueba el correcto alta de una nueva persona
        [Test]
        public void Test_001_editar_persona()
        {
            // Ruta del archivo Excel
            string filePath = @"C:\\Users\\jona\\Desktop\\Script ZEIT COMPACT v0.2.xlsx";

            //Datos para recorrer el Excel
            int inicioRow = 15;
            int inicioCol = 6;
            int finalRow = 15;
            int finalCol = 33;

            // Obtener datos del archivo Excel
            IEnumerable<object[]> datosExcel = ExcelDataReader.GetDataExcel(filePath,inicioRow,inicioCol,finalRow,finalCol);

            // Iterar sobre los datos y ejecutar la prueba para cada conjunto de datos
            foreach (var datos in datosExcel)
            {
                string row1 = (string)datos[0];
                string row2 = (string)datos[1];
                string row3 = (string)datos[2];
                string row4 = (string)datos[3];
                string row5 = (string)datos[4];
                string row6 = (string)datos[5];
                string row7 = (string)datos[6];
                string row8 = (string)datos[7];
                string row9 = (string)datos[8];
                string row10 = (string)datos[9];
                string row11 = (string)datos[10];
                string row12 = (string)datos[11];
                string row13 = (string)datos[12];
                string row14 = (string)datos[13];
                string row15 = (string)datos[14];
                string row16 = (string)datos[15];
                string row17 = (string)datos[16];
                string row18 = (string)datos[17];
                string row22 = (string)datos[22];
                string row23 = (string)datos[23];
                string row24 = (string)datos[24];
                string row25 = (string)datos[25];
                string row26 = (string)datos[26];
                string row27 = (string)datos[27];

                // Ejecutar la prueba con los datos obtenidos del Excel
                PaginaEmpleados.InputNumEmpleado(row1);
                PaginaEmpleados.InputIdentificadorNomina(row2);
                PaginaEmpleados.InputNombre(row3);
                PaginaEmpleados.InputPrimerApellido(row4);
                PaginaEmpleados.InputSegundoApellido(row5);
                PaginaEmpleados.InputNombreCorto(row6);
                PaginaEmpleados.InputDocumentoDeIdentidad(row7);
                PaginaEmpleados.InputCorreoElectrónico(row8);
                PaginaEmpleados.InputTelefonoDeCasa(row9);
                PaginaEmpleados.InputTelefonoDeOficina(row10);
                PaginaEmpleados.InputNúmeroDeTelefonoMoviL(row11);
                PaginaEmpleados.InputPuestolaboral(row12);
                PaginaEmpleados.BoxDesplegablePermisos(row13);
                PaginaEmpleados.BoxDesplegableCalendario(row14);
                PaginaEmpleados.InputFechaAlta(row15);
                PaginaEmpleados.InputFechaBaja(row16); 
                PaginaEmpleados.InputMensajeEspecial(row17);
                PaginaEmpleados.BoxDesplegableZonaHoraria(row18);
                PaginaEmpleados.BoxDesplegableEmpresa(row22);
                PaginaEmpleados.BoxDesplegableCentroTrabajo(row23);
                PaginaEmpleados.BoxDesplegableSeccion(row24);
                PaginaEmpleados.BoxDesplegableDepartamento(row25);
                PaginaEmpleados.InputVacacionesAnuales(row26);
                PaginaEmpleados.BoxDesplegableProteccionCivil(row27);

                // Localizar el botón "Guardar"
                IWebElement botonGuardar = Driver.FindElement(By.CssSelector("#save > img"));

                Actions actions = new Actions(Driver);

                // Desplazarse hasta el botón "Guardar"
                actions.MoveToElement(botonGuardar);
                actions.Perform();

                // Hacer clic en el botón "Guardar"
                botonGuardar.Click();

                ValidaciónAlerts.ValidacionAlert("Registro actualizado correctamente");
            }
        }

        //Método para verificar los alerts en la edicion de persona
        [Test]
        public void Test_002_verificacion_alerts()
        {
            // Localizar el botón "Guardar"
            IWebElement botonGuardar = Driver.FindElement(By.CssSelector("#save > img"));

            Actions actions = new Actions(Driver);

            //---Validacion alert nombre---
            //Ingresar datos en input nombre
            PaginaEmpleados.InputNombre("Vacio");
            // Desplazarse hasta el botón "Guardar"
            actions.MoveToElement(botonGuardar);
            actions.Perform();
            // Hacer clic en el botón "Guardar"
            botonGuardar.Click();
            //validar alert nombre
            ValidaciónAlerts.ValidacionAlert("El nombre es necesario");

            //---Validacion alert primer apellido---
            //Ingresar datos en input nombre
            PaginaEmpleados.InputNombre("Prueba");
            //Ingresar datos en input primer apellido
            PaginaEmpleados.InputPrimerApellido("");
            // Desplazarse hasta el botón "Guardar"
            actions.MoveToElement(botonGuardar);
            actions.Perform();
            // Hacer clic en el botón "Guardar"
            botonGuardar.Click();
            //validar alert primer apellido
            ValidaciónAlerts.ValidacionAlert("El primer apellido es necesario");

            //---Validacion alert documento de identidad---
            //Ingresar datos en input primer apellido
            PaginaEmpleados.InputPrimerApellido("Prueba1");
            //Ingresar datos en input socumento de identidad
            PaginaEmpleados.InputDocumentoDeIdentidad("");
            // Desplazarse hasta el botón "Guardar"
            actions.MoveToElement(botonGuardar);
            actions.Perform();
            // Hacer clic en el botón "Guardar"
            botonGuardar.Click();
            //validar alert documento de identidad
            ValidaciónAlerts.ValidacionAlert("El documento de identidad es necesario");

            //---Validacion alert identificador nomina---
            //Ingresar datos en input socumento de identidad
            PaginaEmpleados.InputDocumentoDeIdentidad("0283455A");
            //Ingresar datos en input identificador nomina
            PaginaEmpleados.InputIdentificadorNomina("");
            // Desplazarse hasta el botón "Guardar"
            actions.MoveToElement(botonGuardar);
            actions.Perform();
            // Hacer clic en el botón "Guardar"
            botonGuardar.Click();
            //validar alert nomina
            ValidaciónAlerts.ValidacionAlert("El número del empleado es necesario");

            //---Validacion alert permisos---
            //Ingresar datos en input identificador nomina
            PaginaEmpleados.InputIdentificadorNomina("7777777");
            //Ingresar datos en input permisos
            PaginaEmpleados.BoxDesplegablePermisos("-- Seleccione una opción --");
            // Desplazarse hasta el botón "Guardar"
            actions.MoveToElement(botonGuardar);
            actions.Perform();
            // Hacer clic en el botón "Guardar"
            botonGuardar.Click();
            //validar alert permisos
            ValidaciónAlerts.ValidacionAlert("El perfil es necesario");

            //---Validacion alert calendario---
            //Ingresar datos en input permisos
            PaginaEmpleados.BoxDesplegablePermisos("001 - Asistencia R26");
            //Ingresar datos en input calendario
            PaginaEmpleados.BoxDesplegableCalendario("-- Seleccione una opción --");
            // Desplazarse hasta el botón "Guardar"
            actions.MoveToElement(botonGuardar);
            actions.Perform();
            // Hacer clic en el botón "Guardar"
            botonGuardar.Click();
            //validar alert calendario
            ValidaciónAlerts.ValidacionAlert("El calendario es necesario");

            //---Validacion alert zona horaria---
            //Ingresar datos en input calendario
            PaginaEmpleados.BoxDesplegableCalendario("2 - Calendario PYV México");
            // Localizar el campo de entrada por XPath
            IWebElement campoInput = Driver.FindElement(By.XPath("//*[@id=\"dropdownlistContentIdTimeZone\"]/input"));
            // Limpiar el contenido del campo de entrada
            campoInput.Clear();
            //Ingresar datos en input zona horaria
            campoInput.SendKeys("-- Seleccione una opción --");
            // Desplazarse hasta el botón "Guardar"
            actions.MoveToElement(botonGuardar);
            actions.Perform();
            // Hacer clic en el botón "Guardar"
            botonGuardar.Click();
            //validar alert zona horaria
            ValidaciónAlerts.ValidacionAlert("La zona horaria es necesaria");

        }

        //TearDown: Anotación de Nunit para ejecutar un método después de cada test
        //Método para cerrar navegador y liberar los recursos asociados con el controlador
        [TearDown]
        public void AfterTest()
        {
            LogsError logsError = new LogsError();
            logsError.LogsErrors();

            // Liberar los recursos asociados con el driver
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
