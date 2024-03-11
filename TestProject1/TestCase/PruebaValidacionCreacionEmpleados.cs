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
    public class PruebaValidacionCreacionEmpleados
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
            //Redirigir a la pagina de creacion de persona
            Driver.Navigate().GoToUrl("http://zceqa.westus2.cloudapp.azure.com/ZCE/ResumenPersona?idPersonal=-1");
        }

        //Test: Anotación de Nunit para marcar a un método como un caso de prueba automatizado
        //Método que implementa el caso de alta de nueva persona,con los datos proporcionados desde el archivo Excel, comprueba la correcta verificación del alert de nombre es necesario
        [Test]
        public void Test_001_verificación_el_nombre_es_necesario()
        {
            // Ruta del archivo Excel
            string filePath = @"C:\\Users\\jona\\Desktop\\Script ZEIT COMPACT v0.2.xlsx";

            //Datos para recorrer el Excel
            int inicioRow = 5;
            int inicioCol = 6;
            int finalRow = 5;
            int finalCol = 8;

            // Obtener datos del archivo Excel
            IEnumerable<object[]> datosExcel = ExcelDataReader.GetDataExcel(filePath, inicioRow, inicioCol, finalRow, finalCol);

            // Iterar sobre los datos y ejecutar la prueba para cada conjunto de datos
            foreach (var datos in datosExcel)
            {
                string row1 = (string)datos[0];
                string row2 = (string)datos[1];
                string row3 = (string)datos[2];

                // Ejecutar la prueba con los datos obtenidos del Excel
                PaginaEmpleados.InputNumEmpleado(row1);
                PaginaEmpleados.InputIdentificadorNomina(row2);
                PaginaEmpleados.InputNombre(row3);

                Thread.Sleep(100);
                // Ejecutar JavaScript para ir al principio del scroll
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("window.scrollTo(0, 0)");

                // Localizar el botón "Guardar"
                IWebElement botonGuardar = Driver.FindElement(By.CssSelector("#save > img"));

                Actions actions = new Actions(Driver);

                // Desplazarse hasta el botón "Guardar"
                actions.MoveToElement(botonGuardar);
                actions.Perform();

                // Hacer clic en el botón "Guardar"
                botonGuardar.Click();

                ValidaciónAlerts.ValidacionAlert("El nombre es necesario");
            }
        }
       
        //Método que implementa el caso de alta de nueva persona,con los datos proporcionados desde el archivo Excel, comprueba la correcta verificación del alert del primer apellido es necesario
        [Test]
        public void Test_002_verificación_el_primer_apellido_es_necesario()
        {
            // Ruta del archivo Excel
            string filePath = @"C:\\Users\\jona\\Desktop\\Script ZEIT COMPACT v0.2.xlsx";

            //Datos para recorrer el Excel
            int inicioRow = 6;
            int inicioCol = 6;
            int finalRow = 6;
            int finalCol = 9;

            // Obtener datos del archivo Excel
            IEnumerable<object[]> datosExcel = ExcelDataReader.GetDataExcel(filePath, inicioRow, inicioCol, finalRow, finalCol);

            // Iterar sobre los datos y ejecutar la prueba para cada conjunto de datos
            foreach (var datos in datosExcel)
            {
                string row1 = (string)datos[0];
                string row2 = (string)datos[1];
                string row3 = (string)datos[2];
                string row4 = (string)datos[3];

                // Ejecutar la prueba con los datos obtenidos del Excel
                PaginaEmpleados.InputNumEmpleado(row1);
                PaginaEmpleados.InputIdentificadorNomina(row2);
                PaginaEmpleados.InputNombre(row3);
                PaginaEmpleados.InputPrimerApellido(row4);

                Thread.Sleep(100);

                // Ejecutar JavaScript para ir al principio del scroll
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("window.scrollTo(0, 0)");
                // Localizar el botón "Guardar"
                IWebElement botonGuardar = Driver.FindElement(By.CssSelector("#save > img"));

                Actions actions = new Actions(Driver);

                // Desplazarse hasta el botón "Guardar"
                actions.MoveToElement(botonGuardar);
                actions.Perform();

                // Hacer clic en el botón "Guardar"
                botonGuardar.Click();

                ValidaciónAlerts.ValidacionAlert("El primer apellido es necesario");
            }
        }

        //Método que implementa el caso de alta de nueva persona,con los datos proporcionados desde el archivo Excel, comprueba la correcta verificación del alert de "Documento de identidad es necesario"
        [Test]
        public void Test_003_verificación_documento_de_identidad_es_necesario()
        {
            // Ruta del archivo Excel
            string filePath = @"C:\\Users\\jona\\Desktop\\Script ZEIT COMPACT v0.2.xlsx";

            //Datos para recorrer el Excel
            int inicioRow = 7;
            int inicioCol = 6;
            int finalRow = 7;
            int finalCol = 12;

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

                // Ejecutar la prueba con los datos obtenidos del Excel
                PaginaEmpleados.InputNumEmpleado(row1);
                PaginaEmpleados.InputIdentificadorNomina(row2);
                PaginaEmpleados.InputNombre(row3);
                PaginaEmpleados.InputPrimerApellido(row4);
                PaginaEmpleados.InputSegundoApellido(row5);
                PaginaEmpleados.InputNombreCorto(row6);
                PaginaEmpleados.InputDocumentoDeIdentidad(row7);

                Thread.Sleep(100);

                // Ejecutar JavaScript para ir al principio del scroll
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("window.scrollTo(0, 0)");

                // Localizar el botón "Guardar"
                IWebElement botonGuardar = Driver.FindElement(By.CssSelector("#save > img"));

                Actions actions = new Actions(Driver);

                // Desplazarse hasta el botón "Guardar"
                actions.MoveToElement(botonGuardar);
                actions.Perform();

                // Hacer clic en el botón "Guardar"
                botonGuardar.Click();

                ValidaciónAlerts.ValidacionAlert("El documento de identidad es necesario");
            }
        }

        //Método que implementa el caso de alta de nueva persona,con los datos proporcionados desde el archivo Excel, comprueba la correcta verificación del alert de "El número de empleado es necesario"
        [Test]
        public void Test_004_verificación_el_numero_de_empleado_es_necesario()
        {
            // Ruta del archivo Excel
            string filePath = @"C:\\Users\\jona\\Desktop\\Script ZEIT COMPACT v0.2.xlsx";

            //Datos para recorrer el Excel
            int inicioRow = 8;
            int inicioCol = 6;
            int finalRow = 8;
            int finalCol = 12;

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

                // Ejecutar la prueba con los datos obtenidos del Excel
                PaginaEmpleados.InputNumEmpleado(row1);
                PaginaEmpleados.InputIdentificadorNomina(row2);
                PaginaEmpleados.InputNombre(row3);
                PaginaEmpleados.InputPrimerApellido(row4);
                PaginaEmpleados.InputSegundoApellido(row5);
                PaginaEmpleados.InputNombreCorto(row6);
                PaginaEmpleados.InputDocumentoDeIdentidad(row7);

                Thread.Sleep(100);

                // Ejecutar JavaScript para ir al principio del scroll
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("window.scrollTo(0, 0)");

                // Localizar el botón "Guardar"
                IWebElement botonGuardar = Driver.FindElement(By.CssSelector("#save > img"));

                Actions actions = new Actions(Driver);

                // Desplazarse hasta el botón "Guardar"
                actions.MoveToElement(botonGuardar);
                actions.Perform();

                // Hacer clic en el botón "Guardar"
                botonGuardar.Click();

                ValidaciónAlerts.ValidacionAlert("El número del empleado es necesario");
            }
        }

        //Método que implementa el caso de alta de nueva persona,con los datos proporcionados desde el archivo Excel, comprueba la correcta verificación del alert de "El perfil es necesario"
        [Test]
        public void Test_005_verificación_el_perfil_es_necesario()
        {
            // Ruta del archivo Excel
            string filePath = @"C:\\Users\\jona\\Desktop\\Script ZEIT COMPACT v0.2.xlsx";

            //Datos para recorrer el Excel
            int inicioRow = 9;
            int inicioCol = 6;
            int finalRow = 9;
            int finalCol = 18;

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

                Thread.Sleep(100);

                // Ejecutar JavaScript para ir al principio del scroll
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("window.scrollTo(0, 0)");

                // Localizar el botón "Guardar"
                IWebElement botonGuardar = Driver.FindElement(By.CssSelector("#save > img"));

                Actions actions = new Actions(Driver);

                // Desplazarse hasta el botón "Guardar"
                actions.MoveToElement(botonGuardar);
                actions.Perform();

                // Hacer clic en el botón "Guardar"
                botonGuardar.Click();

                ValidaciónAlerts.ValidacionAlert("El perfil es necesario");
            }
        }

        //Método que implementa el caso de alta de nueva persona,con los datos proporcionados desde el archivo Excel, comprueba la correcta verificación del alert de "El calendario es necesario"
        [Test]
        public void Test_006_verificación_el_calendario_es_necesario()
        {
            // Ruta del archivo Excel
            string filePath = @"C:\\Users\\jona\\Desktop\\Script ZEIT COMPACT v0.2.xlsx";

            //Datos para recorrer el Excel
            int inicioRow = 10;
            int inicioCol = 6;
            int finalRow = 10;
            int finalCol = 19;

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

                Thread.Sleep(100);

                // Ejecutar JavaScript para ir al principio del scroll
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("window.scrollTo(0, 0)");

                // Localizar el botón "Guardar"
                IWebElement botonGuardar = Driver.FindElement(By.CssSelector("#save > img"));

                Actions actions = new Actions(Driver);

                // Desplazarse hasta el botón "Guardar"
                actions.MoveToElement(botonGuardar);
                actions.Perform();

                // Hacer clic en el botón "Guardar"
                botonGuardar.Click();

                ValidaciónAlerts.ValidacionAlert("El calendario es necesario");
            }
        }

        //Método que implementa el caso de alta de nueva persona,con los datos proporcionados desde el archivo Excel, comprueba la correcta verificación del alert de "La zona horaria es necesaria"
        [Test]
        public void Test_007_verificación_la_zona_horaria_es_necesaria()
        {
            // Ruta del archivo Excel
            string filePath = @"C:\\Users\\jona\\Desktop\\Script ZEIT COMPACT v0.2.xlsx";

            //Datos para recorrer el Excel
            int inicioRow = 11;
            int inicioCol = 6;
            int finalRow = 11;
            int finalCol = 23;

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

                Thread.Sleep(100);

                // Ejecutar JavaScript para ir al principio del scroll
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("window.scrollTo(0, 0)");

                // Localizar el botón "Guardar"
                IWebElement botonGuardar = Driver.FindElement(By.CssSelector("#save > img"));

                Actions actions = new Actions(Driver);

                // Desplazarse hasta el botón "Guardar"
                actions.MoveToElement(botonGuardar);
                actions.Perform();

                // Hacer clic en el botón "Guardar"
                botonGuardar.Click();

                ValidaciónAlerts.ValidacionAlert("La zona horaria es necesaria");
            }
        }

        ////Método que implementa el caso de alta de nueva persona,con los datos proporcionados desde el archivo Excel, comprueba la correcta verificación del alert de "La fecha de alta es necesaria" No consigo eliminar la hora
        //[Test]
        //public void Test_009_verificación_la_fecha_de_alta_es_necesaria()
        //{
        //    // Ruta del archivo Excel
        //    string filePath = @"C:\\Users\\jona\\Desktop\\Script ZEIT COMPACT v0.2.xlsx";

        //    //Datos para recorrer el Excel
        //    int inicioRow = 13;
        //    int inicioCol = 6;
        //    int finalRow = 13;
        //    int finalCol = 23;

        //    // Obtener datos del archivo Excel
        //    IEnumerable<object[]> datosExcel = ExcelDataReader.GetDataExcel(filePath, inicioRow, inicioCol, finalRow, finalCol);

        //    // Iterar sobre los datos y ejecutar la prueba para cada conjunto de datos
        //    foreach (var datos in datosExcel)
        //    {
        //        string row1 = (string)datos[0];
        //        string row2 = (string)datos[1];
        //        string row3 = (string)datos[2];
        //        string row4 = (string)datos[3];
        //        string row5 = (string)datos[4];
        //        string row6 = (string)datos[5];
        //        string row7 = (string)datos[6];
        //        string row8 = (string)datos[7];
        //        string row9 = (string)datos[8];
        //        string row10 = (string)datos[9];
        //        string row11 = (string)datos[10];
        //        string row12 = (string)datos[11];
        //        string row13 = (string)datos[12];
        //        string row14 = (string)datos[13];
        //        string row15 = (string)datos[14];
        //        string row16 = (string)datos[15];
        //        string row17 = (string)datos[16];
        //        string row18 = (string)datos[17];

        //        // Ejecutar la prueba con los datos obtenidos del Excel
        //        PaginaEmpleados.InputNumEmpleado(row1);
        //        PaginaEmpleados.InputIdentificadorNomina(row2);
        //        PaginaEmpleados.InputNombre(row3);
        //        PaginaEmpleados.InputPrimerApellido(row4);
        //        PaginaEmpleados.InputSegundoApellido(row5);
        //        PaginaEmpleados.InputNombreCorto(row6);
        //        PaginaEmpleados.InputDocumentoDeIdentidad(row7);
        //        PaginaEmpleados.InputCorreoElectrónico(row8);
        //        PaginaEmpleados.InputTelefonoDeCasa(row9);
        //        PaginaEmpleados.InputTelefonoDeOficina(row10);
        //        PaginaEmpleados.InputNúmeroDeTelefonoMoviL(row11);
        //        PaginaEmpleados.InputPuestolaboral(row12);
        //        PaginaEmpleados.BoxDesplegablePermisos(row13);
        //        PaginaEmpleados.BoxDesplegableCalendario(row14);
        //        IWebElement inputFechaAlta = Driver.FindElement(By.Id("inputFechaAlta"));
        //        if (string.IsNullOrEmpty(row15) || row15 == "Vacio")
        //        {
        //            inputFechaAlta.Clear(); // Limpiar el campo de fecha
        //        }
        //        else
        //        {
        //            inputFechaAlta.SendKeys(row15); // Añadir el nuevo valor al campo de fecha
        //        }

        //        Thread.Sleep(10000);
        //        PaginaEmpleados.InputFechaBaja(row16);
        //        PaginaEmpleados.InputMensajeEspecial(row17);
        //        PaginaEmpleados.BoxDesplegableZonaHoraria(row18);

        //        Thread.Sleep(100);

        //        // Ejecutar JavaScript para ir al principio del scroll
        //        IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
        //        js.ExecuteScript("window.scrollTo(0, 0)");

        //        // Localizar el botón "Guardar"
        //        IWebElement botonGuardar = Driver.FindElement(By.CssSelector("#save > img"));

        //        Actions actions = new Actions(Driver);

        //        // Desplazarse hasta el botón "Guardar"
        //        actions.MoveToElement(botonGuardar);
        //        actions.Perform();

        //        // Hacer clic en el botón "Guardar"
        //        botonGuardar.Click();

        //        ValidaciónAlerts.ValidacionAlert("Es necesario que indique una fecha de Alta.");
        //    }
        //}

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
