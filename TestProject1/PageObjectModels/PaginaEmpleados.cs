using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestCompact.PageObjectModels
{
    //Clase para representar la pagina de empleados
    public class PaginaEmpleados
    {
        //Selenium driver
        protected IWebDriver Driver;

        //Constructor.
        public PaginaEmpleados(IWebDriver driver)
        {
            Driver = driver;
        }

        //Localizadores

        //-----Información de contacto-----//
        protected By inputNumEmpleado = By.Id("Id_Personal");//Input no.de empleado
        protected By inputIdentificadorNómina = By.Id("NumeroIdentificador");//Input identificador nómina
        protected By inputNombre = By.Id("Nombre");//Input nombre
        protected By inputPrimerApellido = By.Id("ApellidoPaterno");//Input primer apellido
        protected By inputSegundoApellido = By.Id("ApellidoMaterno");//Input segundo apellido
        protected By inputNombreCorto = By.Id("ClavePersona"); //Input nombre corto
        protected By inputDocumentoDeIdentidad = By.Id("DocumentoIdentidad"); //Input documento de identidad
        protected By inputCorreoElectrónico = By.Id("EMail");//Input correo electrónico
        protected By inputTeléfonoDeCasa = By.Id("TelefonoCasa");//Input teléfono de casa
        protected By inputTeléfonoDeOficina = By.Id("TelefonoOficina");//Input teléfono de oficina
        protected By inputNúmeroDeTeléfonoMóviL = By.Id("TelefonoCelular");//Input número de teléfono móvil:
        protected By inputPuestolaboral = By.Id("PuestoLaboral");//Input puesto laboral

        //-----Datos de control-----//
        protected By inputFechaAlta = By.Id("inputFechaAlta");//Input fecha alta
        protected By inputFechaBaja = By.Id("inputFechaBaja");//Input fecha de baja
        protected By inputMensajeEspecial = By.Id("MensajeEspecial"); //Input mensaje especial():

        //-----Situación Laboral-----//
        protected By inputVacacionesAnuales = By.Id("VacacionesAnuales");//Input vacaciones anuales (días)

        //Botones
        protected By btnMenu = By.ClassName("sidebar-toggle-box");
        protected By btnPersonas = By.CssSelector("#nav-accordion > li:nth-child(4) > a");
        protected By btnListadoPersonas = By.CssSelector("#nav-accordion > li:nth-child(4) > ul > li:nth-child(1) > a");
        //protected By btnNuevo = By.CssSelector("#new > img");
        //protected By btnGuardar = By.CssSelector("#save > img");
        protected By btnBuscar = By.XPath("//*[@id=\"filtertablaDatos\"]/div[3]/div[1]");
        protected By btnEditarPersona = By.XPath("//*[@id=\"row0tablaDatos\"]/td[1]/img");

        protected By inputBuscarPersona = By.XPath("//*[@id=\"filtertablaDatos\"]/div[3]/input");


        //Metodos
        //Método para hacer click en menu
        public void BtnMenu()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

            // Esperar hasta que el botón "Personas" esté presente y sea interactuable
            IWebElement boton = wait.Until(ExpectedConditions.ElementToBeClickable(btnMenu));

            // Hacer clic en el botón "Personas"
            boton.Click();
        }

        //Método para hacer click en Personas
        public void BtnPersonas()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

            // Esperar hasta que el botón "Personas" esté presente y sea interactuable
            IWebElement boton = wait.Until(ExpectedConditions.ElementToBeClickable(btnPersonas));

            // Hacer clic en el botón "Personas"
            boton.Click();
        }
        //Método para hacer click en menu
        public void BtnListadoPersonas()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

            // Esperar hasta que el botón "Personas" esté presente y sea interactuable
            IWebElement boton = wait.Until(ExpectedConditions.ElementToBeClickable(btnListadoPersonas));

            // Hacer clic en el botón "Personas"
            boton.Click();
        }

        ////Método para hacer click en nuevo
        //public void BtnNuevo(string urlDestino)
        //{
        //    // Obtener la URL actual antes de hacer clic en el botón
        //    string urlActual = Driver.Url;

        //    // Hacer clic en el botón "Nuevo"
        //    Driver.FindElement(btnNuevo).Click();

        //    // Esperar a que se complete la navegación a la nueva página
        //    WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        //    bool navegacionCompletada = wait.Until(driver => !driver.Url.Equals(urlActual));

        //    // Si la navegación no se ha completado dentro de 10 segundos, hacer un segundo intento de hacer clic en el botón "Nuevo" 
        //    if (!navegacionCompletada)
        //    {
        //        Console.WriteLine("Segundo intento de hacer clic en el botón 'Nuevo' después de 10 segundos.");
        //        Driver.FindElement(btnNuevo).Click();

        //        // Esperar a que se complete la navegación a la nueva página después del segundo intento
        //        navegacionCompletada = wait.Until(driver => !driver.Url.Equals(urlActual));
        //    }

        //    // Verificar si la URL después de hacer clic en el botón es la esperada
        //    if (navegacionCompletada && Driver.Url.Equals(urlDestino))
        //    {
        //        Console.WriteLine("Se ha redireccionado a la URL esperada.");
        //    }
        //    else
        //    {
        //        throw new ArgumentException($"La URL después de hacer clic en el botón no es la esperada. URL actual: {Driver.Url}");
        //    }
        //}

        //Método para hacer click en buscar
        public void BtnEditarPersona()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

            // Esperar hasta que el botón "Personas" esté presente y sea interactuable
            IWebElement boton = wait.Until(ExpectedConditions.ElementToBeClickable(btnEditarPersona));

            // Hacer clic en el botón "Personas"
            boton.Click();
        }

        //Método para hacer click en editarPersona
        public void BtnBuscar()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

            // Esperar hasta que el botón "Personas" esté presente y sea interactuable
            IWebElement boton = wait.Until(ExpectedConditions.ElementToBeClickable(btnBuscar));

            // Hacer clic en el botón "Personas"
            boton.Click();
        }
        
        //Método para buscar persona
        public void InputBuscarPersona(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputBuscarPersona).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputBuscarPersona).SendKeys(dato);
            }
        }

        //-----Información de contacto-----//
        //Método para escribir el número de empleado
        public void InputNumEmpleado(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputNumEmpleado).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputNumEmpleado).SendKeys(dato);
            }
        }

        //Método para escribir el identificador de nómina
        public void InputIdentificadorNomina(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputIdentificadorNómina).Clear();
                Driver.FindElement(inputIdentificadorNómina).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputIdentificadorNómina).SendKeys(dato);
            }
        }

        //Método para escribir nombre
        public void InputNombre(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputNombre).Clear();    
                Driver.FindElement(inputNombre).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputNombre).SendKeys(dato);
            }
        }

        //Método para escribir primer apellido
        public void InputPrimerApellido(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputPrimerApellido).Clear();
                Driver.FindElement(inputPrimerApellido).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputPrimerApellido).SendKeys(dato);
            }
        }

        //Método para escribir segundo apellido
        public void InputSegundoApellido(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputSegundoApellido).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputSegundoApellido).SendKeys(dato);
            }
        }

        // Método para escribir nombre corto
        public void InputNombreCorto(string? dato)
        {
            if (string.IsNullOrEmpty(dato)||dato == "Vacio")
            {
                Driver.FindElement(inputNombreCorto).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputNombreCorto).SendKeys(dato);
            }
        }

        //Método para escribir documento de identidad
        public void InputDocumentoDeIdentidad(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputDocumentoDeIdentidad).Clear();
                Driver.FindElement(inputDocumentoDeIdentidad).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputDocumentoDeIdentidad).SendKeys(dato);
            }
        }

        //Método para escribir correo electrónico
        public void InputCorreoElectrónico(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputCorreoElectrónico).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputCorreoElectrónico).SendKeys(dato);
            }
        }

        //Método para escribir telefono de casa
        public void InputTelefonoDeCasa(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputTeléfonoDeCasa).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputTeléfonoDeCasa).SendKeys(dato);
            }
        }

        //Método para escribir telefono de oficina
        public void InputTelefonoDeOficina(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputTeléfonoDeOficina).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputTeléfonoDeOficina).SendKeys(dato);
            }
        }

        //Método para escribir telefono movil
        public void InputNúmeroDeTelefonoMoviL(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputNúmeroDeTeléfonoMóviL).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputNúmeroDeTeléfonoMóviL).SendKeys(dato);
            }
        }

        //Método para escribir puesto laboral
        public void InputPuestolaboral(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputPuestolaboral).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputPuestolaboral).SendKeys(dato);
            }
        }

        //-----Datos de control-----//
        //Método para permisos de acceso
        public void BoxDesplegablePermisos(string textoSeleccionado)
        {
            //Input permisos de acceso
            IWebElement btnDesplegable = Driver.FindElement(By.Id("dropdownlistArrowcbPerfiles"));
            btnDesplegable.Click();

            Thread.Sleep(100);

            // Configurar una pausa implícita de 10 segundos
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            IWebElement boxDesplegable = Driver.FindElement(By.CssSelector("#listBoxContentinnerListBoxcbPerfiles > div"));
            
            Actions actions = new Actions(Driver);

            // Desplazarse hasta el contenedor
            actions.MoveToElement(boxDesplegable);
            actions.Perform();

            IReadOnlyCollection<IWebElement> dropdownOptions = boxDesplegable.FindElements(By.TagName("span"));
            foreach (IWebElement dropdownOption in dropdownOptions)
            {
                if (dropdownOption.Text.Equals(textoSeleccionado))
                {
                    dropdownOption.Click();
                }
            }
        }
        //Método para calendario
        public void BoxDesplegableCalendario(string textoSeleccionado)
        {
            //Input calendario
            IWebElement btnDesplegableCalendario = Driver.FindElement(By.Id("dropdownlistArrowcbCalendario"));
            btnDesplegableCalendario.Click();

            Thread.Sleep(TimeSpan.FromSeconds(1));

            IWebElement boxDesplegableCalendario = Driver.FindElement(By.CssSelector("#listBoxContentinnerListBoxcbCalendario > div"));
            
            Actions actions = new Actions(Driver);

            // Desplazarse hasta el contenedor
            actions.MoveToElement(boxDesplegableCalendario);
            actions.Perform();

            IReadOnlyCollection<IWebElement> dropdownOptionsCalendarios = boxDesplegableCalendario.FindElements(By.TagName("span"));
            foreach (IWebElement dropdownOptionsCalendario in dropdownOptionsCalendarios)
            {
                if (dropdownOptionsCalendario.Text.Equals(textoSeleccionado))
                {
                    dropdownOptionsCalendario.Click();
                }
            }
        }

        //Método para escribir fecha alta
        public void InputFechaAlta(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputFechaAlta).Clear();
                Driver.FindElement(inputFechaAlta).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputFechaAlta).SendKeys(dato);
            }
        }

        //Método para escribir fecha baja
        public void InputFechaBaja(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputFechaBaja).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputFechaBaja).SendKeys(dato);
            }
        }

        //Método para escribir mensaje especial
        public void InputMensajeEspecial(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputMensajeEspecial).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputMensajeEspecial).SendKeys(dato);
            }
        }

        //Método para zona horaria
        public void BoxDesplegableZonaHoraria(string? dato)
        {
            if (!string.IsNullOrEmpty(dato) || dato == "Vacío")
            {
                // Verificar si el valor proporcionado está presente en las opciones del elemento select
                bool valorValido = false;

                // Encontrar el elemento select por su ID
                IWebElement selectElement = Driver.FindElement(By.Id("IdTimeZone_jqxComboBox"));

                // Obtener todos los elementos option dentro del select
                IReadOnlyCollection<IWebElement> options = selectElement.FindElements(By.TagName("option"));


                // Iterar sobre los elementos option y obtener sus valores
                foreach (IWebElement option in options)
                {
                    string valor = option.GetAttribute("value");
                    if (valor.Equals(dato))
                    {
                        valorValido = true;
                        break;
                    }
                }

                if (valorValido)
                {
                    // Localizar el campo de entrada por XPath
                    IWebElement campoInput = Driver.FindElement(By.XPath("//*[@id=\"dropdownlistContentIdTimeZone\"]/input"));

                    // Limpiar el contenido del campo de entrada
                    campoInput.Clear();

                    // Ingresar el nuevo dato en el campo de entrada
                    campoInput.SendKeys(dato ?? "");
                }
                else
                {
                    throw new ArgumentException($"El valor proporcionado: '{dato}', no está presente en las opciones de zona horaria.");
                }
            }
        }

        //-----Situación Laboral-----//
        //Método para empresa
        public void BoxDesplegableEmpresa(string dato)
        {
            if (!string.IsNullOrEmpty(dato) || dato == "Vacío")
            {
                // Verificar si el valor proporcionado está presente en las opciones del elemento select
                bool valorValido = false;

                // Encontrar el elemento select por su ID
                IWebElement selectElement = Driver.FindElement(By.Id("Empresa_jqxComboBox"));

                // Obtener todos los elementos option dentro del select
                IReadOnlyCollection<IWebElement> options = selectElement.FindElements(By.TagName("option"));


                // Iterar sobre los elementos option y obtener sus valores
                foreach (IWebElement option in options)
                {
                    string valor = option.GetAttribute("value");
                    if (valor.Equals(dato))
                    {
                        valorValido = true;
                        break;
                    }
                }

                if (valorValido)
                {
                    // Localizar el campo de entrada por XPath
                    IWebElement campoInput = Driver.FindElement(By.XPath("//*[@id=\"dropdownlistContentEmpresa\"]/input"));

                    // Limpiar el contenido del campo de entrada
                    campoInput.Clear();

                    // Ingresar el nuevo dato en el campo de entrada
                    campoInput.SendKeys(dato);
                }
                else
                {
                    throw new ArgumentException($"El valor proporcionado: '{dato}', no está presente en las opciones de empresa.");
                }
            }
        }

        //Método para centro de trabajo
        public void BoxDesplegableCentroTrabajo(string dato)
        {
            if (!string.IsNullOrEmpty(dato) || dato == "Vacío")
            {
                // Verificar si el valor proporcionado está presente en las opciones del elemento select
                bool valorValido = false;

                // Encontrar el elemento select por su ID
                IWebElement selectElement = Driver.FindElement(By.Id("CentroTrabajo_jqxComboBox"));

                // Obtener todos los elementos option dentro del select
                IReadOnlyCollection<IWebElement> options = selectElement.FindElements(By.TagName("option"));


                // Iterar sobre los elementos option y obtener sus valores
                foreach (IWebElement option in options)
                {
                    string valor = option.GetAttribute("value");
                    if (valor.Equals(dato))
                    {
                        valorValido = true;
                        break;
                    }
                }

                if (valorValido)
                {
                    // Localizar el campo de entrada por XPath
                    IWebElement campoInput = Driver.FindElement(By.XPath("//*[@id=\"dropdownlistContentCentroTrabajo\"]/input"));

                    // Limpiar el contenido del campo de entrada
                    campoInput.Clear();

                    // Ingresar el nuevo dato en el campo de entrada
                    campoInput.SendKeys(dato);
                }
                else
                {
                    throw new ArgumentException($"El valor proporcionado: '{dato}', no está presente en las opciones de centro de trabajo.");
                }
            }
        }


        //Método para seccion
        public void BoxDesplegableSeccion(string dato)
        {
            if (!string.IsNullOrEmpty(dato) || dato == "Vacío")
            {
                // Verificar si el valor proporcionado está presente en las opciones del elemento select
                bool valorValido = false;

                // Encontrar el elemento select por su ID
                IWebElement selectElement = Driver.FindElement(By.Id("Seccion_jqxComboBox"));

                // Obtener todos los elementos option dentro del select
                IReadOnlyCollection<IWebElement> options = selectElement.FindElements(By.TagName("option"));


                // Iterar sobre los elementos option y obtener sus valores
                foreach (IWebElement option in options)
                {
                    string valor = option.GetAttribute("value");
                    if (valor.Equals(dato))
                    {
                        valorValido = true;
                        break;
                    }
                }

                if (valorValido)
                {
                    // Localizar el campo de entrada por XPath
                    IWebElement campoInput = Driver.FindElement(By.XPath("//*[@id=\"dropdownlistContentSeccion\"]/input"));

                    // Limpiar el contenido del campo de entrada
                    campoInput.Clear();

                    // Ingresar el nuevo dato en el campo de entrada
                    campoInput.SendKeys(dato);
                }
                else
                {
                    throw new ArgumentException($"El valor proporcionado: '{dato}', no está presente en las opciones de seccion.");
                }
            }
        }

        //Método para departamento
        public void BoxDesplegableDepartamento(string dato)
        {
            if (!string.IsNullOrEmpty(dato) || dato == "Vacío")
            {
                // Verificar si el valor proporcionado está presente en las opciones del elemento select
                bool valorValido = false;

                // Encontrar el elemento select por su ID
                IWebElement selectElement = Driver.FindElement(By.Id("Departamento_jqxComboBox"));

                // Obtener todos los elementos option dentro del select
                IReadOnlyCollection<IWebElement> options = selectElement.FindElements(By.TagName("option"));


                // Iterar sobre los elementos option y obtener sus valores
                foreach (IWebElement option in options)
                {
                    string valor = option.GetAttribute("value");
                    if (valor.Equals(dato))
                    {
                        valorValido = true;
                        break;
                    }
                }

                if (valorValido)
                {
                    // Localizar el campo de entrada por XPath
                    IWebElement campoInput = Driver.FindElement(By.XPath("//*[@id=\"dropdownlistContentDepartamento\"]/input"));

                    // Limpiar el contenido del campo de entrada
                    campoInput.Clear();

                    // Ingresar el nuevo dato en el campo de entrada
                    campoInput.SendKeys(dato);
                }
                else
                {
                    throw new ArgumentException($"El valor proporcionado: '{dato}', no está presente en las opciones de departamento.");
                }
            }
        }

        //Método para escribir vacaciones anuales (días)
        public void InputVacacionesAnuales(string? dato)
        {
            if (string.IsNullOrEmpty(dato) || dato == "Vacio")
            {
                Driver.FindElement(inputVacacionesAnuales).SendKeys("");
            }
            else
            {
                Driver.FindElement(inputVacacionesAnuales).SendKeys(dato);
            }
        }

        //Método para pertenece a protección civil
        public void BoxDesplegableProteccionCivil(string dato)
        {
            if (!string.IsNullOrEmpty(dato) || dato == "Vacío")
            {
                // Verificar si el valor proporcionado está presente en las opciones del elemento select
                bool valorValido = false;

                // Encontrar el elemento select por su ID
                IWebElement selectElement = Driver.FindElement(By.Id("ProteccionCivil_jqxComboBox"));

                // Obtener todos los elementos option dentro del select
                IReadOnlyCollection<IWebElement> options = selectElement.FindElements(By.TagName("option"));

                // Iterar sobre los elementos option y obtener sus valores
                foreach (IWebElement option in options)
                {
                    string valor = option.GetAttribute("value");
                    if (valor.Equals(dato))
                    {
                        valorValido = true;
                        break;
                    }
                }

                if (valorValido)
                {
                    // Localizar el campo de entrada por XPath
                    IWebElement campoInput = Driver.FindElement(By.XPath("//*[@id=\"dropdownlistContentProteccionCivil\"]/input"));

                    // Limpiar el contenido del campo de entrada
                    campoInput.Clear();

                    // Ingresar el nuevo dato en el campo de entrada
                    campoInput.SendKeys(dato);
                }
                else
                {
                    throw new ArgumentException($"El valor proporcionado: '{dato}', no está presente en las opciones de pertenece a protección civil.");
                }
            }
        }        
    }
}

