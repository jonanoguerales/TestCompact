using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestCompact.PageObjectModels
{
    public class Inicio
    {
        //Credenciales Login
        private string usuarioLogin = "arturo.ceron@pyv.systems";
        private string contraseñaLogin = "Pruebas123";

        //Url aplicación web
        protected const string UrlProyecto = "http://zceqa.westus2.cloudapp.azure.com/";

        // Ruta del archivo Excel
        string urlFichero = @"C:\\Users\\JonathanNoguerales\\Desktop\\Script ZEIT COMPACT v0.2.xlsx";

        public string ObtenerUsuarioLogin()
        {
            return usuarioLogin;
        }

        public string ObtenerContraseñaLogin()
        {
            return contraseñaLogin;
        }
        public string ObtenerUrlFicheroExcel()
        {
            return urlFichero;
        }

        //Selenium driver
        protected IWebDriver Driver;


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
