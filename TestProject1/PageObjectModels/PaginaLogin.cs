using OpenQA.Selenium;

namespace TestCompact.PageObjectModels
{
    //Clase para representar la pagina de Login
    public class PaginaLogin
    {
        //Selenium driver
        protected IWebDriver Driver;

        //Localizadores
        protected By InputUsuario = By.Id("txtEMailAccount");//Encontrar el elemento por id
        protected By InputContraseña = By.Id("txtPassword");//Encontrar el elemento por id
        protected By btnEntrar = By.ClassName("btn");//Encontrar el elemento por Class
        protected By btnCerrarSesion = By.XPath("//*[@id=\"container\"]/header/div[4]/ul/li[2]/a");//Encontrar el elemento por Class

        //Constructor.
        public PaginaLogin( IWebDriver driver)
        {
            Driver = driver;
        }

        //Método para escribir el usuario
        public void InputUsuarioForm(string? usuario) => Driver.FindElement(InputUsuario).SendKeys(usuario);
     
        //Método para escribir la contraseña
        public void InputContraseñaForm(string? contraseña) => Driver.FindElement(InputContraseña).SendKeys(contraseña);  
        
        //Método para hacer click en el botón de login
        public void ClickBotonLogin() => Driver.FindElement(btnEntrar).Click();

        //Método para hacer click en el botón de cerrar sesión
        public void ClickBotonCerrarSesion() => Driver.FindElement(btnCerrarSesion).Click();

        //Método para logearse.
        public void LoginPrincipal(string? usuario, string? contraseña)
        {
            InputUsuarioForm(usuario);
            InputContraseñaForm(contraseña);
            ClickBotonLogin();
        }

        //Método para logearse y ir a empleados
        public PaginaEmpleados LoginAs(string usuario, string contraseña)
        {
            InputUsuarioForm(usuario);
            InputContraseñaForm(contraseña);
            ClickBotonLogin();
            return new PaginaEmpleados(Driver);
        }

        //Método para capturar una alerta 
        //Retorna false si se detecta una alerta 
        public bool IsErrorAlertPresent()
        {
            IWebElement? errorAlert = null;

            try
            {
                // Encontrar la alerta de error
                errorAlert = Driver.FindElement(By.ClassName("alert-danger"));
            }
            catch (NoSuchElementException)
            {
                // Si no se encuentra la alerta de error, devuelve false
                return false;
            }

            // Si se encuentra la alerta de error, devuelve true
            return true;
        }
    }
}
