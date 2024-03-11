using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProject1.PageObjectModels
{
    public class Inicio
    {
        //Credenciales Login
        private string usuarioLogin = "arturo.ceron@pyv.systems";
        private string contraseñaLogin = "Pruebas123";

        public string ObtenerUsuarioLogin()
        {
            return usuarioLogin;
        }

        public string ObtenerContraseñaLogin()
        {
            return contraseñaLogin;
        }

        //Selenium driver
        protected IWebDriver Driver;

        //Url aplicación web
        protected const string UrlProyecto = "http://zceqa.westus2.cloudapp.azure.com/";

        //Constructor
        public Inicio(IWebDriver driver)
        {
            Driver = driver;
        }

        //Método para iniciar el controlador Chrome
        public static IWebDriver CreateWebDriver()
        {
            return new ChromeDriver();
        }

        //Método para navegar hacia la url de la aplicación web
        public void NavigateUrlProyecto()
        {
            // Cargar la página web
            Driver.Navigate().GoToUrl(UrlProyecto);

            Driver.Manage().Cookies.DeleteAllCookies();
        }
    }
}
