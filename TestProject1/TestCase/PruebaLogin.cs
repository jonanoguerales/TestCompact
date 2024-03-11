using NUnit.Framework.Interfaces;
using OfficeOpenXml;
using OpenQA.Selenium;
using TestProject1.PageObjectModels;

namespace TestProject1.TestCase
{



    //Clase que contiene los casos de pruebas de Login

    [TestFixture]   //Anotación de Nunit para marcar una clase que contenga casos de prueba
    public class PruebaLogin
    {
        //Selenium driver
        public IWebDriver Driver;

        //SetUp: Anotación de Nunit para ejecutar un método antes de cada test
        //Método para iniciar el navegador Chrome y navegar a una url
        [SetUp]
        public void SetUp()
        {
            // Inicializar el navegador Selenium
            Driver = Inicio.CreateWebDriver();

            //Maximizar la ventana browser
            Driver.Manage().Window.Maximize();

            Inicio inicio = new Inicio(Driver);
            // Cargar la página web
            inicio.NavigateUrlProyecto();
        }

        // Método para leer los datos del archivo Excel
        public static IEnumerable<object[]> GetDataExcel(string filePath)
        {
            // Verificar si el archivo existe
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("El archivo Excel no existe.", filePath);
            }

            List<object[]> loginData = new List<object[]>();

            // Abrir el archivo Excel
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Suponiendo que los datos están en la primera hoja

                int rowCount = worksheet.Dimension.Rows;

                // Leer datos y agregarlos a la lista de datos de inicio de sesión
                for (int row = 2; row <= rowCount; row++) // Empezamos desde la fila 2, suponiendo que la primera fila son encabezados
                {
                    string usuario = worksheet.Cells[row, 1].Value?.ToString() ?? string.Empty;
                    string contraseña = worksheet.Cells[row, 2].Value?.ToString() ?? string.Empty;

                    loginData.Add(new object[] { usuario, contraseña });
                }
            }

            return loginData;
        }

        //Test: Anotación de Nunit para marcar a un método como un caso de prueba automatizado
        //Método que implementa el caso de prueba Login,con los datos proporcionados desde el archivo Excel. El resultado esperado es que el usuario se logue correctamente
        [Test, TestCaseSource(nameof(GetDataExcel), new object[] {@"C:\\Users\\JonathanNoguerales\\Desktop\\Libro1Correcto.xlsx"})]
        public void Test_002_LoginCorrecto(string usuario, string contraseña)
        {
            PaginaLogin paginaLogin = new PaginaLogin(Driver);

            try
            {
                // Realizar el inicio de sesión
                paginaLogin.LoginPrincipal(usuario, contraseña);

                Assert.IsFalse(paginaLogin.IsErrorAlertPresent(), $"Error al iniciar sesión con usuario: {usuario} y contraseña: {contraseña}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Error al iniciar sesión con usuario: {usuario} y contraseña: {contraseña}. Detalles: {ex.Message}");
            }
        }
        [Test, TestCaseSource(nameof(GetDataExcel), new object[] {@"C:\\Users\\JonathanNoguerales\\Desktop\\Libro1Fallo.xlsx"})]
        public void Test_002_LoginFallo(string? usuario, string? contraseña)
        {
            PaginaLogin paginaLogin = new PaginaLogin(Driver);

            try
            {
                // Realizar el inicio de sesión
                paginaLogin.LoginPrincipal(usuario,contraseña);

                Assert.IsTrue(paginaLogin.IsErrorAlertPresent());
            }
            catch (Exception ex)
            {
                Assert.Fail($"Error al iniciar sesión con usuario: {usuario} y contraseña: {contraseña}. Detalles: {ex.Message}");
            }
        }

        //TearDown: Anotación de Nunit para ejecutar un método después de cada test
        //Método para cerrar navegador y liberar los recursos asociados con el controlador
        [TearDown]
        public void AfterTest()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                string testName = TestContext.CurrentContext.Test.FullName;
                string? errorMessage = TestContext.CurrentContext.Result.Message;

                string filePath = @"C:\\Users\\JonathanNoguerales\\Desktop\\errorLogin_log.txt"; // Ruta del archivo de logs

                // Escribir el nombre del test y el mensaje de error en un archivo de texto
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine($"Hora del error:{ DateTime.Now}");
                    writer.WriteLine("Empieza nueva alinea");
                    writer.WriteLine($"Test: {testName}, Error: {errorMessage}");
                }
            }

            // Liberar los recursos asociados con el driver
            Driver.Quit();
            Driver.Dispose();
        }
    }
}
