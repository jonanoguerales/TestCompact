using NUnit.Framework.Interfaces;

namespace TestCompact.Utilities
{
    public class LogsError
    {
        public void LogsErrors()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                string testName = TestContext.CurrentContext.Test.FullName;
                string? errorMessage = TestContext.CurrentContext.Result.Message;

                string filePath = @"C:\\Users\\jona\\Desktop\\errorLogin_log.txt"; // Ruta del archivo de logs

                // Escribir el nombre del test y el mensaje de error en un archivo de texto
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine();
                    writer.WriteLine($"Hora del error: {DateTime.Now}");
                    writer.WriteLine($"Test: {testName}, Error: {errorMessage}");
                }
            }
        }
    }
}
