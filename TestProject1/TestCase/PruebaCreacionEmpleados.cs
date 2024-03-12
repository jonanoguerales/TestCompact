using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestCompact.PageObjectModels;
using TestCompact.Utilities;

namespace TestCompact.TestCase
{
    //Clase que contiene los casos de pruebas de Login

    [TestFixture]   //Anotación de Nunit para marcar una clase que contenga casos de prueba
    public class PruebaCreacionEmpleados
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
            Inicio = new Inicio(Driver); 

            //Maximizar la ventana browser
            Driver.Manage().Window.FullScreen();

            // Crear instancias de otras clases necesarias
            PaginaLogin = new PaginaLogin(Driver);
            PaginaEmpleados = new PaginaEmpleados(Driver);
            ValidaciónAlerts = new ValidaciónAlerts(Driver);

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
            //Redirigir a la pagina de creacion de persona
            Driver.Navigate().GoToUrl("http://zceqa.westus2.cloudapp.azure.com/ZCE/ResumenPersona?idPersonal=-1");
        }

        //Test: Anotación de Nunit para marcar a un método como un caso de prueba automatizado
        //Método que implementa el caso de alta de nueva persona,con los datos proporcionados desde el archivo Excel, comprueba el correcto alta de una nueva persona
        [Test]
        public void Test_001_alta_nueva_persona()
        {
            // Ruta del archivo Excel
            string filePath = @"C:\\Users\\JonathanNoguerales\\Desktop\\Script ZEIT COMPACT v0.2.xlsx";

            Actions actions = new Actions(Driver);

            IWebElement inputDepartamento = Driver.FindElement(By.Id("Departamento"));
            IWebElement inputRegistroHorario = Driver.FindElement(By.XPath("//*[@id=\"ultimoFichaje\"]"));

            //Datos para recorrer el Excel
            int inicioRow = 4;
            int inicioCol = 6;
            int finalRow = 4;
            int finalCol = 33;

            // Obtener datos del archivo Excel
            IEnumerable<object[]> datosExcel = ExcelDataReader.GetDataExcel(filePath, inicioRow, inicioCol, finalRow, finalCol);

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
                // Desplazarse hasta el input departamento
                actions.MoveToElement(inputDepartamento);
                actions.Perform();
                PaginaEmpleados.BoxDesplegablePermisos(row13);
                PaginaEmpleados.BoxDesplegableCalendario(row14);
                PaginaEmpleados.InputFechaAlta(row15);
                PaginaEmpleados.InputFechaBaja(row16);
                PaginaEmpleados.InputMensajeEspecial(row17);
                PaginaEmpleados.BoxDesplegableZonaHoraria(row18);
                // Desplazarse hasta el input departamento
                actions.MoveToElement(inputRegistroHorario);
                actions.Perform();
                PaginaEmpleados.BoxDesplegableEmpresa(row22);
                PaginaEmpleados.BoxDesplegableCentroTrabajo(row23);
                PaginaEmpleados.BoxDesplegableSeccion(row24);
                PaginaEmpleados.BoxDesplegableDepartamento(row25);
                PaginaEmpleados.InputVacacionesAnuales(row26);
                PaginaEmpleados.BoxDesplegableProteccionCivil(row27);

                // Ejecutar JavaScript para ir al principio del scroll
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("window.scrollTo(0, 0)");

                // Localizar el botón "Guardar"
                IWebElement botonGuardar = Driver.FindElement(By.CssSelector("#save > img"));

                // Desplazarse hasta el botón "Guardar"
                actions.MoveToElement(botonGuardar);
                actions.Perform();

                // Hacer clic en el botón "Guardar"
                botonGuardar.Click();

                ValidaciónAlerts.ValidacionAlert("Registro actualizado correctamente");
            }
        }

        //Método para verificar los alerts en el alta de nueva persona
        [Test]
        public void Test_002_verificacion_alerts()
        {
            // Localizar el botón "Guardar"
            IWebElement botonGuardar = Driver.FindElement(By.CssSelector("#save > img"));

            Actions actions = new Actions(Driver);

            IWebElement inputDepartamento = Driver.FindElement(By.Id("Departamento"));
            IWebElement inputRegistroHorario = Driver.FindElement(By.XPath("//*[@id=\"ultimoFichaje\"]"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;

            //---Validacion alert nombre---
            //Ingresar datos en input nombre
            PaginaEmpleados.InputNombre("");
            Thread.Sleep(100);
            // Ejecutar JavaScript para ir al principio del scroll
            js.ExecuteScript("window.scrollTo(0, 0)");
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
            Thread.Sleep(100);
            // Ejecutar JavaScript para ir al principio del scroll
            js.ExecuteScript("window.scrollTo(0, 0)");
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
            Thread.Sleep(100);
            // Ejecutar JavaScript para ir al principio del scroll
            js.ExecuteScript("window.scrollTo(0, 0)");
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
            Thread.Sleep(100);
            // Ejecutar JavaScript para ir al principio del scroll
            js.ExecuteScript("window.scrollTo(0, 0)");
            // Desplazarse hasta el botón "Guardar"
            actions.MoveToElement(botonGuardar);
            actions.Perform();
            // Hacer clic en el botón "Guardar"
            botonGuardar.Click();
            //validar alert nomina
            ValidaciónAlerts.ValidacionAlert("El número del empleado es necesario");

            // Desplazarse hasta el input departamento
            actions.MoveToElement(inputDepartamento);
            actions.Perform();

            //---Validacion alert permisos---
            //Ingresar datos en input identificador nomina
            PaginaEmpleados.InputIdentificadorNomina("7777777");
            //Ingresar datos en input permisos
            PaginaEmpleados.BoxDesplegablePermisos("-- Seleccione una opción --");
            Thread.Sleep(100);
            // Ejecutar JavaScript para ir al principio del scroll
            js.ExecuteScript("window.scrollTo(0, 0)");
            // Desplazarse hasta el botón "Guardar"
            actions.MoveToElement(botonGuardar);
            actions.Perform();
            // Hacer clic en el botón "Guardar"
            botonGuardar.Click();
            //validar alert permisos
            ValidaciónAlerts.ValidacionAlert("El perfil es necesario");

            // Desplazarse hasta el input departamento
            actions.MoveToElement(inputDepartamento);
            actions.Perform();

            //---Validacion alert calendario---
            //Ingresar datos en input permisos
            PaginaEmpleados.BoxDesplegablePermisos("001 - Asistencia R26");
            //Ingresar datos en input calendario
            PaginaEmpleados.BoxDesplegableCalendario("-- Seleccione una opción --");
            Thread.Sleep(100);
            // Ejecutar JavaScript para ir al principio del scroll
            js.ExecuteScript("window.scrollTo(0, 0)");
            // Desplazarse hasta el botón "Guardar"
            actions.MoveToElement(botonGuardar);
            actions.Perform();
            // Hacer clic en el botón "Guardar"
            botonGuardar.Click();
            //validar alert calendario
            ValidaciónAlerts.ValidacionAlert("El calendario es necesario");


            // Desplazarse hasta el input departamento
            actions.MoveToElement(inputRegistroHorario);
            actions.Perform();

            //---Validacion alert zona horaria---
            //Ingresar datos en input calendario
            PaginaEmpleados.BoxDesplegableCalendario("2 - Calendario PYV México");
            //Ingresar datos en input zona horaria
            PaginaEmpleados.BoxDesplegableZonaHoraria("-- Seleccione una opción --");
            Thread.Sleep(100);
            // Ejecutar JavaScript para ir al principio del scroll
            js.ExecuteScript("window.scrollTo(0, 0)");
            // Desplazarse hasta el botón "Guardar"
            actions.MoveToElement(botonGuardar);
            actions.Perform();
            // Hacer clic en el botón "Guardar"
            botonGuardar.Click();
            //validar alert zona horaria
            ValidaciónAlerts.ValidacionAlert("La zona horaria es necesaria");

            // Desplazarse hasta el input departamento
            actions.MoveToElement(inputRegistroHorario);
            actions.Perform();

            //---Validacion alert fecha de Alta---
            //Ingresar datos en input  zona horaria
            PaginaEmpleados.BoxDesplegableZonaHoraria("(UTC-10:00) Hawaii");
            //Ingresar datos en input fecha alta
            PaginaEmpleados.InputFechaAlta("");
            Thread.Sleep(100);
            // Ejecutar JavaScript para ir al principio del scroll
            js.ExecuteScript("window.scrollTo(0, 0)");
            // Desplazarse hasta el botón "Guardar"
            actions.MoveToElement(botonGuardar);
            actions.Perform();
            // Hacer clic en el botón "Guardar"
            botonGuardar.Click();
            //validar alert fecha alta
            ValidaciónAlerts.ValidacionAlert("Es necesario que indique una fecha de Alta.");

        }

        //TearDown: Anotación de Nunit para ejecutar un método después de cada test
        //Método para cerrar navegador y liberar los recursos asociados con el controlador
        [TearDown]
        public void AfterTest()
        {
            ResultadosTest ResultadosTest = new ResultadosTest();
            ResultadosTest.LogsResultado();

            // Liberar los recursos asociados con el driver
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
