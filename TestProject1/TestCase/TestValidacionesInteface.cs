using OpenQA.Selenium;
using TestCompact.Utilities;
using TestCompact.PageObjectModels;

namespace TestCompact.TestCase
{
    public class TestValidacionesInteface
    {
        //Selenium Driver
        public IWebDriver Driver;
        //Selenium Element
        public IWebElement? element;

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
        [Test]
        public void Test_001_ValidarPaginaLogin()
        {
            //Verificar pantalla de login por medio del formulario
            element = Driver.FindElement(By.ClassName("form-login"));
            bool verificarFormLogin = element.Displayed;
            Console.WriteLine(verificarFormLogin);
            //Saber si es true pasa prueba
            Assert.IsTrue(verificarFormLogin);
        }

        [Test]
        public void Test_002_ValidarPaginaHome()
        {
            //Input usuario
            element = Driver.FindElement(By.Id("txtEMailAccount"));//Encontrar el elemento por id
            element.SendKeys("arturo.ceron@pyv.systems"); //Introducir el valor en el elemento

            //Input contraseña
            element = Driver.FindElement(By.Id("txtPassword"));
            element.SendKeys("Pruebas123");

            //click Entrar
            element = Driver.FindElement(By.ClassName("btn"));
            element.Click();

            //Verificar pantalla de home 
            element = Driver.FindElement(By.ClassName("breadcrumb"));
            bool verificarBreadcrumb = element.Displayed;

            //Verificar pantalla de home
            element = Driver.FindElement(By.XPath("//*[@id=\"main-content\"]/div/div[2]"));
            bool verificarContenedorPrincipal = element.Displayed;

            //Verificar pantalla de login por medio del formulario
            element = Driver.FindElement(By.Id("divUltimosfich"));
            bool verificarUltimosRegistros = element.Displayed;

            //verificar todos
            bool verificarTodo = verificarBreadcrumb && verificarContenedorPrincipal && verificarUltimosRegistros;

            //Ver si da true
            Console.WriteLine(verificarTodo);
            //Saber si es true pasa prueba
            Assert.IsTrue(verificarTodo);
        }

        [TearDown]
        public void TearDown()
        {
            ResultadosTest ResultadosTest = new ResultadosTest();
            ResultadosTest.LogsResultado();

            // Liberar los recursos asociados con el Driver
            Driver.Quit();
            Driver.Dispose();
        }
    }
}