using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCompact.Utilities
{
    public class ValidaciónAlerts
    {
        private readonly IWebDriver Driver;

        public ValidaciónAlerts(IWebDriver driver)
        {
            Driver = driver;
        }
        public void ValidacionAlert(string expectedText)
        {
            try
            {
                IWebElement modal = Driver.FindElement(By.Id("modalContainer"));

                // Verificar si el texto del modal contiene el texto esperado
                IWebElement pElement = modal.FindElement(By.TagName("p"));
                string actualText = pElement.Text;

                if (actualText != expectedText)
                {
                    // Si el texto no coincide, lanzar una excepción
                    throw new Exception($"El texto del elemento <p> dentro del modal no coincide. Se esperaba: '{expectedText}', pero se encontró: '{actualText}'");
                }

                // Cerrar el modal haciendo clic en el botón "Aceptar"
                IWebElement closeButton = modal.FindElement(By.Id("closeBtn"));
                closeButton.Click();
            }
            catch (NoSuchElementException)
            {
                // Si el modal no se encuentra, lanzar una excepción
                throw new Exception("No se pudo encontrar el modal después de hacer clic en Guardar.");
            }
        }
    }
}
