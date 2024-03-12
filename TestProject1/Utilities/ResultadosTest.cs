using NUnit.Framework.Interfaces;

namespace TestCompact.Utilities
{
    public class ResultadosTest
    {
        public void LogsResultado()
        {
            string testName = TestContext.CurrentContext.Test.FullName;
            string? errorMessage = TestContext.CurrentContext.Result.Message;
            TestStatus testStatus = TestContext.CurrentContext.Result.Outcome.Status;

            string filePath = @"C:\\Users\\JonathanNoguerales\\Desktop\\resultadostest_log.txt"; // Ruta del archivo de logs

            // Escribir el nombre del test, el resultado y el mensaje (si lo hay) en un archivo de texto
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine();
                writer.WriteLine($"Prueba realizada el : {DateTime.Now}");
                writer.WriteLine($"Test: {testName}, Resultado: {testStatus}");

                // Si el test es exitoso y no hay mensaje de error, registramos que el test ha pasado
                if (testStatus == TestStatus.Passed && errorMessage == null)
                {
                    writer.WriteLine($"El test ha pasado correctamente.");
                }
                // Si hay un mensaje de error, registramos el mensaje de error
                else if (testStatus == TestStatus.Failed && errorMessage != null)
                {
                    writer.WriteLine($"Mensaje de error: {errorMessage}");
                }
            }
        }
    }
}

